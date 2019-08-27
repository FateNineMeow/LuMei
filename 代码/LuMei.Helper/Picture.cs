using System;
using System.Drawing;
using System.IO;
using System.Linq;
using LuMei.Model;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Reflection;

namespace LuMei.Helper
{
    /// <summary>
    /// 获取各种图片地址
    /// </summary>
    public static class Picture
    {
        public static string GamePath = Config.GameSet("GamePath");
        public static Image IamgeFromPath(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                int byteLength = (int)fileStream.Length;
                byte[] fileBytes = new byte[byteLength];
                fileStream.Read(fileBytes, 0, byteLength);
                //文件流关閉,文件解除锁定
                fileStream.Close();
                return Image.FromStream(new MemoryStream(fileBytes));
            }
        }
        /// <summary>
        /// air\\assets\\images\\
        /// </summary>
        public const string ImagePath = "air\\assets\\images\\";
        /// <summary>
        /// 皮肤载入图
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public static BitmapImage SkinLoad(Skin skin)
        {
            BitmapImage image;
            var path = Soft.SkinLoadPath(skin);
            if (File.Exists(path))
                image = BitmapLoad(path);
            else
                image = HeroLoad(skin.Hero);
            return image;
        }
        /// <summary>
        /// 皮肤载入图
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public static string SkinLoadPath(Skin skin)
        {
            var path = Soft.SkinLoadPath(skin);
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                path = HeroLoadPath(skin.Hero);
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                path = HeroLoadPath(skin.Hero);
            return path;
        }
        /// <summary>
        /// 皮肤原画
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public static BitmapImage SkinOriginal(Skin skin)
        {
            var path = Soft.SkinOriginalPath(skin);
            return File.Exists(path) ? BitmapOrigina(path) : SkinBackImage(skin);
        }
        /// <summary>
        /// 皮肤原画
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public static string SkinOriginalPath(Skin skin)
        {
            var path = Soft.SkinOriginalPath(skin);
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                path = SkinBackImagePath(skin);
            return path;
        }
        /// <summary>
        /// 皮肤预览图地址
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        private static string SkinBackImagePath(Skin skin)
        {
            var path = Soft.SkinBackImagePath(skin);
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                path = HeroOriginalPath(skin.Hero);
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                path = Soft.LogoIcoPath;
            if (path == null) CopyFile();
            return path;
        }
        /// <summary>
        /// 皮肤预览图地址
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        private static BitmapImage SkinBackImage(Skin skin)
        {
            var path = Soft.SkinBackImagePath(skin);
            return File.Exists(path) ? BitmapLoad(path) : HeroOriginal(skin.Hero);
        }
        /// <summary>
        /// 头像
        /// </summary>
        /// <param name="heroname"></param>
        /// <returns></returns>
        public static string HeroSquarePath(string heroname)
        {
            var path = string.Format("{0}{1}champions\\{2}_Square_0.png", GamePath, ImagePath, heroname);
            if (!File.Exists(path))
                path = string.Format("{0}{1}Tray_48.png", GamePath, ImagePath);
            return path;
        }
        /// <summary>
        /// 载入图
        /// </summary>
        /// <param name="heroname"></param>
        /// <returns></returns>
        public static BitmapImage HeroLoad(string heroname)
        {
            var path = string.Format("{0}{1}champions\\{2}_0.jpg", GamePath, ImagePath, heroname);
            return File.Exists(path) ? BitmapLoad(path) : BitmapLoad("/Resources/load.png");
        }
        /// <summary>
        /// 载入图
        /// </summary>
        /// <param name="heroname"></param>
        /// <returns></returns>
        public static string HeroLoadPath(string heroname)
        {
            var path = string.Format("{0}{1}champions\\{2}_0.jpg", Config.GameSet("GamePath"), ImagePath, heroname);
            return File.Exists(path) ? path : null;
        }
        /// <summary>
        /// 原画
        /// </summary>
        /// <param name="heroname"></param>
        /// <returns></returns>
        public static BitmapImage HeroOriginal(string heroname)
        {
            var path = string.Format("{0}{1}champions\\", GamePath, ImagePath);
            if (!Directory.Exists(path))
                return BitmapOrigina("/Resources/back.jpg");
            var files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories).ToList();
            files = files.Where(d => d.Contains(heroname + "_Splash_") && !d.Contains("Centered") && !d.Contains("Tile")).ToList();
            var ran = new Random();
            var i = 0;
            var max = files.Count() - 1;
            if (max > 0)
                i = ran.Next(0, max);
            path = string.Format("{0}{1}champions\\{2}_Splash_{3}.jpg", GamePath, ImagePath, heroname, i);
            return File.Exists(path) ? BitmapOrigina(path) : BitmapOrigina("/Resources/back.jpg");
        }
        /// <summary>
        /// 原画地址
        /// </summary>
        /// <param name="heroname"></param>
        /// <returns></returns>
        public static string HeroOriginalPath(string heroname)
        {
            var path = string.Format("{0}{1}champions\\", GamePath, ImagePath);
            if (!Directory.Exists(path))
                return null;
            var files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories).ToList();
            files = files.Where(d => d.Contains(heroname + "_Splash_") && !d.Contains("Centered") && !d.Contains("Tile")).ToList();
            if (!files.Any()) return null;
            var ran = new Random();
            var i = 0;
            var max = files.Count() - 1;
            if (max > 0)
                i = ran.Next(0, max);
            path = files[i];
            return File.Exists(path) ? path : null;
        }
        /// <summary>
        /// 载入图列表
        /// </summary>
        /// <param name="heroname"></param>
        /// <returns></returns>
        public static List<string> HeroOriginalPaths(string heroname)
        {
            var path = string.Format("{0}{1}champions\\", GamePath, ImagePath);
            if (!Directory.Exists(path))
                return null;
            var files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories).ToList();
            return files.Where(d => d.Contains(heroname + "_") && !d.Contains("_Splash_") && !d.Contains("Centered") && !d.Contains("Tile")).ToList();
        }
        public static BitmapImage BitmapOrigina(string path)
        {
            if (!File.Exists(path)) path = Soft.LogoIcoPath;
            if (!File.Exists(path)) CopyFile();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();//开始更新状态
                                    //指定BitmapImage的StreamSource为按指定路径打开的文件流
            bitmapImage.StreamSource = new FileStream(path, FileMode.Open, FileAccess.Read);
            bitmapImage.DecodePixelWidth = 1215;//设置图像的宽度
            bitmapImage.DecodePixelHeight = 717;//设置图像的高度

            //加载Image后以便立即释放流
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();//结束更新
                                  //清除流以避免在尝试删除图像时出现文件访问异常
            bitmapImage.StreamSource.Dispose();
            return bitmapImage;//返回BitmapImage
        }
        public static BitmapImage BitmapLoad(string path)
        {
            if (!File.Exists(path)) path = Soft.LogoIcoPath;
            if (!File.Exists(path)) CopyFile();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();//开始更新状态
                                    //指定BitmapImage的StreamSource为按指定路径打开的文件流
            bitmapImage.StreamSource = new FileStream(path, FileMode.Open, FileAccess.Read);
            bitmapImage.DecodePixelWidth = 308;//设置图像的宽度
            bitmapImage.DecodePixelHeight = 560;//设置图像的高度

            //加载Image后以便立即释放流
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();//结束更新
                                  //清除流以避免在尝试删除图像时出现文件访问异常
            bitmapImage.StreamSource.Dispose();
            return bitmapImage;//返回BitmapImage
        }
        public static void CopyFile()
        {
            try
            {
                if (!File.Exists(Soft.AppXmlPath))
                    using (FileStream fs = new FileStream(Soft.AppXmlPath, FileMode.Create, FileAccess.Write))
                    {
                        byte[] res = HelperResource.application;
                        fs.Write(res, 0, res.Length);
                        fs.Close();
                    };
                if (!File.Exists(Soft.LogoIcoPath))

                    using (var back = HelperResource.back)
                    {
                        back.Save(Soft.LogoIcoPath);
                    }

            }
            catch
            {
                throw new Exception("软件初始化失败！");
            }

        }
        public static void CopyStream(Stream i, Stream o)
        {
            byte[] b = new byte[32768];
            while (true)
            {
                int r = i.Read(b, 0, b.Length);
                if (r <= 0)
                    return;
                o.Write(b, 0, r);
            }
        }
    }

}

