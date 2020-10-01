using ff14bot.Directors;
using ff14bot.Managers;
using System;

namespace Faith.Helpers
{
    /// <summary>
    /// Simplified readouts for the active instanced content.
    /// </summary>
    public static class CurrentInstance
    {
        /// <summary>
        /// Instanced content-specific Director.  <see langword="null"/> if not inside a relevant instance.
        /// </summary>
        private static InstanceContentDirector InstanceDirector => DirectorManager.ActiveDirector as InstanceContentDirector;

        /// <summary>
        /// Gets the current instance ID.
        /// </summary>
        public static uint Id => InstanceDirector.DungeonId;

        /// <summary>
        /// Gets the current instance name in the current game client localization.
        /// </summary>
        public static string Name => WorldManager.CurrentLocalizedZoneName;

        /// <summary>
        /// Gets the current instance name in English.
        /// </summary>
        public static string EnglishName => WorldManager.CurrentZoneName;

        /// <summary>
        /// <see langword="true"/> if in instance at any stage of loading (black screen, barrier, duty commenced, etc).
        /// </summary>
        public static bool IsInInstance => InstanceDirector != null;

        /// <summary>
        /// <see langword="true"/> if instance is fully loaded, dungeon barrier is down, and "DUTY COMMENCED" has displayed.
        /// </summary>
        public static bool IsDutyCommenced
        {
            get
            {
                if (InstanceDirector != null)
                {
                    InstanceFlags flags = (InstanceFlags)InstanceDirector.InstanceFlags;
                    return flags.HasFlag(InstanceFlags.DUTY_COMMENCED);
                }

                return false;
            }
        }

        /// <summary>
        /// <see langword="true"/> if instance has been completed.
        /// </summary>
        public static bool IsDutyComplete => InstanceDirector?.InstanceEnded ?? false;

        /// <summary>
        /// Meanings of individual bits in InstanceFlags.
        /// </summary>
        [Flags]
        enum InstanceFlags : byte
        {
            LOADED = 0b0000_0100,
            BARRIER_DOWN = 0b0000_1000,
            DUTY_COMMENCED = 0b0001_0000,
        }

        /// <summary>
        /// Meanings of individual bits in InstanceSubFlags.
        /// </summary>
        [Flags]
        enum InstanceSubFlags : byte
        {
            DUTY_COMPLETE = 0b0001_0000,
        }
    }
}
