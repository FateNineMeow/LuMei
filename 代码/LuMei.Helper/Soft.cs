using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LuMei.Model;

namespace LuMei.Helper
{
    /// <summary>
    /// 软件的各种目录
    /// </summary>
    public static class Soft
    {
        /// <summary>
        /// 软件目录
        /// </summary>
        public static readonly string SoftPath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 数据文件夹
        /// </summary>
        public static readonly string Data = AppDomain.CurrentDomain.BaseDirectory + "Data\\";
        /// <summary>
        /// 日志文件夹
        /// </summary>
        public static readonly string LogInfo = AppDomain.CurrentDomain.BaseDirectory + "Data\\Log\\LogInfo\\";
        /// <summary>
        /// 日志文件夹
        /// </summary>
        public static readonly string LogError = AppDomain.CurrentDomain.BaseDirectory + "Data\\Log\\LogError\\";
        /// <summary>
        /// 解压用临时文件夹（ZipTemp\\）
        /// </summary>
        public static readonly string ZipTemp = AppDomain.CurrentDomain.BaseDirectory + "Temp\\UnZipTemp\\";
        /// <summary>
        /// 解压用临时文件夹（ZipTemp）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ZipTempPath(string name)
        {
            return ZipTemp + name + "\\";
        }
        /// <summary>
        /// 临时文件夹（Temp\\）
        /// </summary>
        public static readonly string Temp = AppDomain.CurrentDomain.BaseDirectory + "Temp\\";
        /// <summary>
        /// LuMei.txt所在文件夹（UnZip\\）
        /// </summary>
        public static readonly string LuMeiUnZip = AppDomain.CurrentDomain.BaseDirectory + "Temp\\UnZip\\";
        /// <summary>
        /// LuMei.txt
        /// </summary>
        public static readonly string LuMeitxt = AppDomain.CurrentDomain.BaseDirectory + "Temp\\UnZip\\LuMei.txt";
        /// <summary>
        /// 配置文件路径（config.ini）
        /// </summary>
        public static readonly string IniPath = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
        /// <summary>
        /// 配置文件路径（config.cfg）
        /// </summary>
        public static readonly string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "config.cfg";
        /// <summary>
        /// 数据库文件
        /// </summary>
        public static readonly string DbPath = AppDomain.CurrentDomain.BaseDirectory + "skin.db3";
        /// <summary>
        /// 自动更新程序
        /// </summary>
        public static readonly string UpdateExe = AppDomain.CurrentDomain.BaseDirectory + "autoupdate.exe";
        /// <summary>
        /// 主程序
        /// </summary>
        public static readonly string MainExe = Application.ExecutablePath;
        /// <summary>
        /// 程序版本号
        /// </summary>
        /// <returns></returns>
        public static Version MainVersion()
        {
            var info = FileVersionInfo.GetVersionInfo(MainExe);
            return new Version(info.FileVersion);
        }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static readonly string ConnectionString = string.Format("Data Source={0}\\skin.db3;Version=3;", Environment.CurrentDirectory);

        /// <summary>
        /// 创建数据库
        /// </summary>
        public static void CreateDb()
        {
            if (!File.Exists(DbPath))
                SQLiteConnection.CreateFile(DbPath);

        }
        public static void CreateTable()
        {
            SQLiteConnection dbConn;
            dbConn = new SQLiteConnection(ConnectionString);
            dbConn.Open();
            string sql = "CREATE TABLE Skin (ID  integer PRIMARY KEY autoincrement,SkinName nvarchar(64),Author nvarchar(64),LoadPic ntext,Original ntext,BackImage ntext,Hero  nvarchar(64),MountType nvarchar(64),CreateTime nvarchar(64),FileName ntext,Comment ntext);";
            var command = new SQLiteCommand(sql, dbConn);
            command.ExecuteNonQuery();
            dbConn.Close();
        }
        public static List<string> ZipList()
        {
            var list = Zip.GetZipFilesName(Game.ZipContentsPath);
            return list.Select(item => Config.GameSet("GamePath") + "Game\\" + item.Replace("txt", "zip")).ToList();
        }

        public static string AppXmlPath = Data + "application.xml";
        public static string LogoIcoPath = AppDomain.CurrentDomain.BaseDirectory + "Data\\back.jpg";

        #region  皮肤文件夹
        /// <summary>
        /// 皮肤文件夹（Skins\\）
        /// </summary>
        public static readonly string Skins = AppDomain.CurrentDomain.BaseDirectory + "Data\\Skins\\";
        /// <summary>
        /// 皮肤数据文件夹
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public static string SkinPath(Skin skin)
        {
            return string.Format("{0}{1}-{2}\\", Skins, skin.Hero, skin.SkinName);
        }
        public static string SkinOldPath(Skin skin)
        {
            return string.Format("{0}Skins\\Skins\\{1}-{2}\\", AppDomain.CurrentDomain.BaseDirectory, skin.Hero, skin.SkinName);
        }
        /// <summary>
        /// 软件目录下的皮肤包
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public static string SkinFile(Skin skin)
        {
            return string.Format("{0}{1}-{2}\\{3}", Skins, skin.Hero, skin.SkinName, skin.FileName);
        }
        /// <summary>
        /// 软件目录下的皮肤包
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public static string SkinConfig(Skin skin)
        {
            return string.Format("{0}{1}-{2}\\skin.cfg", Skins, skin.Hero, skin.SkinName);
        }
        /// <summary>
        /// 皮肤载入图地址
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public static string SkinLoadPath(Skin skin)
        {
            return string.Format("{0}{1}-{2}\\{3}", Skins, skin.Hero, skin.SkinName, skin.LoadPic);
        }
        /// <summary>
        /// 皮肤原画地址
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public static string SkinOriginalPath(Skin skin)
        {
            return string.Format("{0}{1}-{2}\\{3}", Skins, skin.Hero, skin.SkinName, skin.Original);
        }
        /// <summary>
        /// 皮肤预览图地址
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public static string SkinBackImagePath(Skin skin)
        {
            return string.Format("{0}{1}-{2}\\{3}", Skins, skin.Hero, skin.SkinName, skin.BackImage);
        }
        #endregion
    }
}

