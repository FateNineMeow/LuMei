using System;

namespace Ezhu.AutoUpdater.Lib
{
    /// <summary>
    /// 升级信息的具体包装
    /// </summary>
    [Serializable]
    public class UpdateInfo : UpdateModel
    {
        public Version UpdateVersion { get; set; }
        public Guid MD5
        {
            get;
            set;
        }
    }
}
