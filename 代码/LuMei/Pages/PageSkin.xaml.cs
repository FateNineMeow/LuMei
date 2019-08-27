using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LuMei.IO;
using LuMei.Model;
using Ay.Framework.WPF;
using LuMei.Helper;
using System.IO;

namespace LuMei.Pages
{
    /// <summary>
    /// PageSkin.xaml 的交互逻辑
    /// </summary>
    public partial class PageSkin
    {
        readonly ILuMeiClass _lumei = new LuMeiClass();
        readonly SoftParam _soft = SoftParam.GetInstance();
        private readonly string _heroName;
        public PageSkin()
        {
            InitializeComponent();
            SkinOperation.Visibility = Visibility.Hidden;
            SkinComment.Visibility = Visibility.Hidden;
        }
        public PageSkin(string heroname)
        {
            InitializeComponent();
            SkinOperation.Visibility = Visibility.Hidden;
            SkinComment.Visibility = Visibility.Hidden;
            _soft.SkinP = this;
            _heroName = heroname;
            SkinList.AddHandler(MouseWheelEvent, new MouseWheelEventHandler(SkinList_MouseWheel), true);
            _soft.SetBackImage(heroname);
            UpdateSkinList();
        }
        private void BackHero(object sender, RoutedEventArgs e)
        {
            if (NavigationService == null) return;
            if (_soft.HeroP == null) _soft.HeroP = new PageHero();
            _soft.HeroP.HeroList.SelectedIndex = -1;
            NavigationService.Navigate(_soft.HeroP);
        }
        private void SkinList_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ItemsControl items = (ItemsControl)sender;
            ScrollViewer scroll = FindVisualChild<ScrollViewer>(items);
            if (scroll != null)
            {
                int d = e.Delta;
                if (d > 0)
                    scroll.PageLeft();
                if (d < 0)
                    scroll.PageRight();
                scroll.ScrollToTop();
            }
        }
        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            if (obj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    var visualChild = child as T;
                    if (visualChild != null)
                    {
                        return visualChild;
                    }
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }
        private void SelectSkin(object sender, SelectionChangedEventArgs e)
        {
            if (SkinList.SelectedIndex < 0)
            {
                SkinOperation.Visibility = Visibility.Hidden;
                SkinComment.Visibility = Visibility.Hidden;
                return;
            }
            var skin = (Skin)SkinList.SelectedItem;
            _soft.SetBackImage(skin);
            var skinmodel = new SkinModel
            {
                Id = skin.Id,
                SkinName = skin.SkinName,
                HeroName = _lumei.EnNameToChName(skin.Hero),
                Author = !string.IsNullOrWhiteSpace(skin.Author) ? skin.Author : "未知",
                Comment = !string.IsNullOrWhiteSpace(skin.Comment) ? skin.Comment : "暂无简介！"
            };

            SkinOperation.Visibility = Visibility.Visible;
            SkinComment.Visibility = Visibility.Visible;
            if (skin.MountType == "已挂载")
            {
                BtnSkinMount.Visibility = Visibility.Hidden;
                BtnSkinUnMount.Visibility = Visibility.Visible;
            }
            else
            {
                BtnSkinMount.Visibility = Visibility.Visible;
                BtnSkinUnMount.Visibility = Visibility.Hidden;
            }
            SkinComment.Dispatcher.BeginInvoke(new Action(() => { SkinComment.DataContext = skinmodel; }), null);
            SkinOperation.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 挂载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkinMount(object sender, RoutedEventArgs e)
        {
            if (SkinList.SelectedIndex < 0) return;
            var skin = (Skin)SkinList.SelectedItem;
            _lumei.SkinMount(skin);
            UpdateSkinList();
        }
        /// <summary>
        /// 卸载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkinUnMount(object sender, RoutedEventArgs e)
        {
            if (SkinList.SelectedIndex < 0) return;
            var skin = (Skin)SkinList.SelectedItem;
            _lumei.SkinUnMount(skin);
            UpdateSkinList();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkinExport(object sender, RoutedEventArgs e)
        {
            if (SkinList.SelectedIndex < 0) return;
            var skin = (Skin)SkinList.SelectedItem;
            _lumei.SkinExport(skin);
            UpdateSkinList();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkinDelete(object sender, RoutedEventArgs e)
        {
            if (SkinList.SelectedIndex < 0) return;
            var skin = (Skin)SkinList.SelectedItem;
            if (AyMessageBox.ShowCus("确认要删除皮肤 " + skin.SkinName + " 吗？", "确认删除", "/Resources/logo.png", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            UpdateSkinList(skin);
            _soft.SetBackImage(skin.Hero);
            GC.Collect();
            _lumei.SkinDelete(skin);
        }

        private void FileDropIn(object sender, DragEventArgs e)
        {
            var argument = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (argument.Length > 0)
                _soft.FileDropIn(argument);
        }

        public void UpdateSkinList()
        {
            var list = !string.IsNullOrWhiteSpace(_heroName) ? _lumei.Skin(_heroName) : _lumei.AllSkin();
            SkinList.Dispatcher.BeginInvoke(new Action(() => { SkinList.DataContext = list; }), null);
        }
        private void UpdateSkinList(Skin skin)
        {
            var list = !string.IsNullOrWhiteSpace(_heroName) ? _lumei.Skin(_heroName, skin.Id) : new System.Collections.ObjectModel.ObservableCollection<Skin>();
            SkinList.Dispatcher.BeginInvoke(new Action(() => { SkinList.DataContext = list; }), null);
        }
        private void SkinInfo(object sender, RoutedEventArgs e)
        {
            if (SkinList.SelectedIndex < 0) return;
            var skin = (Skin)SkinList.SelectedItem;
            if (NavigationService != null) NavigationService.Navigate(new PageSkinInfo(skin?.Id));
        }

        private void HeroExport(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_heroName))
            {
                MessageBox.Show("未找到对应英雄");
                return;
            }
            var path = Config.SoftSet("Extraction") ?? (Soft.ZipTemp + "\\" + _heroName);
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            var list = Soft.ZipList();
            _soft.MainW.MsgGrid.Dispatcher.BeginInvoke(new Action(() => { _soft.MainW.MsgGrid.Visibility = Visibility.Visible; }), null);
            _soft.MainW.MsgText.Dispatcher.BeginInvoke(new Action(() => { _soft.MainW.MsgText.Text = $"开始导出{_heroName}的皮肤。"; }), null);
            foreach (var item in list)
            {
                Zip.UnZipFile(item, path, _heroName);
                _soft.MainW.MsgText.Dispatcher.BeginInvoke(new Action(() => { _soft.MainW.MsgText.Text = $"开始导出{_heroName}的皮肤。进度：{list.IndexOf(item)}/{list.Count}"; }), null);
            }
            System.Diagnostics.Process.Start("explorer.exe", path);

            _soft.MainW.MsgGrid.Dispatcher.BeginInvoke(new Action(() => { _soft.MainW.MsgGrid.Visibility = Visibility.Hidden; }), null);
            _soft.MainW.MsgText.Dispatcher.BeginInvoke(new Action(() => { _soft.MainW.MsgText.Visibility = Visibility.Visible; }), null);
        }
    }
}
