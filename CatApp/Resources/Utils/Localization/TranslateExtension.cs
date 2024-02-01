using System.Diagnostics;
using System.Resources;

namespace CatApp.Resources.Utils.Localization
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private static readonly ResourceManager resourceManager = new("CatApp.Resources.Localization.Lang", typeof(App).Assembly);

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;

            string translation = resourceManager.GetString(Text);

            if (translation == null)
            {
#if DEBUG
                Debug.WriteLine($"Key '{Text}' was not found in the localization resource file.");
#else
            translation = Text; // Fallback to the key
#endif
            }
            return translation;
        }
    }
}
