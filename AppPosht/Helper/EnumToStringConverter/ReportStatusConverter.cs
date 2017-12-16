using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using MahApps.Metro.Converters;

namespace AppPosht.Helper.EnumToStringConverter
{
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public sealed class ReportStatusConverter : MarkupConverter
    {
        private static ReportStatusConverter _instance;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new ReportStatusConverter());
        }

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return ReportStatusResource.ResourceManager.GetString(value.ToString());
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}