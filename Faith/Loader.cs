using ff14bot;
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
    /// <summary>
    /// Loads and proxies precompiled Faith BotBase assemblies.
    /// </summary>
    public class Loader : ff14bot.AClasses.BotBase
    {
        private const string ProjectName = "Faith";
        private const string ProjectMainType = "Faith.Startup";
        private const string ProjectAssemblyName = "Faith.dll";
        private static readonly Color _logColor = Colors.Aqua;

        /// <inheritdoc/>
        public override PulseFlags PulseFlags => PulseFlags.All;

        /// <inheritdoc/>
        public override bool IsAutonomous => true;

        /// <inheritdoc/>
        public override bool WantButton => true;

        /// <inheritdoc/>
        public override bool RequiresProfile => false;

        private static readonly string _projectAssembly = Path.Combine(Environment.CurrentDirectory, $@"BotBases\{ProjectName}\{ProjectAssemblyName}");
        private static readonly string _greyMagicAssembly = Path.Combine(Environment.CurrentDirectory, @"GreyMagic.dll");
        private static Func<Composite> _root;
        private static Action _onButtonPress, _start, _stop;

        private static readonly Composite _failsafeRoot = new TreeSharp.Action(c =>
        {
            Log($"{ProjectName} is not loaded correctly.");
            TreeRoot.Stop();
        });

        /// <summary>
        /// Initializes a new instance of the <see cref="Loader"/> class.  Created by RebornBuddy during bot startup.
        /// </summary>
        public Loader()
        {
            Load(Dispatcher.CurrentDispatcher);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string Name => ProjectName;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Composite Root => _root() ?? _failsafeRoot;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void OnButtonPress()
        {
            _onButtonPress?.Invoke();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Start()
        {
            _start?.Invoke();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Stop()
        {
            _stop?.Invoke();
        }

        private void Load(Dispatcher dispatcher)
        {
            RedirectAssembly();

            Assembly assembly = LoadAssembly(_projectAssembly);
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

                Type type = product.GetType();
                _root = (Func<Composite>)type.GetProperty("Root")?.GetValue(product);
                _start = (Action)type.GetProperty("OnStart")?.GetValue(product);
                _stop = (Action)type.GetProperty("OnStop")?.GetValue(product);
                _onButtonPress = (Action)type.GetProperty("OnButtonPress")?.GetValue(product);

                Log($"{ProjectName} loaded.");
            }));
        }

        private static void RedirectAssembly()
        {
            Assembly Handler(object sender, ResolveEventArgs args)
            {
                string name = Assembly.GetEntryAssembly()?.GetName().Name;
                AssemblyName requestedAssembly = new AssemblyName(args.Name);
                return requestedAssembly.Name != name ? null : Assembly.GetEntryAssembly();
            }

            AppDomain.CurrentDomain.AssemblyResolve += Handler;

            Assembly GreyMagicHandler(object sender, ResolveEventArgs args)
            {
                AssemblyName requestedAssembly = new AssemblyName(args.Name);
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
