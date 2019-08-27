using Ay.Framework.WPF;
using LuMei.Helper;
using LuMei.IO;
using LuMei.Model;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace LuMei.Pages
{
    /// <summary>
    /// PageSkinInfo.xaml 的交互逻辑
    /// </summary>
    public partial class PageSkinInfo : Page, INotifyPropertyChanged
    {
        readonly SoftParam _soft = SoftParam.GetInstance();
        readonly ILuMeiClass _lumei = new LuMeiClass();
        Skin skin = new Skin();
        Skin old = new Skin();
        bool Changed = false;
        public PageSkinInfo()
        {
            InitializeComponent();
        }
        public PageSkinInfo(long? skinid)
        {
            InitializeComponent();
            skin = _lumei.GetSkin(skinid?.ToString());
            if (skin == null)
                NavigationService.Navigate(_soft.SkinP);
            old = skin;
            SkinLoadPic.Source = Picture.SkinLoad(skin);
            SkinInfo.Dispatcher.BeginInvoke(new Action(() => { SkinInfo.DataContext = skin; }), null);
        }
        private void BackSkin(object sender, System.Windows.RoutedEventArgs e)
        {

            if (Changed && AyMessageBox.Show("您已修改了本英雄信息，是否保存？", "已被修改", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                AyMessageBox.Show("OK", "OK");
            _soft.BackPageSkin();
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        private void ChooseHero(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogHero spw = new DialogHero();
            spw.ShowDialog();
            if (spw.DialogResult == true)
            {
                skin.Hero = spw.HeroName;
                Changed = true;
            }
        }
        private void SaveSkin()
        {
            if (skin.Hero != old.Hero && skin.MountType == "已挂载")
            {
                _lumei.SkinUnMount(old);
                _lumei.SkinMount(skin);
            }
            _lumei.SaveSkinModel(skin);
            _soft.BackPageSkin();
        }

        private void SaveSkinInfo(object sender, System.Windows.RoutedEventArgs e)
        {
            var s = skin;
            SaveSkin();
        }
    }
}
