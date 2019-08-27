using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using LuMei.Data;
using LuMei.Helper;
using LuMei.Model;
using Ay.Framework.WPF;

namespace LuMei.IO
{
    public class SkinImport : IDisposable, INotifyPropertyChanged
    {
        readonly SoftParam _softParam = SoftParam.GetInstance();
        readonly SkinService _skin = new SkinService();
        readonly ChampionsService _champion = new ChampionsService();
        bool _canGoOn = true;

        public SkinImport()
        {
            State = 1;
        }
        public void StartImport()
        {
            try
            {
                StartImport(FilePath);
            }
            catch (Exception ex)
            {
                //    _softParam.MainW.Dispatcher.BeginInvoke(new Action(() => { _softParam.MainW.ShowMessageAsync("皮肤导入失败！", ex.Message, MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings { DefaultButtonFocus = MessageDialogResult.Affirmative }); }), null);
            }
        }
        public void StartImport(string filepath)
        {
            Skin = new Skin();
            Comment = "导入中……";
            if (string.IsNullOrWhiteSpace(filepath) || !File.Exists(filepath))
                MessageBox.Show("源文件不存在或找不到！");
            FilePath = filepath;
            获取皮肤信息();
            if (!_canGoOn) return;
            检查重复皮肤();
            if (!_canGoOn) return;
            查看压缩包结构();
            if (FileOperations.CreateDir(Soft.SkinPath(Skin)) && _canGoOn)
            {
                解压压缩包的图片();
                复制各种文件到皮肤文件夹();
                保存皮肤信息();
            }
            if (Directory.Exists(TempPath))
                try
                {
                    Directory.Delete(TempPath, true);
                }
                catch { throw; }

        }
        #region 导入皮肤文件

