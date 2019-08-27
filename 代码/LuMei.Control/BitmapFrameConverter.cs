using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace LuMei.Control.Conver
{
    /// <summary>
    /// 本转换器简化了很多关于图像的需求，在图像一旦被加载就释放流，
    /// 以避免在尝试删除文件时出现文件访问异常
    /// 其次，允许将图像调整为整定的宽工和高度，
    /// 因而在图像需要进行缩放的场合节省了内存。
    /// </summary>
    public sealed class BitmapFrameConverter : IValueConverter
    {
        //使用double用来利于绑定
        private double _decodePixelWidth;
        private double _decodePixelHeight;
        private bool _haveSize;
        private string _defaultImage;
        //调整图像宽度
        public double DecodePixelWidth
        {
            get
            {
                return _decodePixelWidth;
            }
            set
            {
                _decodePixelWidth = value;
            }
        }
        //调整图像高度
        public double DecodePixelHeight
        {
            get
            {
                return _decodePixelHeight;
            }
            set
            {
                _decodePixelHeight = value;
            }
        }
        public bool HaveSize
        {
            get
            {
                return _haveSize;
            }
            set
            {
                _haveSize = value;
            }
        }
        public string DefaultImage
        {
            get
            {
                return _defaultImage;
            }
            set
            {
                _defaultImage = value;
            }
        }
        //IValueConverter的转换器方法
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = value as string;//得到文件路径
                                          //如果文件路径存在
            if (path != null)
            {
                //创建一个新的BitmapImage对象以及一个新的文件流
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();//开始更新状态
                                        //指定BitmapImage的StreamSource为按指定路径打开的文件流
                bitmapImage.StreamSource = new FileStream(path, FileMode.Open, FileAccess.Read);
                if (_haveSize)
                {
                    bitmapImage.DecodePixelWidth = (int)_decodePixelWidth;//设置图像的宽度
                    bitmapImage.DecodePixelHeight = (int)_decodePixelHeight;//设置图像的高度
                }
                //加载Image后以便立即释放流
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();//结束更新
                                      //清除流以避免在尝试删除图像时出现文件访问异常
                bitmapImage.StreamSource.Dispose();
                return bitmapImage;//返回BitmapImage
            }
            else
            {
                return new BitmapImage(new Uri(_defaultImage, UriKind.Relative));
            }
        }
        //反转换方法，不需要进行实现
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
