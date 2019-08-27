
using System;
using System.ComponentModel;

namespace LuMei.Model
{
    public class SkinModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Int64 Id { get; set; }
        /// <summary>
        /// 皮肤名称
        /// </summary>
        public string SkinName
        {
            get
            {
                return _skinname;
            }
            set
            {
                if (_skinname != value)
                {
                    _skinname = value;
                    OnPropertyChanged("SkinName");
                }
            }
        }
        private string _skinname;
        /// <summary>
        /// 对应英雄
        /// </summary>
        public string HeroName
        {
            get
            {
                return _heroname;
            }
            set
            {
                if (_heroname != value)
                {
                    _heroname = value;
                    OnPropertyChanged("HeroName");
                }
            }
        }
        private string _heroname;
        /// <summary> 
        /// 皮肤作者
        /// </summary>
        public string Author
        {
            get
            {
                return _author;
            }
            set
            {
                if (_author != value)
                {
                    _author = value;
                    OnPropertyChanged("Author");
                }
            }
        }
        private string _author;
        /// <summary>
        /// 挂载类型
        /// </summary>
        public bool MountType { get; set; }
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
        private string _comment;

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
