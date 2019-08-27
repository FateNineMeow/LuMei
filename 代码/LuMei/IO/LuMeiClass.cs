using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Ezhu.AutoUpdater.Lib;
using LuMei.Data;
using LuMei.Helper;
using LuMei.LoLModel;
using LuMei.Model;
using MessageBox = System.Windows.MessageBox;
using UpdateModel = Ezhu.AutoUpdater.Lib.UpdateModel;
using Ay.Framework.WPF;
using LuMei.FileLibrary.SknVersion;
using System.Windows.Forms;

namespace LuMei.IO
{
    public class LuMeiClass : ILuMeiClass
    {
        private readonly OtherHero _other = new OtherHero();
        private readonly ChampionsService _champions = new ChampionsService();
        private readonly SearchTagsService _searchtags = new SearchTagsService();
        private readonly ChampionSearchTagsService _championtigs = new ChampionSearchTagsService();
        readonly SkinService _skin = new SkinService();

        #region 初始化及配置文件相关
        /// <summary>
        /// 重建LuMei.txt文件
        /// </summary>
        public bool CreateLuMeiTxt()
        {
            try
            {
                #region  重建UnZipPath文件夹

                if (Directory.Exists(Soft.LuMeiUnZip))
                {
                    Directory.Delete(Soft.LuMeiUnZip, true);
                }
                Directory.CreateDirectory(Soft.LuMeiUnZip);
                File.SetAttributes(Soft.LuMeiUnZip, FileAttributes.Hidden);

                #endregion

                #region 读取并解压的ZipContents

                //循环获取客户端压缩包内的所有文件路径
                try
                {
                    Zip.UnZipFile(Game.ZipContentsPath, Soft.LuMeiUnZip);
                }
                catch (Exception ex)
                {
                    Log.LogInfo("解压ZipContents", ex.Message);
                }

                #endregion

                #region   创建"LuMei.txt"

                try
                {
                    var myFs = new FileStream(Soft.LuMeitxt, FileMode.Create);
                    var mySw = new StreamWriter(myFs);
                    mySw.Close();
                    myFs.Close();
                }
                catch (Exception ex)
                {
                    Log.LogInfo("创建LuMei.txt", ex.Message);
                }

                #endregion

                #region 写入"LuMei.txt"

                var folder = new DirectoryInfo(Soft.LuMeiUnZip);
                var text = folder.GetFiles("*.txt");
                foreach (var file in text)
                {
                    var filepath = file.FullName;
                    if (!string.IsNullOrEmpty(filepath))
                    {
                        //读取此文件，将此文件的内容追加到"ApricotM.txt"中
                        var alllines = File.ReadAllLines(filepath, Encoding.Default);
                        using (var files = new StreamWriter(Soft.LuMeitxt, true))
                        {
                            foreach (string line in alllines)
                            {
                                files.WriteLine(line); // 直接追加文件末尾，换行
                            }
                        }
                    }
                }

                #endregion

                GC.Collect();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #region 检查ClientZips.txt内挂载的文件是否存在
        /// <summary>
        /// 检查挂载皮肤
        /// </summary>
        /// <returns></returns>
        public int CheckMountSkin()
        {
            var nofile = UnMountSkin();
            return nofile.Count;

        }
        public List<string> UnMountSkin()
        {
            var list = File.ReadAllLines(Game.ClientZipsPath, Encoding.Default).ToList();
            return (from item in list where !string.IsNullOrEmpty(item) && !File.Exists(item) let itempath = Game.GameDir + item where !File.Exists(itempath) select item).ToList();
        }
        public bool ClearCheckSkin()
        {
            try
            {
                var nofile = UnMountSkin();
                WriteFile(nofile);
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion
        #region 
        public void CheckMountSkinFile()
        {
            GetSkinsByMountType("已挂载").ForEach(d => CheckMountSkinFile(d));
        }
        private void CheckMountSkinFile(Skin skin)
        {
            var file = Game.SkinFile(skin);
            if (File.Exists(file)) return;
            File.Copy(Soft.SkinFile(skin), file, true);
            ReadOrWriteClientZipstxt(skin, true);
        }
        #endregion
        /// <summary>
        /// 批量卸载
        /// </summary>
        /// <param name="deletelist"></param>
        /// <returns></returns>
        private void WriteFile(List<string> list)
        {
            try
            {
                list.ForEach(d => ReadOrWriteClientZipstxt(d, false));
            }
            catch
            {
                throw new Exception("批量卸载失败！");
            }
        }
        #endregion

        #region 基础操作
        public ObservableCollection<Hero> AllHero()
        {
            var heros = new ObservableCollection<Hero>();
            var list = AllHeroes().OrderBy(d => d.Name);
            var skins = AllSkins();
            foreach (var item in list)
            {
                var hero = new Hero
                {
                    HeroLogo = Picture.HeroSquarePath(item.Name),
                    EnName = item.Name,
                    Alias = item.Title,
                    ChName = item.DisplayName
                };
                if (skins.Count(d => d.Hero == item.Name) > 0)
                {
                    hero.SkinCount = skins.Count(d => d.Hero == item.Name).ToString();
                    if (skins.Count(d => d.MountType == "已挂载") > 0)
                        hero.MountType = "M";
                }
                heros.Add(hero);
            }
            return heros;
        }
        /// <summary>
        /// 1-6为英雄类型，7为其他皮肤，8为有皮肤的英雄
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public ObservableCollection<Hero> Hero(int typeid)
        {
            var heros = new ObservableCollection<Hero>();
            var list = AllHeroes().OrderBy(d => d.Name);
            switch (typeid)
            {
                case 0:
                    break;
                case 8:
                    list = GetSkinHero().OrderBy(d => d.Name);
                    break;
                default:
                    var championtigs = GetHeroByType(typeid);
                    list = list.Where(c => championtigs.Any(d => d.Id == c.Id)).OrderBy(d => d.Name);
                    break;
            }
            var skins = AllSkins();
            foreach (var item in list)
            {
                var hero = new Hero
                {
                    HeroLogo = Picture.HeroSquarePath(item.Name),
                    EnName = item.Name,
                    ChName = item.DisplayName

                };
                if (skins.Count(d => d.Hero == item.Name) > 0)
                {
                    hero.SkinCount = skins.Count(d => d.Hero == item.Name).ToString();
                    if (skins.Count(d => d.MountType == "已挂载") > 0)
                        hero.MountType = "M";
                }
                heros.Add(hero);
            }
            return heros;
        }
        public ObservableCollection<Skin> AllSkin()
        {
            var list = new ObservableCollection<Skin>();
            foreach (var item in AllSkins())
            {
                item.LoadPic = Picture.SkinLoadPath(item);
                list.Add(item);
            }
            return list;
        }
        public ObservableCollection<Skin> Skin(string heroname)
        {
            var list = new ObservableCollection<Skin>();
            foreach (var item in GetISkinsByHero(heroname))
            {
                item.LoadPic = Picture.SkinLoadPath(item);
                list.Add(item);
            }
            return list;
        }
        public ObservableCollection<Skin> Skin(string heroname, long skinid)
        {
            var list = new ObservableCollection<Skin>();
            foreach (var item in GetISkinsByHero(heroname))
            {
                if (item.Id == skinid) continue;
                item.LoadPic = Picture.SkinLoadPath(item);
                list.Add(item);
            }
            return list;
        }
        public List<Champions> AllHeroes() { return _champions.GetAll(); }
        public ObservableCollection<Hero> Hero(string heroname)
        {
            var heros = new ObservableCollection<Hero>();
            heroname = heroname.ToLower();
            var list = AllHeroes().Where(d => d.Name.ToLower().Contains(heroname) || d.Title.Contains(heroname) || d.DisplayName.Contains(heroname));
            var skins = AllSkins();
            foreach (var item in list)
            {
                var hero = new Hero
                {
                    HeroLogo = Picture.HeroSquarePath(item.Name),
                    EnName = item.Name,
                    ChName = item.DisplayName,
                    Alias = item.Title
                };
                if (skins.Count(d => d.Hero == item.Name) > 0)
                {
                    hero.SkinCount = skins.Count(d => d.Hero == item.Name).ToString();
                    if (skins.Count(d => d.MountType == "已挂载") > 0)
                        hero.MountType = "M";
                }
                heros.Add(hero);
            }
            return heros;
        }
        /// <summary>
        /// 获取有皮肤的英雄
        /// </summary>
        /// <returns></returns>
        public List<Champions> GetSkinHero()
        {
            var heros = (from c in _skin.AllSkins() select c.Hero).ToList();
            return _champions.GetAll().Where(d => heros.Contains(d.Name)).ToList();
        }
        /// <summary>
        /// 获取所有皮肤
        /// </summary>
        /// <returns></returns>
        public List<Skin> AllSkins() { return _skin.AllSkins(); }
        /// <summary>
        /// 获取英雄皮肤数量
        /// </summary>
        /// <param name="heroname"></param>
        /// <returns></returns>
        public int SkinCount(string heroname)
        {
            if (!string.IsNullOrWhiteSpace(heroname))
                return GetISkinsByHero(heroname).Count();
            return AllSkins().Count;
        }
        /// <summary>
        /// 获取所有皮肤
        /// </summary>
        /// <returns></returns>
        public List<Skin> GetSkinsByMountType(string mounttype) { return _skin.AllSkins().Where(d => d.MountType == mounttype).ToList(); }
        /// <summary>
        /// 依据英雄名称获取皮肤
        /// </summary>
        /// <param name="heroname"></param>
        /// <returns></returns>
        public IEnumerable<Skin> GetISkinsByHero(string heroname)
        {
            var skins = from c in AllSkins() where c.Hero == heroname select c;
            return skins;
        }
        public string EnNameToChName(string enname)
        {
            var hero = GetHero(enname, true);
            if (hero != null)
                return string.Format("{0} - {1}", hero.DisplayName, hero.Title);
            return null;
        }
        public Skin GetSkin(string id)
        {
            return _skin.GetSkin(id);
        }
        /// <summary>
        /// 依据英雄名称获取实体
        /// </summary>
        /// <param name="enname">英雄名称</param>
        /// <param name="type">是否包含其他皮肤</param>
        /// <returns></returns>
        public Champions GetHero(string enname, bool type)
        {
            if (!string.IsNullOrWhiteSpace(enname))
                return type ? _champions.GetModel(enname) : _champions.GetModel(d => d.Name == enname);
            return null;
        }
        /// <summary>
        /// 依据英雄类型获取英雄
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public List<Champions> GetHeroByType(int typeid)
        {
            if (typeid == 7)
                return _other.Heroes;
            var championtigs = (from c in _championtigs.GetList(d => d.SearchTagId == typeid) select c.ChampionId).ToList();
            var list = _champions.GetAll();
            return list.Where(c => championtigs.Any(d => d == c.Id)).ToList();
        }
        #endregion

        #region 保存皮肤文件
        /// <summary>
        /// 保存皮肤文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Set SaveSkinModel(Skin model)
        {
            var set = new Set();
            if (model != null)
            {
                try
                {
                    if (model.Id == 0)
                        _skin.Add(model);
                    else
                        _skin.Update(model);
                    set.State = true;
                }
                catch
                {
                    set.State = false;
                }

            }
            return set;
        }
        #endregion

        #region  挂载、卸载与删除皮肤
        /// <summary>
        /// 挂载皮肤
        /// </summary>
        /// <param name="model"></param>
        public Skin SkinMount(Skin model)
        {
            var gameskin = Game.SkinFile(model);
            var softskin = Soft.SkinFile(model);
            if (!File.Exists(softskin))
                MessageBox.Show("皮肤 [" + model.SkinName + "]文件丢失", "挂载失败");
            try
            {
                if (FileOperations.CreateFileDir(gameskin) && ReadOrWriteClientZipstxt(model, true))
                {
                    File.Copy(softskin, gameskin, true);
                    model.MountType = "已挂载";
                    _skin.ChangeMountType(model);
                }
                else
                    MessageBox.Show("ClientZips.txt文件读写失败！请确认改文件未被锁定", "挂载失败！");
            }
            catch (Exception ex)
            {
                ReadOrWriteClientZipstxt(model, false);
                MessageBox.Show("皮肤复制出错！\r\n文件被占用或无权限操作", "挂载失败！");
                Log.LogError("挂载皮肤", ex);
            }
            return model;
        }
        /// <summary>
        /// 卸载皮肤
        /// </summary>
        /// <param name="model"></param>
        public Skin SkinUnMount(Skin model)
        {
            var gameskin = Game.SkinFile(model);
            try
            {
                if (ReadOrWriteClientZipstxt(model, false))
                {
                    File.Delete(gameskin);
                    model.MountType = "未挂载";
                    _skin.ChangeMountType(model);
                }
                else
                    MessageBox.Show("文件被占用无法删除！", "删除失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件被占用无法删除！", "删除失败！");
                Log.LogError("卸载皮肤", ex);
            }
            return model;
        }
        public void SkinAllUninstall()
        {
            AllSkins().Where(d => d.MountType == "已挂载").ToList().ForEach(d => SkinUnMount(d));
        }
        public void SkinReMount()
        {
            var list = AllSkins().Where(d => d.MountType == "已挂载").ToList();
            list.ForEach(d => SkinUnMount(d));
            list.ForEach(d => SkinMount(d));
        }
        /// <summary>
        /// 删除皮肤
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void SkinDelete(Skin model)
        {
            try
            {
                if (model.MountType == "已挂载")
                    SkinUnMount(model);
                var path = Soft.SkinPath(model);
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
                _skin.Delete(model.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件或目录被占用无法删除！", "删除失败！");
                Log.LogError("删除失败", ex);
            }
        }
        /// <summary>
        /// 提取皮肤
        /// </summary>
        /// <param name="heroname"></param>
        public void HeroExtract(string heroname)
        {
            var path = Config.SoftSet("Extraction") ?? (Soft.ZipTemp + "\\" + heroname);
            if (Directory.Exists(path))
                Directory.Delete(path, true);
            var list = Soft.ZipList();
            foreach (var item in list)
            {
                Zip.UnZipFile(item, path, heroname);
            }
            Process.Start("explorer.exe", path);
        }
        #region 导出皮肤
        /// <summary>
        /// 导出皮肤
        /// </summary>
        /// <param name="model"></param>
        public void SkinExport(Skin model)
        {
            var file = Soft.SkinFile(model);
            if (!File.Exists(file))
            {
                MessageBox.Show("皮肤文件丢失！", "导出失败");
                return;
            }
            生成带皮肤信息的压缩包(model);
            var dialog = new FolderBrowserDialog { Description = @"请选择导出路径" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath + "\\" + model.FileName;
                try
                {
                    File.Copy(file, foldPath, true);
                    Process.Start("explorer.exe", dialog.SelectedPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("文件被占用或导出路径出错！！", "导出失败！");
                    Log.LogError("导出失败！", ex);
                }
            }
        }
        #endregion
        public void 生成带皮肤信息的压缩包(Skin skin)
        {
            var file = Config.SkinConfigSave(skin);
            if (string.IsNullOrWhiteSpace(file)) return;
            var skinfile = Soft.SkinFile(skin);
            if (File.Exists(file) && File.Exists(skinfile))
                Zip.ZipAddFile(file, skinfile, "skin.cfg");
        }
        #endregion

        #region ClientZipstxt操作
        /// <summary>
        /// 读写ClientZipstxt文件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type">Flase为卸载，True为挂载</param>
        /// <returns></returns>
        public bool ReadOrWriteClientZipstxt(Skin model, bool type)
        {
            var filepath = Game.SkinInClientZipstxt(model);
            return ReadOrWriteClientZipstxt(filepath, type);
        }
        /// <summary>
        /// 读写ClientZipstxt文件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type">Flase为卸载，True为挂载</param>
        /// <returns></returns>
        public bool ReadOrWriteClientZipstxt(string filepath, bool type)
        {
            var textpath = Game.ClientZipsPath;
            var list = File.ReadAllLines(textpath, Encoding.Default).ToList();
            try
            {
                if (list.Any(d => string.Equals(d, filepath, StringComparison.CurrentCultureIgnoreCase)))
                {
                    if (!type)
                        list.Remove(filepath);
                }
                else
                {
                    if (type)
                        list.Add(filepath);
                }
                File.Delete(textpath);
                File.WriteAllLines(textpath, list, Encoding.Default);
                return true;
            }
            catch (Exception ex)
            {
                Log.LogError("挂/卸载皮肤", ex);
                throw new Exception("无法读取ClientZips.txt文件！");
            }
            return false;
        }
        #endregion

        #region 自动更新相关
        /// <summary>
        /// 检查更新状态、检查自动更新程序是否存在、判断程序是否正在运行
        /// </summary>
        /// <returns></returns>
        public Set CheckUpdate()
        {
            var set = new Set() { State = false, Message = "获取更新失败！", Title = "" };
            try
            {
                WebRequest req = WebRequest.Create(Constants.RemoteUrl);
                WebResponse res = req.GetResponse();
                // ReSharper disable once AssignNullToNotNullAttribute
                StreamReader sr = new StreamReader(res.GetResponseStream());
                string jsonstr = sr.ReadToEnd();
                var item = FormJson(jsonstr);
                UpdateInfo model = new UpdateInfo()
                {
                    Explain = item.Explain,
                    FileName = item.FileName,
                    FileUrl = item.FileUrl,
                    MD5 = Guid.NewGuid(),
                    UpdateTime = item.UpdateTime,
                    UpdateVersion = new Version(item.Version)
                };
                if (Soft.MainVersion() <= model.UpdateVersion)
                {
                    set.Title = model.Version;
                    set.State = true;
                }
                return set;
            }
            catch (Exception ex)
            {
                throw new Exception("网络连接失败！");
            }
        }
        /// <summary>
        /// Json2Model
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static UpdateModel FormJson(string json)
        {
            try
            {
                var a = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(UpdateModel));
                using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
                {
                    return (UpdateModel)a.ReadObject(stream);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 开始升级
        /// </summary>
        /// <returns></returns>
        public void StartUpdate()
        {
            Updater.CheckUpdateStatus();
        }

        #endregion

        #region  打开什么的
        /// <summary>
        /// 打开网址
        /// </summary>
        /// <param name="url"></param>
        public static void OpenUrl(string url)
        {
            Process.Start(url);
        }
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="filepath"></param>
        public static void OpenFile(string filepath)
        {
            if (File.Exists(filepath) || Directory.Exists(filepath))
                Process.Start(filepath);
            else
                AyMessageBox.ShowCus("文件：" + filepath + "  不存在！", "文件不存在！", "/Resources/logo.png");
        }
        #endregion

        /// <summary>
        /// 获取所有文件
        /// </summary>
        /// <param name="sourepath"></param>
        /// <returns></returns>
        public List<string> AllSkinZipFiles(string sourepath)
        {
            var list = new List<string>();
            foreach (var item in SkinFileExtList())
            {
                var files = Directory.GetFiles(sourepath, item, SearchOption.AllDirectories);
                list.AddRange(files.ToList());
            }
            return list;
        }
        /// <summary>
        /// 过滤文件格式
        /// </summary>
        /// <returns></returns>
        private static List<string> SkinFileExtList()
        {
            var list = new List<string> { "*.skl", "*.skn", "*.dds", "*.anm" };
            return list;
        }

        #region  Skn版本转换
        public void SknConverter(string sknfile)
        {
            if (string.IsNullOrWhiteSpace(sknfile) && !File.Exists(sknfile))
            {
                AyMessageBox.Show("文件不存在，请重新选择！", "文件不存在！");
                return;
            }
            try
            {
                var sklfile = Path.ChangeExtension(sknfile, ".skl");
                var skn = new SknFile(sknfile) { Version = 2 };
                skn.SaveFile(FileOperations.NewFilePath(sknfile));
                if (!string.IsNullOrWhiteSpace(sklfile) && File.Exists(sklfile))
                    File.Copy(sklfile, FileOperations.NewFilePath(sklfile), true);

            }
            catch
            {
                AyMessageBox.Show("文件读取失败！请确认", "文件错误！");
            }
        }
        #endregion
    }
}
