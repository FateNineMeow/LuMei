using System.Collections.Generic;
using System.ComponentModel;

namespace LuMei.Model
{
    public class ImportModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        /// <summary>
        /// 皮肤文件
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 临时文件夹
        /// </summary>
        public string TempPath { get; set; }
        /// <summary>
        /// 临时文件夹内的文件
        /// </summary>
        public List<string> TempPathFiles { get; set; }
        /// <summary>
        /// 压缩包内的文件
        /// </summary>
        public List<string> ZipFiles { get; set; }
        /// <summary>
        /// 整理好的皮肤文件夹
        /// </summary>
        public string TempZipDir { get; set; }
        public int State { get; set; }
        /// <summary>
        /// 临时压缩包地址
        /// </summary>
        public string TempZip { get; set; }
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
        public ImportModel()
        {
            Skin = new Skin();
            State = 1;
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


