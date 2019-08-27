using Ay.Framework.WPF;
using Ay.Framework.WPF.Controls;
using LuMei.IO;
using LuMei.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LuMei
{
    /// <summary>
    /// HeroW.xaml 的交互逻辑
    /// </summary>
    public partial class DialogHero : AyPopupWindow
    {
        readonly ILuMeiClass _lumei = new LuMeiClass();
        readonly SoftParam _softParam = SoftParam.GetInstance();
        public string HeroName { get; set; }
        public DialogHero()
        {
            InitializeComponent();
            MinButtonVisibility = Visibility.Hidden;
            HeroList.DataContext = _lumei.AllHero();
            _softParam.SetBackImage(this, "");
        }

        private void Sure(object sender, RoutedEventArgs e)
        {
            if (HeroList.SelectedIndex == -1 || HeroList.SelectedItem == null)
            {
                AyMessageBox.ShowCus("Message", "Title");
                return;
            }
            var item = (Hero)HeroList.SelectedItem;
            HeroName = item.EnName;
            this.DialogResult = true;
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void HeroChange(object sender, SelectionChangedEventArgs e)
        {
            if (HeroList.SelectedIndex == -1)
                return;
            if (HeroList.SelectedItem == null) return;
            var item = (Hero)HeroList.SelectedItem;
            _softParam.SetBackImage(this, item.EnName);
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
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
    }
}
