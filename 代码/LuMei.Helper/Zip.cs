using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace LuMei.Helper
{
    /// <summary>
    /// UnZipFloClass 的摘要说明
    /// </summary>
    public static class Zip
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="strFile">压缩目录</param>
        /// <param name="strZip">目标压缩包</param>
        public static void ZipFile(string strFile, string strZip)
        {            
            using (ZipOutputStream s = new ZipOutputStream(File.Create(strZip)))
            {
                if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
                    strFile += Path.DirectorySeparatorChar;
                s.SetLevel(6); // 0 - store only to 9 - means best compression
                zip(strFile, s, strFile);
                s.Finish();
                s.Close();
            }
        }
        private static void zip(string strFile, ZipOutputStream s, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar) strFile += Path.DirectorySeparatorChar;
            Crc32 crc = new Crc32();
            string[] filenames = Directory.GetFileSystemEntries(strFile);
            foreach (string file in filenames)
            {
                if (Directory.Exists(file))                
                    zip(file, s, staticFile);                
                else // 否则直接压缩文件
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(file);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string tempfile = file.Substring(staticFile.LastIndexOf('\\') + 1);
                    ZipEntry entry = new ZipEntry(tempfile) { DateTime = DateTime.Now, Size = fs.Length };
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                }
            }
        }
        public static void ZipAddFile(string file, string zipfile, string filename)
        {
            using (ZipFile zip = new ZipFile(zipfile))
            {
                zip.BeginUpdate();
                var entry = new ZipEntry(file) { DateTime = DateTime.Now, Size = file.Length };
                zip.Add(entry);
                zip.CommitUpdate();
            }
        }
        /// <summary>
        /// 解压压缩包（保持目录结构）
        /// </summary>
        /// <param name="targetFile">压缩包</param>
        /// <param name="fileDir">解压到</param>
        /// <returns></returns>
        public static string UnZipFile(string targetFile, string fileDir)
        {
            string rootFile = " ";
            try
            {
                //读取压缩文件(zip文件)，准备解压缩
                ZipInputStream s = new ZipInputStream(File.OpenRead(targetFile.Trim()));
                ZipEntry theEntry;
                string path = fileDir;
                //解压出来的文件保存的路径
                string rootDir = " ";
                //根目录下的第一个子文件夹的名称
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    rootDir = Path.GetDirectoryName(theEntry.Name);
                    //得到根目录下的第一级子文件夹的名称
                    if (rootDir.IndexOf('\\') >= 0)
                    {
                        rootDir = rootDir.Substring(0, rootDir.IndexOf('\\') + 1);
                    }
                    string dir = Path.GetDirectoryName(theEntry.Name);
                    //根目录下的第一级子文件夹的下的文件夹的名称
                    string fileName = Path.GetFileName(theEntry.Name);
                    //根目录下的文件名称
                    if (dir != " ")
                    //创建根目录下的子文件夹,不限制级别
                    {
                        if (!Directory.Exists(string.Format("{0}\\{1}", fileDir, dir)))
                        {
                            path = string.Format("{0}\\{1}", fileDir, dir);
                            //在指定的路径创建文件夹
                            Directory.CreateDirectory(path);
                        }
                    }
                    else if (dir == " " && fileName != "")
                    //根目录下的文件
                    {
                        path = fileDir;
                        rootFile = fileName;
                    }
                    else if (dir != " " && fileName != "")
                    //根目录下的第一级子文件夹下的文件
                    {
                        if (dir.IndexOf('\\') > 0)
                        //指定文件保存的路径
                        {
                            path = string.Format("{0}\\{1}", fileDir, dir);
                        }
                    }
                    if (dir == rootDir)
                    //判断是不是需要保存在根目录下的文件
                    {
                        path = string.Format("{0}\\{1}", fileDir, rootDir);
                    }
                    //以下为解压缩zip文件的基本步骤
                    //基本思路就是遍历压缩文件里的所有文件，创建一个相同的文件。
                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(string.Format("{0}\\{1}", path, fileName));
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Close();
                    }
                }
                s.Close();
                return rootFile;
            }
            catch (Exception ex)
            {
                return "1; " + ex.Message;
            }
        }
        /// <summary>
        /// 解压压缩包(解压到同一文件夹)
        /// </summary>
        /// <param name="targetFile">要解压压缩包</param>
        /// <param name="keywords">要解压的文件关键字</param>
        /// <param name="outpath">解压到的地址</param>
        public static void UnZipFile(string targetFile, string fileDir, string keywords)
        {
            if (!File.Exists(targetFile))
                return;
            if (string.IsNullOrWhiteSpace(keywords))
                return;
            keywords = keywords.ToLower().Trim();
            byte[] data = new byte[4096];
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(targetFile)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(theEntry.Name) && theEntry.Name.ToLower().Contains(keywords))
                    {
                        if (theEntry.Name.ToLower().Contains(keywords))
                        {
                            var dirname = fileDir + Path.GetDirectoryName(theEntry.Name);
                            if (!Directory.Exists(dirname))
                                Directory.CreateDirectory(dirname);
                            string fileName = Path.GetFileName(theEntry.Name);
                            if (fileName != String.Empty)
                            {
                                using (FileStream streamWriter = File.Create(fileDir + theEntry.Name))
                                {
                                    int size = 2048;
                                    byte[] data2 = new byte[2048];
                                    while (true)
                                    {
                                        size = s.Read(data2, 0, data2.Length);
                                        if (size > 0)
                                            streamWriter.Write(data2, 0, size);
                                        else
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                s.Close();
            }
        }
        /// <summary>
        /// 解压多个文件（非解压全部）
        /// </summary>
        /// <param name="targetFile"></param>
        /// <param name="fileDir"></param>
        /// <param name="filenames"></param>
        public static void UnzipFiles(string targetFile, string fileDir, List<string> filenames)
        {
            if (!File.Exists(targetFile))
                return;
            if (filenames.Count < 1)
            {
                UnZipFile(targetFile, fileDir);
                return;
            }
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(targetFile)))
            {
                byte[] data = new byte[4096];
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (string.IsNullOrWhiteSpace(theEntry.Name)) continue;
                    if (filenames.All(d => d.ToLower().Trim() != theEntry.Name.ToLower().Trim())) continue;
                    if (!Directory.Exists(fileDir))
                        Directory.CreateDirectory(fileDir);
                    string fileName = Path.GetFileName(theEntry.Name);
                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(fileDir + fileName))
                        {
                            int size = 2048;
                            byte[] data2 = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data2, 0, data2.Length);
                                if (size > 0)
                                    streamWriter.Write(data2, 0, size);
                                else
                                    break;
                            }
                        }
                    }
                }
                s.Close();
            }
        }
        public static string UnZipOneFile(string zipfile, string entryfile)
        {
            if (GetZipFilesName(zipfile).Any(d => d == entryfile))
            {
                var fileDir = Path.GetDirectoryName(zipfile);
                var filename = fileDir + entryfile;
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipfile)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(theEntry.Name)) continue;
                        if (entryfile.ToLower() != theEntry.Name.ToLower().Trim()) continue;
                        using (FileStream streamWriter = File.Create(filename))
                        {
                            int size = 2048;
                            byte[] data2 = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data2, 0, data2.Length);
                                if (size > 0)
                                    streamWriter.Write(data2, 0, size);
                                else
                                    break;
                            }
                        }

                    }
                    s.Close();
                }
                return filename;
            }
            return null;
        }
        /// <summary>
        /// 读取压缩包注释
        /// </summary>
        /// <param name="zipfile"></param>
        /// <returns></returns>
        public static string GetZipInfo(string zipfile)
        {
            var str = "";
            try
            {
                using (ZipFile s = new ZipFile(zipfile))
                {
                    str = s.ZipFileComment;
                }
            }
            catch (Exception ex)
            {
                Log.LogError("获取皮肤包信息", ex);
            }
            return str;

        }
        public static ZipNote GetZipNote(string zipfile)
        {
            var zip = new ZipNote { State = 0 };
            try
            {
                using (ZipFile s = new ZipFile(zipfile))
                {
                    var str = s.ZipFileComment;
                    str = str.Replace("\r\n", ",");
                    var list = str.Split(',');
                    if (list.Length < 1)
                        return null;
                    var skinname = list[0];
                    if (skinname != null)
                    {
                        if (skinname.Contains("名称："))
                            skinname = skinname.Replace("名称：", "");
                        zip.SkinName = skinname;
                    }
                    if (list.Length < 2)
                        return zip;
                    var hero = list[1];
                    if (hero != null)
                    {
                        if (hero.Contains("英雄："))
                            hero = hero.Replace("英雄：", "");
                        zip.HeroName = hero;
                    }

                    if (list.Length < 3)
                        return zip;
                    var author = list[2];
                    if (author != null)
                    {
                        if (author.Contains("作者："))
                            author = author.Replace("作者：", "");
                        zip.Author = author;
                    }

                    if (list.Length < 4)
                        return zip;
                    var ver = list[3];
                    if (ver != null)
                    {
                        if (ver.Contains("版本号："))
                            ver = ver.Replace("版本号：", "");
                        zip.GameVersion = ver;
                    }

                    if (list.Length < 5)
                        return zip;
                    var comment = list[4];
                    if (comment != null)
                    {
                        if (comment.Contains("备注："))
                            comment = comment.Replace("备注：", "");
                        zip.Comment = comment;
                    }
                    zip.State = 1;
                }
            }
            catch (Exception ex)
            {
                Log.LogError("获取皮肤包信息", ex);
                return zip;
            }
            return zip;

        }
        public static bool SetZipNote(string zipfile, ZipNote model)
        {
            var comment = "名称：" + model.SkinName + "\r\n英雄：" + model.HeroName + "\r\n作者：" + model.Author + "\r\n版本号：" + model.GameVersion + "\r\n备注：" + model.Comment;
            return SetZipInfo(zipfile, comment);
        }
        /// <summary>
        /// 压缩包添加注释
        /// </summary>
        /// <param name="zipfile"></param>
        /// <param name="comment"></param>
        public static bool SetZipInfo(string zipfile, string comment)
        {
            try
            {
                using (ZipFile s = new ZipFile(zipfile))
                {
                    s.BeginUpdate();
                    s.SetComment(comment);
                    s.CommitUpdate();
                    s.AbortUpdate();
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.LogError("获取皮肤包信息", ex);
                return false;
            }
        }
        /// <summary>
        /// 读取压缩包文件(不解压)
        /// </summary>
        /// <param name="targetFile">要解压压缩包</param>
        /// <param name="keywords">要解压的文件关键字</param>
        /// <param name="outpath">解压到的地址</param>
        public static List<string> GetZipFilesName(string targetFile)
        {
            if (!File.Exists(targetFile))
                return null;
            var list = new List<string>();
            using (ZipFile zip = new ZipFile(targetFile))
            {
                foreach (ZipEntry entry in zip)
                {
                    if (!string.IsNullOrWhiteSpace(entry.Name))
                        list.Add(entry.Name);
                }
                zip.Close();
            }
            return list;
        }
    }
    public class ZipNote
    {
        public string SkinName { get; set; }
        public string HeroName { get; set; }
        public string Author { get; set; }
        public string GameVersion { get; set; }
        public string Comment { get; set; }
        public string CreateTime { get; set; }
        public int State { get; set; }
    }
}
