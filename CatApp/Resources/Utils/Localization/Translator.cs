using System.Diagnostics;
using System.Globalization;
using System.Resources;

namespace CatApp.Resources.Utils.Localization
{
    public class Translator
    {
        private static readonly ResourceManager resourceManager = new("CatApp.Resources.Localization.Lang", typeof(App).Assembly);

        public static string GetString(string text) => GetString(text, CultureInfo.CurrentUICulture);

        public static string GetString(string text, CultureInfo culture)
        {
            if (text == null)
                return string.Empty;

            string translation = resourceManager.GetString(text, culture);
            if (translation == null)
            {
#if DEBUG
                var ex = new ArgumentException(
                    string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", text, "CatApp.Resources.Localization.Lang", culture.Name),
                    "Text");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.Message);
                translation = text;
                throw ex; // (Adrian): at Tim's request, this will crash the app in development so that we can fix missing translations before they go live.

#else
				// returns the key, which GETS DISPLAYED TO THE USER
                translation = text;
#endif
            }
            return translation;
        }

        public static string? TryToGetString(string text) => Translator.TryToGetString(text, CultureInfo.CurrentUICulture);

        public static string? TryToGetString(string text, CultureInfo ci)
        {
            return (text == null) ? null : resourceManager.GetString(text, ci);
        }
    }
}
