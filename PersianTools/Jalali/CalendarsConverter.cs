using System;
using System.Windows.Data;

namespace PersianTools.Jalali
{
    /// <summary>
    /// Converts DateTime values to PersianDate values and vice versa, to be used in XAML
    /// </summary>
    [ValueConversion(typeof (DateTime), typeof (PersianDate))]
    public class CalendarsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime? date = value as DateTime?;
            if (date == null) return null;
            return new PersianDate(date.Value);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            PersianDate? pDate = value as PersianDate?;
            if (pDate == null) return null;
            return pDate.Value.ToDateTime();
        }
    }
}
