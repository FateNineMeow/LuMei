using System;
using System.IO;
using LuMei.Model;
using Microsoft.Win32;

namespace LuMei.Helper
{
    /// <summary>
    /// 游戏的各种目录
    /// </summary>
    public static class Game
    {
        /// <summary>
        /// 游戏根目录
        /// </summary>
        public static string GamePath = Config.GameSet("GamePath");
        /// <summary>
        /// Game\\ZipContents.zip
        /// </summary>
        public static string ZipContentsPath = GamePath + "Game\\ZipContents.zip";
        /// <summary>
        /// ClientZips.txt文件
        /// </summary>
        public static string ClientZipsPath = GamePath + "Game\\ClientZips.txt";
        /// <summary>
        /// Game\\
        /// </summary>
        public static string GameDir = GamePath + "Game\\";
        /// <summary>
        /// Game\\LuMei\\
        /// </summary>
        public static string SkinPath = GamePath + "Game\\LuMei\\";
        /// <summary>
        /// 客戶端数据库
        /// </summary>
        /// <returns></returns>
        public static string ConnectionString = "Data Source=" + GamePath + "Air\\assets\\data\\gameStats\\gameStats_zh_CN.sqlite;";
        /// <summary>
        /// 查找已挂载皮肤的皮肤包
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public static string SkinFile(Skin skin)
        {
            return string.Format("{0}{1}{2}", SkinPath, skin.Hero.ToLower(), "-", skin.Id, ".zip");
        }
        public static string OldMountSkin(Skin skin)
        {
            return "LuMei\\" + skin.FileName.ToLower();
        }
        public static string SkinInClientZipstxt(Skin skin)
        {
            return "LuMei\\" + skin.Hero.ToLower() + "-" + skin.Id + ".zip";
        }
        public static bool ReadReg()
        {
            var set = new Set();
            var appPath = Registry.CurrentUser.OpenSubKey(@"Software\Tencent\lol\");//获取注册表里面的值

            if (appPath == null)
                return false;
            try
            {
                var lolpath = appPath.GetValue("InstallPath") + "\\";//获取子键名为installpath的值。此为游戏安装路径。
                var clientPath = lolpath + "TCLS\\Client.exe";
                if (File.Exists(clientPath))
                    return Config.GameSave("GamePath", lolpath);
            }
            catch (Exception ex)
            {
                Log.LogInfo("获取注册表失败", ex.Message);
            }
            return false;
        }
        public static string GetGameVersion()
        {
            var gamepath = Config.GameSet("GamePath");
            return null;
        }
    }

}