        private void 检查重复皮肤()
        {
            var skins = _skin.AllSkins();
            if (skins.Any(d => d.SkinName == Skin.SkinName && d.Hero == Skin.Hero))
            {
                switch (AyMessageBox.Show("已存在名字为 " + Skin.SkinName + " 的同名皮肤，是否覆盖保存？\r选择是：将覆盖旧皮肤保存\r选择否：将不覆盖旧皮肤保存\r选择取消：将取消导入", "重复皮肤", MessageBoxButton.YesNoCancel))
                {
                    case MessageBoxResult.Yes:
                        Skin = skins.FirstOrDefault(d => d.SkinName == Skin.SkinName);
                        break;
                    case MessageBoxResult.No:
                        Skin.SkinName = Skin.SkinName + "[副本]";
                        break;
                    default:
                        AyMessageBox.Show("取消对当前皮肤" + Skin.SkinName + "的导入！", "取消导入！");
                        _canGoOn = false;
                        break;
                }
            }
        }
        private void 查看压缩包结构()
        {
            ZipFiles = Zip.GetZipFilesName(FilePath);
            if (!ZipFiles.Any(a => a.ToLower().Contains("data")) || ZipFiles.Any(a => a.ToLower().Contains("lol_game_client")))
            {
                if (AyMessageBox.ShowCus(string.Format("皮肤包结构异常，可能为未整理过的美服皮肤包{0}是否使用自动整理功能？", Environment.NewLine), "皮肤包异常", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    重新整理皮肤包();
            }
        }
        #region 重新整理皮肤包
        private void 重新整理皮肤包()
        {
            try
            {
                var temppath = Soft.ZipTempPath(Skin.Hero);
                var tempzip = Soft.ZipTempPath(Skin.Hero) + Skin.FileName;
                var savepath = Soft.SkinFile(Skin);
                if (!FileOperations.CreateDirWithDelete(temppath))
                    throw new Exception("文件夹删除失败！");
                Zip.UnZipFile(FilePath, temppath);
                var tempfile = Directory.GetFiles(temppath, "*.*", SearchOption.AllDirectories);
                var tempcount = tempfile.Count();
                var zipname = string.Format("{0}{1:yyMMMMddHHmmDD}", temppath, DateTime.Now);
                var skincount = 0;
                var othercount = "";
                ForeachMove(tempfile, zipname, out skincount, out othercount);
                if (skincount > 0)
                {
                    AyMessageBox.Show(string.Format("皮肤文件数：{0}，整理后文件数：{1},未知文件：{2}", tempcount, skincount, othercount), "整理皮肤");                 
                    Zip.ZipFile(zipname, tempzip);
                }
                else
                {
                    AyMessageBox.Show("无法识别该皮肤包，请确认该压缩包内再无压缩包！", "整理失败！");
                    _canGoOn = false;
                }
                FilePath = tempzip;
                Skin.FileName = Path.GetFileName(FilePath);
            }
            catch (Exception ex)
            {
                AyMessageBox.Show("请确认本软件拥有对软件目录的读写权限！", "皮肤整理失败");
                Log.LogError("皮肤整理失败", ex);
            }
        }
        /// <summary>
        /// 循环处理皮肤包文件
        /// </summary>
        /// <param name="tempfile">所有文件</param>
        /// <param name="zipname">压缩目录</param>
        /// <param name="savepath">目标压缩包</param>
        /// <param name="skincount">皮肤文件数</param>
        /// <param name="othercount">未知文件</param>
        private static void ForeachMove(IEnumerable<string> tempfile, string zipname, out int skincount, out string othercount)
        {
            var apricotxt = File.ReadAllLines(Soft.LuMeitxt, Encoding.Default);
            skincount = 0;
            othercount = "";
            foreach (var file in tempfile)
            {
                var name = Path.GetFileName(file);
                if (!string.IsNullOrEmpty(name))
                {
                    name = name.ToLower();
                    var filename = "";
                    if (apricotxt.Any(d => d.ToLower().Contains(name)))
                    {
                        var str = apricotxt.FirstOrDefault(d => d.ToLower().Contains(name));
                        filename = string.Format("{0}\\{1}", zipname, str);
                        skincount++;
                    }
                    else
                        filename = string.Format("{0}\\{1}", zipname, name);

                    FileOperations.CreateFileDir(filename);
                    File.Copy(file, filename, true);
                }
            }
        }
        #endregion
        private void 获取皮肤信息()
        {
            var skinconfig = Zip.UnZipOneFile(FilePath, "skin.cfg");
            if (skinconfig != null)
                Skin = Config.SkinConfigGet(skinconfig) ?? new Skin();
            else
            {
                DialogHero spw = new DialogHero();
                spw.ShowDialog();
                if (spw.DialogResult == true)
                    Skin.Hero = spw.HeroName;
                else
                {
                    AyMessageBox.ShowCus("未能正常识别皮肤英雄！皮肤导入失败", "识别失败");
                    _canGoOn = false;
                }
            }
            if (string.IsNullOrEmpty(Skin.SkinName))
                Skin.SkinName = Path.GetFileNameWithoutExtension(FilePath);
            if (string.IsNullOrEmpty(Skin.FileName))
                Skin.FileName = Path.GetFileName(FilePath);
        }

        #region  解压压缩包的图片
        private void 解压压缩包的图片()
        {
            Comment = "解压压缩包的图片";
            TempPath = Soft.ZipTempPath(Skin.Hero);
            FileOperations.CreateDir(TempPath);
            var files = ZipFiles.Where(d => d != null && (d.EndsWith(".dds") || d.EndsWith(".png") || d.EndsWith(".jpg") || d.EndsWith(".jpeg") || d.EndsWith(".bmp")) && !d.ToLower().Contains("particles")).ToList();
            try
            {
                Zip.UnzipFiles(FilePath, TempPath, files);
                files = FileOperations.AllFiles(TempPath);
                获取各种图片(files);
            }
            catch
            {
                AyMessageBox.Show("载入图读取失败！", "压缩包错误！");
            }

        }
        private void 获取各种图片(List<string> files)
        {
            Comment = "获取各种图片";
            //原画
            var splash = files.FirstOrDefault(d => d.EndsWith(".jpg") && d.Contains("Splash"));
            if (!string.IsNullOrWhiteSpace(splash) && File.Exists(splash))
            {
                Skin.Original = Path.GetFileName(splash);
                File.Copy(splash, Soft.SkinOriginalPath(Skin), true);
            }
            //载入图
            var load = files.FirstOrDefault(d => d.ToLower().EndsWith("loadscreen.dds"));
            if (!string.IsNullOrWhiteSpace(load) && File.Exists(load))            
                using (var image = DevIL.DevIL.LoadBitmap(load))
                {
                    Skin.LoadPic = Path.GetFileNameWithoutExtension(load) + ".png";
                    DevIL.DevIL.SaveBitmap(Soft.SkinLoadPath(Skin), image);
                }            
            //背景图
            var back = files.FirstOrDefault(d => (d.EndsWith("*.png") || d.EndsWith("*.jpg") || d.EndsWith("*.jpeg") || d.EndsWith("*.bmp")) && !d.ToLower().Contains("splash") && !d.ToLower().Contains("circle") && !d.ToLower().Contains("square"));
            if (!string.IsNullOrWhiteSpace(back) && File.Exists(back))
            {
                Skin.BackImage = Path.GetFileName(back);
                File.Copy(back, Soft.SkinBackImagePath(Skin), true);
            }
        }
        #endregion

        private void 复制各种文件到皮肤文件夹()
        {
            Comment = "复制各种文件到皮肤文件夹";
            FileOperations.CreateDir(Soft.SkinPath(Skin));
            File.Copy(FilePath, Soft.SkinFile(Skin), true);
        }
        private void 保存皮肤信息()
        {
            Comment = "保存皮肤信息";
            if (State == 1)
            {
                AyMessageBox.ShowCus("皮肤  " + Skin.SkinName + "  导入成功", "导入成功！", Picture.HeroSquarePath(Skin.Hero));
                _skin.SaveOrEdit(Skin);
                _softParam.BackPageHero();
            }
        }
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="type">true为移动到原始原始压缩包对应地址，false为移动到Data文件夹</param>
        private static bool MoveFile(string file, string name, bool type, string zipname)
        {
            var apricotxt = File.ReadAllLines(Soft.LuMeitxt, Encoding.Default);
            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
                if (!apricotxt.Any(d => d.ToLower().Contains(name)))
                    return false;
                var str = apricotxt.FirstOrDefault(d => d.ToLower().Contains(name));
                var filename = string.Format("{0}\\{1}", zipname, str);
                var strPath = Path.GetDirectoryName(filename);
                try
                {
                    FileOperations.CreateDir(strPath);
                    File.Copy(file, filename, true); return true;
                }
                catch (Exception)
                {
                    throw new Exception(string.Format("{0}移动失败！{1}请确认文件未被占用！", name, Environment.NewLine));
                }

            }
            return false;
        }

        #endregion

        #region ClientZipstxt操作
        /// <summary>
        /// 读写ClientZips文件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="add">true为写入false为删除</param>
        /// <returns></returns>
        public bool WriteFile(Skin model, bool add)
        {
            var textpath = Game.ClientZipsPath;
            var skin = Game.OldMountSkin(model);
            var list = File.ReadAllLines(textpath, Encoding.Default).ToList();
            try
            {
                if (list.Any(d => d.ToLower() == skin.ToLower()))
                {
                    if (!add)
                        list.Remove(skin);
                }
                else
                {
                    if (add)
                        list.Add(skin);
                }
                File.Delete(textpath);
                File.WriteAllLines(textpath, list, Encoding.Default);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void WriteFile(List<Skin> list, bool add)
        {
            foreach (var item in list)
            {
                WriteFile(item, add);
            }
        }

        /// <summary>
        /// 批量卸载
        /// </summary>
        /// <param name="pathlist">写入ClientZips的List</param>
        /// <param name="add"></param>
        /// <returns></returns>
        public bool WriteFile(List<string> pathlist, bool add)
        {
            var textpath = Game.ClientZipsPath;
            var list = File.ReadAllLines(textpath, Encoding.Default).ToList();
            try
            {
                if (add)
                    list.RemoveAll(d => pathlist.Any(c => c == d));
                else
                {
                    foreach (var item in pathlist)
                    {
                        if (list.Contains(item))
                            pathlist.Remove(item);
                    }
                    list.AddRange(pathlist);
                }
                File.Delete(textpath);
                File.WriteAllLines(textpath, list, Encoding.Default);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("文件被占用无法进行操作！", "数据储存失败！");
                return false;
            }

        }
        #endregion

        #region  获取最新失效英雄皮肤

        #endregion

        #region  自动生成游戏载入图

        public void CreateLoadZip(string picpath)
        {

            switch (FileOperations.GetFileType(picpath))
            {
                case FileOperations.FileType.Picture:

                    PictureToLoadImage(picpath);
                    break;
                default:
                    MessageBox.Show("未知的文件类型！");
                    break;
            }

        }
        private void PictureToLoadImage(string picpath)
        {
            try
            {
                FileOperations.CreateDirWithDelete(Soft.ZipTempPath("Load"));
                var temppath = Soft.ZipTempPath("Load") + "Load.png";
                if (!FileOperations.CreateFileDir(temppath)) return;
                var model = new Skin { Hero = "SRBackground" };
                model.SkinName = Path.GetFileNameWithoutExtension(picpath);
                model.FileName = model.SkinName + ".zip";
                model.BackImage = "load.png";
                var loadpath = Soft.ZipTempPath("Load") + "DATA\\LoadingScreen\\SRBackground.dds";
                CutPic(picpath, temppath);
                //if (Directory.Exists(Soft.ZipTempPath("Load")))
                //    Directory.Delete(Soft.ZipTempPath("Load"),true);
                FileOperations.CreateFileDir(loadpath);
                var pic = DevIL.DevIL.LoadBitmap(temppath);
                if (pic == null)
                {
                    AyMessageBox.Show("图片读取失败！\r\n此图片无法被导入！\r\n原因：我也不知道为啥导入不能……", "读取失败");
                    return;
                }
                DevIL.DevIL.SaveBitmap(loadpath, pic);
                loadpath = Soft.ZipTempPath("LoadTemp");
                FileOperations.CreateDirWithDelete(loadpath);
                Zip.ZipFile(Soft.ZipTempPath("Load"), loadpath + model.FileName);
                var savepath = Soft.SkinFile(model);
                FileOperations.CreateFileDir(savepath);
                File.Copy(loadpath + model.FileName, savepath, true);
                File.Copy(temppath, Soft.SkinPath(model) + "load.png", true);
                //TODO 重新整理文件
                _skin.Add(model);
                AyMessageBox.Show("载入图 " + model.SkinName + " 导入成功！", "导入成功");
                _softParam.BackPageSkin();
            }
            catch (Exception ex)
            {
                AyMessageBox.Show("载入图导入失败\r\n请重新尝试\r\n或错误原因请查看错误日志！", "载入图导入失败！");
                Log.LogError("载入图导入失败", ex);
            }

        }
        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <param name="picpath">图片地址</param>
        /// <param name="temppath">dds存放地址</param>
        /// <returns></returns>
        public static void CutPic(string picpath, string temppath)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    var bit = new Bitmap(picpath);
                    bit.Save(stream, ImageFormat.Bmp);
                    BinaryReader br = new BinaryReader(stream);
                    var width = 1920; var height = 1080;
                    if (bit.Width < width) width = bit.Width;
                    if (bit.Height < height) height = bit.Height;
                    LuMei.Helper.ImageHelper.CutForCustom(stream, temppath, width, height, 100);
                    stream.Close();
                    br.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception(string.Format("图片读取失败！{0}请确认文件未被占用！", Environment.NewLine));
            }
        }

        #endregion

        //供程序员显式调用的Dispose方法
        public void Dispose()
        {
            //调用带参数的Dispose方法，释放托管和非托管资源
            Dispose(true);
            //手动调用了Dispose释放资源，那么析构函数就是不必要的了，这里阻止GC调用析构函数
            GC.SuppressFinalize(this);
        }

        //protected的Dispose方法，保证不会被外部调用。
        //传入bool值disposing以确定是否释放托管资源
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                Skin = null;
                //TODO:在这里加入清理"托管资源"的代码，应该是xxx.Dispose();
            }
            //TODO:在这里加入清理"非托管资源"的代码
        }

        //供GC调用的析构函数
        ~SkinImport()
        {
            Dispose(false);//释放非托管资源
        }
        /// <summary>
        /// 皮肤文件
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 临时文件夹
        /// </summary>
        public string TempPath { get; set; }
        /// <summary>
        /// 压缩包内的文件
        /// </summary>
        public List<string> ZipFiles { get; set; }
        public int State { get; set; }
        public Skin Skin { get; set; }
        private string _comment;
        /// <summary>
        /// 信息说明
        /// </summary>
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
        private float _percent;
        /// <summary>
        /// 进度条
        /// </summary>
        public float Percent
        {

            get
            {
                return _percent;
            }
            set
            {
                if (_percent != value)
                {
                    _percent = value;
                    OnPropertyChanged("Percent");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

}
