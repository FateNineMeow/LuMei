using LuMei.FileLibrary.BinFile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace LuMei.TestForm
{
    /// <summary>
    /// BinWinM.xaml 的交互逻辑
    /// </summary>
    public partial class BinWinM : Window
    {
        BinFile binFile;
        List<BinFileValue> BinFileList = new List<BinFileValue>();
        public BinWinM()
        {
            InitializeComponent();
        }
        private void BinWin_Loaded(object sender, RoutedEventArgs e)
        {

            this.binFile = new BinFile(@"F:\Project\Code\LuMei\代码\LuMei.TestForm\Katarina.bin");
           // var list = GetFileString(binFile.Entries);
              var list = new ObservableCollection<BinFileValue>() { new BinFileValue() { Value = "100" } };
           // var heros = new ObservableCollection<BinFileValue>(list);
            datagrid.DataContext = list;
            listview.ItemsSource = list;
            //datagrid.Dispatcher.BeginInvoke(new Action(() => { datagrid.ItemsSource = list; listview.ItemsSource = list; }), null);
        }
        private List<string> GetFileString(List<BinFileEntry> entrys)
        {
            var list = new List<string>();
            var slist = new List<string>();
            foreach (var item in entrys)
            {
                slist = (from c in item.Values select c.Value.ToString()).ToList();
                list.AddRange(slist);
            }
            //   MessageBox.Show(list.Count.ToString());
            return list;
        }
        private List<BinFileValue> GetFileViue(List<BinFileEntry> entrys)
        {
            var list = new List<BinFileValue>();
            foreach (var item in entrys)
            {
                list.AddRange(item.Values);
            }
            //   MessageBox.Show(list.Count.ToString());
            return list;
        }
    }
}


