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
            return date == null ? default(object) : new PersianDate(date.Value);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            PersianDate? pDate = value as PersianDate?;
            return pDate == null ? default(object) : pDate.Value.ToDateTime();
        }
    }
}
