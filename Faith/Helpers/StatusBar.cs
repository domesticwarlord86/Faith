using ff14bot;

namespace Faith.Helpers
{
    /// <summary>
    /// Wrapper around RebornBuddy's Status Bar.
    /// </summary>
    public static class StatusBar
    {
        /// <summary>
        /// Text displayed in RebornBuddy's Status Bar.
        /// </summary>
        public static string Text
        {
            get { return TreeRoot.StatusText; }
            set { TreeRoot.StatusText = value; }
        }

        /// <summary>
        /// Clears the Status Bar text.
        /// </summary>
        public static void Clear()
        {
            Text = string.Empty;
        }
    }
}