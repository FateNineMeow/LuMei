using Ay.Framework.WPF;
using System.Windows;
using Ay.Framework.WPF.Controls;
namespace LuMei
{
    /// <summary>
    /// WindowMain.xaml 的交互逻辑
    /// </summary>
    public partial class WindowMain:AyWindow
    {
        public WindowMain()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AyMessageBox.ShowCus("这里是内容", "成功！", @"F:\NetDisk\360\Sync\Other\Model\8-18\bianfu\m003_2.png");
        }
    }
}
