using TreeSharp;

namespace Faith.BotBase
{
    /// <summary>
    /// <see cref="ff14bot.AClasses.BotBase"/> that receives proxied method calls from a loader.
    /// </summary>
    internal interface IProxiedBotBase
    {
        /// <summary>
        /// Gets the start of the BotBase's behavior tree.
        /// </summary>
        Composite Root { get; }

        /// <summary>
        /// Called when pressing the "Botbase Settings" button in RebornBuddy.
        /// </summary>
        void OnButtonPress();

        /// <summary>
        /// Called when pressing the "Start" button in RebornBuddy.
        /// </summary>
        void OnStart();

        /// <summary>
        /// Called when pressing the "Stop" button in RebornBuddy.
        /// </summary>
        void OnStop();
    }
}
