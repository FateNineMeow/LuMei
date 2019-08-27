using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace LuMei.Control.Conver
{
    /// <summary>
    /// ��ת�������˺ܶ����ͼ���������ͼ��һ�������ؾ��ͷ�����
    /// �Ա����ڳ���ɾ���ļ�ʱ�����ļ������쳣
    /// ��Σ�����ͼ�����Ϊ�����Ŀ��͸߶ȣ�
    /// �����ͼ����Ҫ�������ŵĳ��Ͻ�ʡ���ڴ档
    /// </summary>
    public sealed class BitmapFrameConverter : IValueConverter
    {
        //ʹ��double�������ڰ�
        private double _decodePixelWidth;
        private double _decodePixelHeight;
        private bool _haveSize;
        private string _defaultImage;
        //����ͼ����
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
        //����ͼ��߶�
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
        //IValueConverter��ת��������
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = value as string;//�õ��ļ�·��
                                          //����ļ�·������
            if (path != null)
            {
                //����һ���µ�BitmapImage�����Լ�һ���µ��ļ���
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();//��ʼ����״̬
                                        //ָ��BitmapImage��StreamSourceΪ��ָ��·���򿪵��ļ���
                bitmapImage.StreamSource = new FileStream(path, FileMode.Open, FileAccess.Read);
                if (_haveSize)
                {
                    bitmapImage.DecodePixelWidth = (int)_decodePixelWidth;//����ͼ��Ŀ��
                    bitmapImage.DecodePixelHeight = (int)_decodePixelHeight;//����ͼ��ĸ߶�
                }
                //����Image���Ա������ͷ���
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();//��������
                                      //������Ա����ڳ���ɾ��ͼ��ʱ�����ļ������쳣
                bitmapImage.StreamSource.Dispose();
                return bitmapImage;//����BitmapImage
            }
            else
            {
                return new BitmapImage(new Uri(_defaultImage, UriKind.Relative));
            }
        }
        //��ת������������Ҫ����ʵ��
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
