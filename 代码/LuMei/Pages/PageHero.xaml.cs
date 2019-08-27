using System;
using System.Windows;
using System.Windows.Controls;
using LuMei.IO;
using LuMei.Model;
using Ay.Framework.WPF;

namespace LuMei.Pages
{
    /// <summary>
    /// PageHero.xaml 的交互逻辑
    /// </summary>
    public partial class PageHero
    {
        readonly ILuMeiClass _lumei = new LuMeiClass();
        readonly SoftParam _softParam = SoftParam.GetInstance();
        public PageHero()
        {
            InitializeComponent();
            HeroList.DataContext = _lumei.AllHero();
            _softParam.HeroP = this;
        }

        private void HeroTypeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var sen = (Button)sender;
                var typeid = Convert.ToInt32(sen.Tag.ToString());
                HeroList.Dispatcher.BeginInvoke(new Action(() => { HeroList.DataContext = _lumei.Hero(typeid); }), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void HeroListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HeroList.SelectedIndex == -1)
                return;
            if (HeroList.SelectedItem == null) return;
            var item = (Hero)HeroList.SelectedItem;
            if (NavigationService != null) NavigationService.Navigate(new PageSkin(item.EnName));
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
            HeroList.Dispatcher.BeginInvoke(new Action(() => { HeroList.DataContext = _lumei.Hero(8); }), null);
        }

        private void FileDropIn(object sender, DragEventArgs e)
        {
            var argument = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (argument.Length > 0)
                _softParam.FileDropIn(argument);
        }
        private void HeroNameChange(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(HeroName.Text)) return;
            if (string.IsNullOrEmpty(HeroName.Text.Trim())) return;
            HeroList.Dispatcher.BeginInvoke(new Action(() => { HeroList.DataContext = _lumei.Hero(HeroName.Text.Trim()); }), null);
        }
        public void UpdateHeroList()
        {
            HeroList.Dispatcher.BeginInvoke(new Action(() => { HeroList.DataContext = _lumei.AllHero(); }), null);

        }
    }
}
