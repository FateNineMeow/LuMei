using System.Windows;
using System.Windows.Threading;

namespace LuMei.TestForm
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }
        void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            //这里通常需要给用户一些较为友好的提示，并且后续可能的操作
           
            e.Handled = true;//使用这一行代码告诉运行时，该异常被处理了，不再作为UnhandledException抛出了。
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //设置整个项目的资源主题
            Current.AddResourceDictionary(@"/Style/TestDictionary.xaml").AYUI();
            base.OnStartup(e);
        }
    }
}
