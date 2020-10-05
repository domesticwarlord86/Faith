using Faith.Logging;
using Faith.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Faith.Localization
{
    /// <summary>
    /// Localization helpers.
    /// </summary>
    internal class LocalizationProvider : AbstractLoggable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationProvider"/> class.
        /// </summary>
        public LocalizationProvider(
            ILogger<LocalizationProvider> logger,
            IOptionsMonitor<FaithOptions> optionsMonitor
        ) : base(logger)
        {
            SetLocalization(optionsMonitor.CurrentValue.Localization);
            optionsMonitor.OnChange((options) => SetLocalization(options.Localization));
        }

        /// <summary>
        /// Sets the user-facing language and number presentation for the BotBase UI and logging.
        /// 
        /// Localized strings are loaded from Localization.{cultureCode}.resx files.  Unlocalized strings default to placeholders from Localization.resx
        /// </summary>
        /// <param name="cultureCode">Localization to display.</param>
        public void SetLocalization(string cultureCode)
        {
            CultureInfo culture;

            try
            {
                culture = CultureInfo.GetCultureInfo(cultureCode);
            }
            catch (CultureNotFoundException)
            {
                Logger.LogError(Translations.LOG_LOCALIZATION_NOT_FOUND, cultureCode);
                return;
            }

            if (Translations.Culture == culture)
            {
                return;
            }

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            Translations.Culture = culture;

            Logger.LogInformation(Translations.LOG_LOCALIZATION_CHANGED, cultureCode);
        }
    }
}
