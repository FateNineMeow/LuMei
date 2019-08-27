using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LuMei.Helper;
using LuMei.Model;
using LuMei.Pages;
using Ay.Framework.WPF;

namespace LuMei.IO
{
    /// <summary>
    /// 单例函数
    /// </summary>
    class SoftParam
    {
        private static readonly object LockHelper = new object();
        public static SoftParam Instance;
        private readonly ILuMeiClass _lumei = new LuMeiClass();

        private SoftParam() { }
        public static SoftParam GetInstance()
        {
            if (Instance == null)
            {
                //这里的lock其实使用的原理可以用一个词语来概括“互斥”这个概念也是操作系统的精髓
                //其实就是当一个进程进来访问的时候，其他进程便先挂起状态
                lock (LockHelper)
                {
                    if (Instance == null)
                        Instance = new SoftParam();
                }
            }
            return Instance;
        }
        public MainW MainW { get; set; }
        public PageSkin SkinP { get; set; }
        public PageHero HeroP { get; set; }
        /// <summary>
        /// 设置背景图
        /// </summary>
        /// <param name="enname"></param>
        public void SetBackImage(string enname)
        {
            var path = Picture.HeroOriginalPath(enname);
            if (!File.Exists(path))
                path = Soft.LogoIcoPath;
            MainW.ReLoadImageBmp(path);
        }
        /// <summary>
        /// 设置背景图
        /// </summary>
        /// <param name="enname"></param>
        public void SetBackImage(Window win, string enname)
        {
            var path = Picture.HeroOriginalPath(enname);
            if (string.IsNullOrWhiteSpace(path))
                path = Soft.LogoIcoPath;
            ImageBrush b3 = new ImageBrush { ImageSource = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute)) };
            win.Background = b3;
        }
        /// <summary>
        /// 设置背景图
        /// </summary>
        /// <param name="skin"></param>
        public void SetBackImage(Skin skin)
        {
            var path = Picture.SkinOriginalPath(skin);
            MainW.ReLoadImageBmp(path);
        }
        public void BackPageSkin()
        {
            if (SkinP != null)
            {
                SkinP.SkinList.SelectedIndex = -1;
                SkinP.UpdateSkinList();
                MainW.HeroFrame.NavigationService.Navigate(SkinP);
            }
            else
                BackPageHero();
        }
        public void BackPageHero()
        {
            if (HeroP != null)
            {
                HeroP.UpdateHeroList();
                HeroP.HeroList.SelectedIndex = -1;
            }
            MainW.HeroFrame.NavigationService?.Navigate(HeroP ?? new PageHero());
        }
        public void FileDropIn(string[] argument)
        {
            if (argument.Length < 1) return;
            FileDrop(argument);
        }

        private void FileDrop(object argument)
        {
            var list = (string[])argument;
            if (list.Length < 1) return;
            MainW.MsgText.Text = "开始导入皮肤";
            MainW.MsgGrid.Visibility = Visibility.Visible;
            foreach (var item in list)
            {
                if (!File.Exists(item))
                    continue;
                CheckFile(item);
            }
            MainW.MsgGrid.Visibility = Visibility.Hidden;
        }
        private void CheckFile(string file)
        {
            var ex = Path.GetExtension(file).ToLower();
            //  var ex = Path.GetExtension(file)?.ToLower();
            switch (ex)
            {
                case ".skn":
                    _lumei.SknConverter(file);
                    break;
                case ".zip":
                    var imp = new SkinImport();
                    imp.StartImport(file);
                    break;
                default:
                    AyMessageBox.Show("无法识别此文件格式！", "未知格式");
                    break;
            }
        }
        public void SetTheme(string name)
        {
            try
            {
                //var theme = ThemeManager.DetectAppStyle(Application.Current);
                //var accent = ThemeManager.GetAccent(name);
                //ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
                //Config.SoftSave("SoftTheme", name);
            }
            catch
            {
                // ignored
            }
        }
        public void ReInstall()
        {
            AyThread.Instance.InitDispatcher(MainW.Dispatcher);

            //无返回值，启动其他线程，不阻塞
            AyThread.Instance.RunNew(() =>
            {
                //然后更新UI
                AyThread.Instance.RunUI(() =>
                {
                    MainW.MsgText.Text = "正在重新挂载...\r\n请等待操作完成！";
                    MainW.MsgGrid.Visibility = Visibility.Visible;
                });
                //其他很多的操作
                //一个耗时5秒的任务
                // AyThread.Instance.Sleep(5000);
                _lumei.SkinReMount();
                //然后更新UI
                AyThread.Instance.RunUI(() =>
                {
                    MainW.MsgGrid.Visibility = Visibility.Hidden;
                });
            });
        }
        public void AllUninstall()
        {
            AyThread.Instance.InitDispatcher(MainW.Dispatcher);

            //具有返回值的线程
            AyThread.Instance.RunNew<bool>(() =>
            {
                //然后更新UI
                AyThread.Instance.RunUI(() =>
                {
                    MainW.MsgText.Text = "正在批量卸载皮肤...\r\n请等待操作完成！";
                    MainW.MsgGrid.Visibility = Visibility.Visible;
                });
                //其他很多的操作
                //一个耗时5秒的任务
                // AyThread.Instance.Sleep(5000);
                _lumei.SkinAllUninstall();
                //然后更新UI
                AyThread.Instance.RunUI(() =>
                {
                    MainW.MsgGrid.Visibility = Visibility.Hidden;
                });
                return true;

            }, (result) =>
            { //完成任务后，返回值，是异步操作的
                if (result)
                    HeroP.HeroList.Dispatcher.BeginInvoke(new Action(() => { HeroP.HeroList.DataContext = _lumei.AllHero(); }), null);
            });
        }

        public void ShowMsg(string msg)
        {
            MainW.MsgText.Dispatcher.BeginInvoke(new Action(() => { MainW.MsgText.Text = msg; }), null);
            MainW.MsgGrid.Dispatcher.BeginInvoke(new Action(() => { MainW.MsgGrid.Visibility = Visibility.Visible; }), null);
        }
        public void HideMsg()
        {
            AyThread.Instance.RunNew(() =>
            {
                AyThread.Instance.RunUI(() =>
                {
                    MainW.MsgGrid.Visibility = Visibility.Hidden;
                });
            });
        }
    }
}
