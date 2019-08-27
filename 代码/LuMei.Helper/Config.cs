using System;
using System.Collections.Generic;
using System.IO;
using LuMei.Model;
using SharpConfig;

namespace LuMei.Helper
{
    /// <summary>
    /// 配置文件操作类
    /// </summary>
    public static class Config
    {
        #region 基础方法
        static Configuration _config;
        public static string SkinConfigSave(Skin skin)
        {
            try
            {
                var file = Soft.SkinConfig(skin);
                var config = new Configuration();
                config["Skin"]["Hero"].SetValue(skin.Hero);
                config["Skin"]["SkinName"].SetValue(skin.SkinName);
                config["Skin"]["FileName"].SetValue(skin.SkinName + ".zip");
                config["Skin"]["Author"].SetValue(skin.Author);
                config["Skin"]["Comment"].SetValue(skin.Comment);
                config.SaveToFile(file);
                return file;
            }
            catch
            {
                return null;
            }
        }
        public static Skin SkinConfigGet(string filepath)
        {
            var config = Configuration.LoadFromFile(filepath);
            try
            {
                var skin = new Skin();
                config["Skin"].MapTo(skin);
                return skin;
            }
            catch
            {
                return null;
            }
        }
        public static void CheckConfig()
        {
            if (!File.Exists(Soft.ConfigPath))
                File.WriteAllBytes(Soft.ConfigPath, HelperResource.Config);
            _config = Configuration.LoadFromFile(Soft.ConfigPath);
        }
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="sectionName">节名称（插件名称）</param>
        /// <param name="configName">参数名称</param>
        /// <returns></returns>
        public static object GetObject(string sectionName, string configName)
        {
            try
            {
                CheckConfig();
                return _config[sectionName][configName].GetValue<object>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="sectionName">节名称（插件名称）</param>
        /// <param name="configName">参数名称</param>
        /// <returns></returns>
        public static string GetString(string sectionName, string configName)
        {
            try
            {
                CheckConfig();
                return _config[sectionName][configName].GetValue<string>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="sectionName">节名称（插件名称）</param>
        /// <param name="configName">参数名称</param>
        /// <returns></returns>
        public static int GetInt(string sectionName, string configName)
        {
            try
            {
                CheckConfig();
                return _config[sectionName][configName].GetValue<int>();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="sectionName">节名称（插件名称）</param>
        /// <param name="configName">参数名称</param>
        /// <returns></returns>
        public static bool GetBool(string sectionName, string configName)
        {
            try
            {
                CheckConfig();
                return _config[sectionName][configName].GetValue<bool>();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sectionName">节名称（插件名称）</param>
        /// <param name="dic">参数名称，值</param>
        /// <returns></returns>
        public static bool Save(string sectionName, Dictionary<String, String> dic)
        {
            foreach (var item in dic)
            {
                Save(sectionName, item.Key, item.Value);
            }
            return true;
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sectionName">节名称（插件名称）</param>
        /// <param name="dic">参数名称，值</param>
        /// <returns></returns>
        public static bool Save(string sectionName, Dictionary<String, int> dic)
        {
            foreach (var item in dic)
            {
                Save(sectionName, item.Key, item.Value);
            }
            return true;
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sectionName">节名称（插件名称）</param>
        /// <param name="configName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static bool Save(string sectionName, string configName, string value)
        {
            try
            {
                CheckConfig();
                _config[sectionName][configName].SetValue(value);
                _config.SaveToFile(Soft.ConfigPath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sectionName">节名称（插件名称）</param>
        /// <param name="configName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static bool Save(string sectionName, string configName, int value)
        {
            CheckConfig();
            try
            {
                CheckConfig();
                _config[sectionName][configName].SetValue(value);
                _config.SaveToFile(Soft.ConfigPath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        public static SoftModel SoftSet()
        {
            var soft = new SoftModel { State = true };
            try
            {
                soft.Version = SoftSet("Version");
                soft.UpdateTime = SoftSet("UpdateTime");
                soft.Extraction = SoftSet("Extraction");
                soft.UpdateUrl = SoftSet("UpdateUrl");
                if (string.IsNullOrWhiteSpace(soft.Version))
                {
                    SaveNewSoftSet();
                    soft = SoftSet();
                }

            }
            catch
            {
                soft.State = false;
                soft.Title = "配置文件读取出错！";
                soft.Message = "即将重建配置文件！";
            }
            return soft;
        }
        public static void SaveNewSoftSet()
        {
            SoftSave("Version", Soft.MainVersion().ToString());
            SoftSave("UpdateTime", string.Format("{0:yyyy-MM-dd}", DateTime.Now));
            SoftSave("Extraction", Soft.Temp + "Extraction\\");
            SoftSave("UpdateUrl", "http://www.lolskin.cc/update.php");
            SoftSave("SoftTheme", "Blue");
        }
        public static GameModel GameSet()
        {
            var game = new GameModel
            {
                State = true,
                Version = GameSet("Version"),
                GamePath = GameSet("GamePath")
            };


            if (string.IsNullOrWhiteSpace(game.GamePath))
            {
                game.State = false;
                game.Title = "配置文件读取出错！";
                game.Message = "即将重建配置文件！";
            }
            return game;

        }
        public static string SoftTheme()
        {
            var theme = SoftSet("SoftTheme");
            if (string.IsNullOrWhiteSpace(theme) || string.IsNullOrWhiteSpace(theme.Trim())) theme = "Blue";
            return theme;
        }
        public static string SoftSet(string key)
        {
            return GetString("Soft", key);
        }
        public static string GameSet(string key)
        {
            return GetString("Game", key);
        }
        public static bool SoftSave(string key, string value)
        {
            return Save("Soft", key, value);
        }
        public static bool GameSave(string key, string value)
        {
            return Save("Game", key, value);
        }
    }
}
