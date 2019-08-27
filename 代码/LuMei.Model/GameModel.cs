namespace LuMei.Model
{
    public class GameModel:Set
    {
        /// <summary>
        /// 游戏路径
        /// </summary>
        public string GamePath { get; set; }
        /// <summary>
        /// 上次更新时间
        /// </summary>
        public string UpdateTime { get; set; }
        /// <summary>
        /// 当前版本号
        /// </summary>
        public string Version { get; set; }
    }
}
