using System;
using System.Globalization;
using System.Windows.Data;

namespace LuMei.TestForm
{
    public class StringConverter : IValueConverter
    {
        /// <summary>
        /// 将Statu转换为bool?
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
