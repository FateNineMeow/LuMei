using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LuMei.FileLibrary.BinFile;
namespace LuMei.TestForm
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<ViewModels> _heroData = new ObservableCollection<ViewModels>();
        public MainWindow()
        {
            InitializeComponent();
            _heroData = GetAll();
            BinFile bin = new BinFile(@"F:\Project\Code\LuMei\代码\LuMei.TestForm\Katarina.bin");
            var i=bin.Entries.Capacity;
            //listaaaaa.Dispatcher.BeginInvoke(new Action(() => { listaaaaa.DataContext = _heroData; }), null);
            //MessageBox.Show(listaaaaa.Items.Count.ToString());
        }

        private ObservableCollection<ViewModels> GetAll()
        {
            var list = new ObservableCollection<ViewModels>();
            list.Add(new ViewModels { SkinName = "1", Comment = "111", SuccessRate = 0f, Id = 1 });
            //list.Add(new ViewModels() { SkinName = "2", Comment = "222", SuccessRate = 0, ID = 2 });
            //list.Add(new ViewModels() { SkinName = "3", Comment = "333", SuccessRate = 0, ID = 3 });
            //list.Add(new ViewModels() { SkinName = "4", Comment = "444", SuccessRate = 0, ID = 4 });
            //list.Add(new ViewModels() { SkinName = "5", Comment = "555", SuccessRate = 0, ID = 5 });
            //list.Add(new ViewModels() { SkinName = "6", Comment = "666", SuccessRate = 0, ID = 6 });
            //list.Add(new ViewModels() { SkinName = "7", Comment = "777", SuccessRate = 0, ID = 7 });
            return list;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //foreach (var item in _heroData)
            //{
            //    Thread t = new Thread(new ParameterizedThreadStart(SetValue));
            //    t.Start(item);

            //}
        }
        public void SetValue(object item)
        {
            var model = (ViewModels)item;
            while (model.SuccessRate < 100)
            {
                model.SuccessRate += model.Id;
                Thread.Sleep(300);
            }
        }
        private void ClickItem(object sender, SelectionChangedEventArgs e)
        {
            var item = (ViewModels)ParList.SelectedItem;
            if (item == null) return;
            MessageBox.Show(item.SuccessRate.ToString());
        }

        private void listaaaaa_MouseWheel(object sender, MouseWheelEventArgs e)
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
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }


    }
    public class ViewModels : INotifyPropertyChanged
    {
        readonly Object _obj = new object();
        public string Name { get; set; }
        public string SkinName { get; set; }
        public string Comment { get; set; }
        public int Id { get; set; }
        public float Rate;

        public float SuccessRate
        {

            get
            {
                return Rate;
            }
            set
            {
                lock (_obj)
                {
                    if (Rate != value)
                    {
                        Rate = value;
                        OnPropertyChanged("SuccessRate");
                    }
                }

            }
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
    }
}
