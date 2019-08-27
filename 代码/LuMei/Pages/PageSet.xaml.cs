using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LuMei.Helper;
using LuMei.IO;

namespace LuMei.Pages
{
    /// <summary>
    /// PageSet.xaml 的交互逻辑
    /// </summary>
    public partial class PageSet
    {
        readonly SoftParam _soft = SoftParam.GetInstance();
        public List<AccentColorMenuData> AccentColors { get; set; }
        public PageSet()
        {
            InitializeComponent();
            SetColorBox();
        }
        private void SetColorBox()
        {
            var theme = Config.SoftTheme();
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null && NavigationService.CanGoBack) NavigationService.GoBack();
            if (NavigationService != null) NavigationService.Navigate(_soft.HeroP ?? new PageHero());
        }

        private void FileDropIn(object sender, DragEventArgs e)
        {
            var argument = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (argument.Length > 0)
                _soft.FileDropIn(argument);
        }

        private void ToggleSwitch_IsCheckedChanged(object sender, EventArgs e)
        {

        }

        private void AppThemeChange(object sender, SelectionChangedEventArgs e)
        {
            if (ColorBox.SelectedItem == null) return;
            var item = (AccentColorMenuData)ColorBox.SelectedItem;
            _soft.SetTheme(item.Name);
        }
        public double GaoSi
        {
            get
            {
                return _soft.MainW.GaoSiRadius;
            }
            set
            {
                if (value != _soft.MainW.GaoSiRadius)
                    _soft.MainW.GaoSiRadius = value;
            }
        }

        private void GaoSiChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _soft.MainW.GaoSiRadius = gaoSi.Value;
        }
    }
}
