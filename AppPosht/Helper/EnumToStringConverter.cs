using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using AppPosht.Properties;
using MahApps.Metro.Converters;

namespace AppPosht.Helper
{
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public sealed class EnumToStringConverter : MarkupConverter
    {
        private static EnumToStringConverter _instance;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new EnumToStringConverter());
        }

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            { return null; }

            return Resources.ResourceManager.GetString(value.ToString());
            
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;

            foreach (object enumValue in Enum.GetValues(targetType))
            {
                if (str == Resources.ResourceManager.GetString(enumValue.ToString()))
                { return enumValue; }
            }

            throw new ArgumentException(null, nameof(value));
        }
    }
}