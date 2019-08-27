namespace LuMei.Model
{
    public class SoftModel:Set
    {
        /// <summary>
        /// 上次更新时间
        /// </summary>
        public string UpdateTime { get; set; }
        /// <summary>
        /// 更新地址
        /// </summary>
        public string UpdateUrl { get; set; }
        /// <summary>
        /// 当前版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 官方文件解压文件夹
        /// </summary>
        public string Extraction { get; set; }
        /// <summary>
        /// 软件主题
        /// </summary>
        public string SoftTheme { get; set; }
    }
}
