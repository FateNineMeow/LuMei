using System.Windows;
using System.Windows.Threading;
using LuMei.Helper;
using Ay.Framework.WPF;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System;
using Ay.Framework.WPF.Applications;

namespace LuMei
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        [DllImport("user32", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string cls, string win);
        [DllImport("user32")]
        static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32")]
        static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32")]
        static extern bool OpenIcon(IntPtr hWnd);
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        private const int WS_SHOWNORMAL = 1;

        public App()
        {
            Process instance = RunningInstance();
            if (instance != null)
            {
                ActivateOtherWindow();
                Current.Shutdown();
            }
            else
                Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }
        void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            AyMessageBox.Show(e.Exception.Message, MessageBoxButton.OK, AyMessageBoxImage.Error);
            AyMessageBox.Show("我们很抱歉，当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系符文之地-九命喵式微", "意外的操作");//这里通常需要给用户一些较为友好的提示，并且后续可能的操作
            Log.LogError("未知错误", e.Exception);
            e.Handled = true;//使用这一行代码告诉运行时，该异常被处理了，不再作为UnhandledException抛出了。
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            Picture.CopyFile();
            ConfigSetting.ConfigSettingApplicationXmlFilePath = Soft.AppXmlPath;
            //设置整个项目的资源主题
            Current.AddResourceDictionary(@"/Style/TestDictionary.xaml").AYUI();
            Current.AddResourceDictionary(@"/Style/ListBox.xaml").AYUI();
            Current.AddResourceDictionary(@"/Style/Button.xaml").AYUI();
            base.OnStartup(e);
        }
        private static Process RunningInstance()
        {
            var pro = Process.GetProcesses();
            Process current = Process.GetCurrentProcess();
            Log.LogInfo(current.ProcessName);
            Process[] processes = Process.GetProcessesByName("LuMei");
            //遍历与当前进程名称相同的进程列表  
            foreach (Process process in processes)
            {
                //如果实例已经存在则忽略当前进程  
                if (process.Id != current.Id)
                {
                    //保证要打开的进程同已经存在的进程来自同一文件路径
                    //  if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    //返回已经存在的进程
                    return process;
                }
            }
            return null;
        }

        private static void ActivateOtherWindow()
        {
            var other = FindWindow(null, "撸妹挂载器");
            if (other != IntPtr.Zero)
            {
                SetForegroundWindow(other);
                if (IsIconic(other))
                    OpenIcon(other);
            }
        }

    }
}
