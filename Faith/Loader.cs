using ff14bot;
using ff14bot.AClasses;
using ff14bot.Behavior;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Threading;
using TreeSharp;
using Action = System.Action;

namespace Faith
{
    public class Loader : BotBase
    {
        private const string ProjectName = "Faith";
        private const string ProjectMainType = "Faith.Faith";
        private const string ProjectAssemblyName = "Faith.dll";
        private static readonly Color _logColor = Color.FromRgb(0, 255, 0);
        public override PulseFlags PulseFlags => PulseFlags.All;
        public override bool IsAutonomous => true;
        public override bool WantButton => true;
        public override bool RequiresProfile => false;

        public object Faith { get; set; }

        private static readonly string _projectAssembly = Path.Combine(Environment.CurrentDirectory, $@"BotBases\{ProjectName}\{ProjectAssemblyName}");
        private static readonly string _greyMagicAssembly = Path.Combine(Environment.CurrentDirectory, @"GreyMagic.dll");
        private static Composite _root;
        private static Action _onButtonPress, _start, _stop;

        private static readonly Composite _failsafeRoot = new TreeSharp.Action(c =>
        {
            Log($"{ProjectName} is not loaded correctly.");
            TreeRoot.Stop();
        });

        public Loader()
        {
            Load(Dispatcher.CurrentDispatcher);
        }

        public override string Name => ProjectName;

        public override Composite Root => _root ?? _failsafeRoot;

        public override void OnButtonPress() => _onButtonPress?.Invoke();

        public override void Start() => _start?.Invoke();

        public override void Stop() => _stop?.Invoke();

        private void Load(Dispatcher dispatcher)
        {
            RedirectAssembly();

            var assembly = LoadAssembly(_projectAssembly);
            if (assembly == null) { return; }

            Type baseType;
            try { baseType = assembly.GetType(ProjectMainType); }
            catch (Exception e)
            {
                Log(e.ToString());
                return;
            }

            dispatcher.BeginInvoke(new Action(() =>
            {
                object product;
                try { product = Activator.CreateInstance(baseType); }
                catch (Exception e)
                {
                    Log(e.ToString());
                    return;
                }

                var type = product.GetType();
                _root = (Composite)type.GetProperty("Root")?.GetValue(product);
                _start = (Action)type.GetProperty("OnStart")?.GetValue(product);
                _stop = (Action)type.GetProperty("OnStop")?.GetValue(product);
                _onButtonPress = (Action)type.GetProperty("OnButtonPress")?.GetValue(product);
                Faith = product;

                Log($"{ProjectName} loaded.");
            }));
        }

        private static void RedirectAssembly()
        {
            Assembly Handler(object sender, ResolveEventArgs args)
            {
                var name = Assembly.GetEntryAssembly()?.GetName().Name;
                var requestedAssembly = new AssemblyName(args.Name);
                return requestedAssembly.Name != name ? null : Assembly.GetEntryAssembly();
            }

            AppDomain.CurrentDomain.AssemblyResolve += Handler;

            Assembly GreyMagicHandler(object sender, ResolveEventArgs args)
            {
                var requestedAssembly = new AssemblyName(args.Name);
                return requestedAssembly.Name != "GreyMagic" ? null : Assembly.LoadFrom(_greyMagicAssembly);
            }

            AppDomain.CurrentDomain.AssemblyResolve += GreyMagicHandler;
        }

        private static Assembly LoadAssembly(string path)
        {
            if (!File.Exists(path)) { return null; }

            Assembly assembly = null;
            try { assembly = Assembly.LoadFrom(path); }
            catch (Exception e) { ff14bot.Helpers.Logging.WriteException(e); }

            return assembly;
        }

        private static void Log(string message)
        {
            ff14bot.Helpers.Logging.Write(_logColor, $"[{ProjectName}] {message}");
        }
    }
}
