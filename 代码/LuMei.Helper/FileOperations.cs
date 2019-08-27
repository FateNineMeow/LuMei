using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LuMei.Helper
{
    public class FileOperations
    {
        /// <summary>
        /// 获取路径下的所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> AllFiles(string path)
        {
            if (!Directory.Exists(path))
                return new List<string>();
            return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList();
        }
        /// <summary>
        /// 依据文件路径创建文件夹
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static bool CreateFileDir(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath)) return false;
            var dirpath = Path.GetDirectoryName(filepath);
            try
            {
                if (!Directory.Exists(dirpath))
                    Directory.CreateDirectory(dirpath);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "文件夹创建失败！");
                return false;
            }

        }
        /// <summary>
        /// 新建文件夹
        /// </summary>
        /// <param name="dirpath"></param>
        public static bool CreateDir(string dirpath)
        {
            try
            {
                if (!Directory.Exists(dirpath))
                    Directory.CreateDirectory(dirpath);
                return true;
            }
            catch
            {
                MessageBox.Show("文件夹创建失败！！\r\n请确认软件拥有足够权限\r\n并且文件未被占用！", "文件夹操作失败！");
                return false;
            }
        }
        /// <summary>
        /// 删除并重建文件夹
        /// </summary>
        /// <param name="dirpath"></param>
        public static bool CreateDirWithDelete(string dirpath)
        {
            try
            {
                if (Directory.Exists(dirpath))
                    Directory.Delete(dirpath, true);
                Directory.CreateDirectory(dirpath);
                return true;
            }
            catch
            {
                MessageBox.Show("文件夹删除失败！！\r\n请确认软件拥有足够权限\r\n并且文件未被占用！", "文件夹操作失败！");
                return false;
            }
        }
        /// <summary>
        /// 检查文件类型
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FileType GetFileType(string filePath)//filePath是文件的完整路径   
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader reader = new BinaryReader(fs);
                    string fileClass;
                    byte buffer;
                    byte[] b = new byte[2];
                    buffer = reader.ReadByte();
                    b[0] = buffer;
                    fileClass = buffer.ToString();
                    buffer = reader.ReadByte();
                    b[1] = buffer;
                    fileClass += buffer.ToString();
                    reader.Close();
                    fs.Close();
                    switch (fileClass)
                    {
                        case "255216"://JPG
                        case "7173"://GIF
                        case "6677"://BMP
                        case "13780"://PNG
                            return FileType.Picture;
                        case "6868":
                            return FileType.Texture;
                        case "210187":
                            return FileType.Other;
                        default:
                            return FileType.Other;
                    };

                }
            }
            catch
            {
                return FileType.None;
            }
        }
        public static string NewFilePath(string file)
        {
            var ex = Path.GetExtension(file);
            var newfile = Path.ChangeExtension(file, ".new" + ex);
            return newfile;
        }
        public enum FileType
        {
            None,
            Other,
            Texture,
            Picture,
            SkinFile
        }
        public enum FileExtension
        {
            Dds = 6868,
            Jpg = 255216,
            Gif = 7173,
            Bmp = 6677,
            Png = 13780,
            Com = 7790,
            Exe = 7790,
            Dll = 7790,
            Rar = 8297,
            Zip = 8075,
            Xml = 6063,
            Html = 6033,
            Aspx = 239187,
            Cs = 117115,
            Js = 119105,
            Txt = 210187,
            Sql = 255254,
            Bat = 64101,
            Btseed = 10056,
            Rdp = 255254,
            Psd = 5666,
            Pdf = 3780,
            Chm = 7384,
            Log = 70105,
            Reg = 8269,
            Hlp = 6395,
            Doc = 208207,
            Xls = 208207,
            Docx = 208207,
            Xlsx = 208207
        }
    }
}
