using System.Globalization;
using System.Resources;

namespace CatApp.Resources.Utils.Localization
{
    public class TranslateConverter : IValueConverter
    {
        private static readonly ResourceManager resourceManager = new("CatApp.Resources.Localization.Lang", typeof(App).Assembly);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.ToString() == null || value.Equals(string.Empty)) return string.Empty;

            string format = parameter as string;

            if (string.IsNullOrEmpty(format))
                return resourceManager.GetString(value.ToString(), culture);
            else
                return resourceManager.GetString(string.Format(format, value), culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
