using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LuMei.Model;

namespace LuMei.Helper
{
    public class Ini
    {
        public static Dictionary<string, string> Read(string path)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] array = File.ReadAllLines(path);
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text = array2[i];
                if (text.Length > 3 && text.Contains("="))
                {
                    try
                    {
                        int num = text.IndexOf('=');
                        string key = text.Substring(0, num).Trim();
                        string value = text.Substring(num + 1, text.Length - (num + 1)).Trim();
                        dictionary.Add(key, value);
                    }
                    catch (Exception ex)
                    {
                        Log.LogError("配置读取出错", ex);
                    }
                }
            }
            return dictionary;
        }
        public static void Write(string path, Dictionary<string, string> dict)
        {
            var array = new string[dict.Count];
            var num = 0;
            foreach (var current in dict)
            {
                array[num] = string.Format("{0}={1}", current.Key, current.Value);
                num++;
            }
            File.WriteAllLines(path, array);
        }

        public static Set ReadSet()
        {
            var set = new Set() { State = true };
            try
            {
                var cfg = Read(Soft.IniPath);
                if (cfg.ContainsKey("Version"))
                {
                    set.Version = cfg["Version"];
                }
                if (cfg.ContainsKey("GamePath"))
                {
                    set.GamePath = cfg["GamePath"];
                }
                if (cfg.ContainsKey("UpdateTime"))
                {
                    set.UpdateTime = cfg["UpdateTime"];
                }
                if (cfg.ContainsKey("Extraction"))
                {
                    set.Extraction = cfg["Extraction"];
                }
            }
            catch
            {
                set.State = false;
                set.Title = "配置文件读取出错！";
                set.Message = "即将重建配置文件！";
            }
            return set;
        }
        /// <summary>
        /// 读取INI文件的指定键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadSet(string key)
        {
            var cfg = Read(Soft.IniPath);
            if (cfg.ContainsKey(key))
                return cfg[key];
            else
                return null;
        }
        public static Set WriteSet(Set set)
        {
            var main = new FileInfo(Soft.MainExe);
            var info = FileVersionInfo.GetVersionInfo(Soft.MainExe);
            if (string.IsNullOrWhiteSpace(set.UpdateTime))
                set.UpdateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", main.CreationTime);

            if (string.IsNullOrWhiteSpace(set.Version) || set.Version != info.FileVersion)
                set.Version = info.FileVersion;
            try
            {
                var dictionary = new Dictionary<string, string>();
                dictionary.Add("GamePath", set.GamePath);
                dictionary.Add("UpdateTime", set.UpdateTime);
                dictionary.Add("Version", set.Version);
                dictionary.Add("UpdateUrl", set.UpdateUrl ?? "http://www.lolskin.cc/update.php");
                dictionary.Add("Extraction", set.Extraction ?? Soft.ZipTemp);
                Write(Soft.IniPath, dictionary);
                set.State = true;
            }
            catch (Exception ex)
            {
                set.State = false;
                Log.LogError("配置文件保存出错！", ex);
            }
            return set;
        }
    }
}
