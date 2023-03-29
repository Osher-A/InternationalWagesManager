using System;
using System.Globalization;
using System.Windows.Data;

namespace InternationalWagesManager.Views.converters
{
    internal class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                bool isDateTime = DateTime.TryParse(value.ToString(), out DateTime date);
                if (isDateTime)
                    return $"{date.ToShortDateString()}";

            }
            return "00/00/0000";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringDate = (string)value;
            if (DateTime.TryParse(stringDate, culture, DateTimeStyles.None, out var date))
                return date;
            return value;

        }
    }
}
