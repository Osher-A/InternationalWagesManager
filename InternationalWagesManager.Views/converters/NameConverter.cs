using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InternationalWagesManager.Views.converters
{
    public class NameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = (value as string)!;
            var splitName = name.Split(' ');
            var firstName = splitName[0];
            var lastName = splitName[splitName.Length - 1];
            lastName = splitName.Length > 1 ? '(' + lastName + ')' : lastName;
            lastName = lastName + "'s";
            return firstName + lastName;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
