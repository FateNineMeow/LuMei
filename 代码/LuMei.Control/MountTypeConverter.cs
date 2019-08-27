using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LuMei.Control.Conver
{
    public class MountTypeConverter : IValueConverter
    {
        /// <summary>
        /// 将Statu转换为bool?
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string c = (string)value;
            return c == "已挂载" ? Visibility.Visible : Visibility.Hidden;

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
