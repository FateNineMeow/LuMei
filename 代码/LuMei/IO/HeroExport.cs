using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using LuMei.Data;
using LuMei.Helper;
using LuMei.Model;
using Ay.Framework.WPF;

namespace LuMei.IO
{
    public class HeroExport
    {
        readonly SoftParam _softParam = SoftParam.GetInstance();
        readonly SkinService _skin = new SkinService();
        readonly ChampionsService _champion = new ChampionsService();
        bool _canGoOn = true;

        public HeroExport()
        {
            State = 1;
        }
        public void StartImport()
        {
            try
            {
                StartImport(HeroEnName);
            }
            catch (Exception ex)
            {
                //    _softParam.MainW.Dispatcher.BeginInvoke(new Action(() => { _softParam.MainW.ShowMessageAsync("皮肤导入失败！", ex.Message, MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings { DefaultButtonFocus = MessageDialogResult.Affirmative }); }), null);
            }
        }
        public void StartImport(string enname)
        {
            Skin = new Skin();
            Comment = "导入中……";
            if (string.IsNullOrWhiteSpace(enname))
                MessageBox.Show("英雄名不可为空");
            HeroEnName = enname;
            var files = Directory.GetFiles(Soft.LuMeiUnZip).Where(d => !d.Contains("lumei.txt", StringComparison.OrdinalIgnoreCase));
            foreach (var item in files)
            {
                var filename = Path.GetFileNameWithoutExtension(item);

                var zipfilelist = File.ReadAllLines(item, Encoding.Default).Where(d => d.Contains(HeroEnName, StringComparison.OrdinalIgnoreCase));

            }
        }
        /// <summary>
        /// 英雄名称（英文名）
        /// </summary>
        public string HeroEnName { get; set; }
        /// <summary>
        /// 临时文件夹
        /// </summary>
        public string TempPath { get; set; }
        /// <summary>
        /// 压缩包内的文件
        /// </summary>
        public List<string> ZipFiles { get; set; }
        public int State { get; set; }
        public Skin Skin { get; set; }
        private string _comment;
        /// <summary>
        /// 信息说明
        /// </summary>
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
        private float _percent;
        /// <summary>
        /// 进度条
        /// </summary>
        public float Percent
        {

            get
            {
                return _percent;
            }
            set
            {
                if (_percent != value)
                {
                    _percent = value;
                    OnPropertyChanged("Percent");
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
    public static class ObjEx
    {
        /// <summary>
        /// 比较字符串（忽略大小写）
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp= StringComparison.OrdinalIgnoreCase)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
