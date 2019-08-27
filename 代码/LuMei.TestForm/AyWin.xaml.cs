using Ay.Framework.WPF.Controls;
using System.IO;
using System.Windows.Media.Imaging;

namespace LuMei.TestForm
{
    /// <summary>
    /// AyWin.xaml 的交互逻辑
    /// </summary>
    public partial class AyWin : AyWindow
    {
        public AyWin()
        {
            InitializeComponent();
           
            var filePath = "123.jpg";
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                FileInfo fi = new FileInfo(filePath);
                byte[] bytes = reader.ReadBytes((int)fi.Length);
                imagesssss = new BitmapImage();
                imagesssss.BeginInit();
                imagesssss.StreamSource = new MemoryStream(bytes);
                imagesssss.EndInit();
            }
        }
        public BitmapImage imagesssss { get; set; }
    }
}
