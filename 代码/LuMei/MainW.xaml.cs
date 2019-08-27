using System;
using System.Windows;
using System.Windows.Input;
using LuMei.Helper;
using LuMei.IO;
using LuMei.Pages;
using Application = System.Windows.Application;
using Ay.Framework.WPF.Controls;
using Ay.Framework.WPF;
using System.IO;
using System.Windows.Forms;

namespace LuMei
{
    /// <summary>
    /// MainW.xaml 的交互逻辑
    /// </summary>
    public partial class MainW : AyWindow
    {
        readonly SoftParam _softParam = SoftParam.GetInstance();
        readonly private string GamePath = Config.GameSet("GamePath");
        readonly ILuMeiClass _lumei = new LuMeiClass();
        bool isShow = true;
        public MainW()
        {
            InitializeComponent();
            _softParam.MainW = this;
            try
            {
                GaoSiRadius = 0f;
                ayuiTaskBar.Visibility = Visibility.Hidden;
                SkinButtonVisibility = Visibility.Hidden;
                MaxButtonVisibility = Visibility.Hidden;
                Visibility = Visibility.Hidden;
                SplashWindow spw = new SplashWindow();
                spw.ShowDialog();
                if (spw.DialogResult != true)
                    Application.Current.Shutdown();
                _softParam.SetBackImage("");
                SetPage();
                Visibility = Visibility.Visible;
                ayuiTaskBar.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }


        }
        public void SetPage()
        {
            var page = new PageHero();
            _softParam.HeroP = page;
            HeroFrame.Content = page;
        }

        private void SetBackImage(object sender, RoutedEventArgs e)
        {
            _softParam.SetBackImage("");
        }
        private void OpenPageSet(object sender, RoutedEventArgs e)
        {
            if (HeroFrame.NavigationService != null) HeroFrame.NavigationService.Navigate(new PageSet());
        }

        #region 托盘菜单

        #region 用于控制窗体显隐
        private void ShowOrHideWindow(object sender, RoutedEventArgs e)
        {
            if (isShow)
                DoCloseWindow();
            else
                ShowWindow();
            isShow = !isShow;
        }
        public void ShowWindow()
        {
            if (CloseIsHideWindow)
            {
                if (_softParam.MainW == null)
                    _softParam.MainW = new MainW();
                _softParam.MainW.Show();
                _softParam.MainW.WindowState = WindowState.Normal;
                _softParam.MainW.Activate();
                _softParam.MainW.Topmost = true;
                _softParam.MainW.Topmost = false;
                _softParam.MainW.Focus();
            }
        }
        #endregion

        private void CloseWin(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void OpenAdvise(object sender, RoutedEventArgs e)
        {
            LuMeiClass.OpenUrl("http://www.lolskin.cc");
        }

        private void OpenLoLSkin(object sender, RoutedEventArgs e)
        {
            LuMeiClass.OpenUrl("http://www.lolskin.cc");
        }
        private void OpenClientZips(object sender, RoutedEventArgs e)
        {
            LuMeiClass.OpenFile(Game.ClientZipsPath);
        }

        private void OpenGameDic(object sender, RoutedEventArgs e)
        {
            LuMeiClass.OpenFile(Game.GameDir);
        }
        #endregion
        /// <summary>
        /// 界面可以拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void SknConverter(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog() { Title = "请选择Skn文件" };
            ofd.Filter = "请选择Skn文件(*.skn)|*.skn";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            ofd.Multiselect = false;
            ofd.ShowDialog();
            var sknfile = ofd.FileName;
            if (File.Exists(sknfile))
                _lumei.SknConverter(sknfile);
        }
        private void ReInstall(object sender, RoutedEventArgs e)
        {
            // if (NavigationService != null) NavigationService.Navigate(new PageSet());
            if (AyMessageBox.ShowCus("是否要重新挂载全部皮肤？", "重新挂载", "/Resources/logo.png", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _softParam.ReInstall();
        }

        private void AllUninstall(object sender, RoutedEventArgs e)
        {
            if (AyMessageBox.ShowCus("是否要卸载全部皮肤？", "卸载全部", "/Resources/logo.png", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            _softParam.AllUninstall();
        }

        private void AllSkinHero(object sender, RoutedEventArgs e)
        {
            if (_softParam.HeroP == null) _softParam.HeroP = new PageHero();
            _softParam.HeroP.HeroList.Dispatcher.BeginInvoke(new Action(() => { _softParam.HeroP.HeroList.DataContext = _lumei.Hero(8); }), null);
            HeroFrame.Content = _softParam.HeroP;
        }

        private void CreateLoadPic(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog() { Title = "请选择图片文件" };
            ofd.Filter = "请选择图片文件|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            ofd.Multiselect = false;
            ofd.ShowDialog();
            var sknfile = ofd.FileName;
            if (File.Exists(sknfile))
                new SkinImport().CreateLoadZip(sknfile);
        }
    }
}
