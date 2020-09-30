using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Threading;

namespace Faith.Localization
{
    internal class LocalizationProvider
    {
        private readonly ILogger<LocalizationProvider> _logger;

        public LocalizationProvider(ILogger<LocalizationProvider> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Sets the user-facing language and number presentation for the BotBase UI and logging.
        /// 
        /// Localized strings are loaded from Localization.{cultureCode}.resx files.  Unlocalized strings default to placeholders from Localization.resx
        /// </summary>
        /// <param name="cultureCode">Localization to display.</param>
        public void SetLocalization(string cultureCode)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureCode);
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureCode);
                _logger.LogDebug(Translations.LOG_LOCALIZATION_CHANGED, cultureCode);
            }
            catch (CultureNotFoundException)
            {
                _logger.LogError(Translations.LOG_LOCALIZATION_NOT_FOUND, cultureCode);
            }
        }
    }
}
