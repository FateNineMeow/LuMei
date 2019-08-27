using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using LuMei.Data;
using LuMei.Helper;
using LuMei.IO;
using LuMei.Model;
using Ay.Framework.WPF.Controls;
using System.Text;
using LuMei.Control;
using Ay.Framework.WPF;
using System.Windows.Forms;

namespace LuMei
{
    /// <summary>
    /// SplashWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SplashWindow : AyPopupWindow
    {
        readonly ILuMeiClass _lumei = new LuMeiClass();
        readonly SkinService _skin = new SkinService();
        public static bool State = true;
        private double _time;
        private string GamePath = Config.GameSet("GamePath");
        public Set Set = new Set { State = true };
        readonly Thread _check;
        public SplashWindow()
        {
            InitializeComponent();
            this.MinButtonVisibility = Visibility.Hidden;
            this.TitleVisibility = Visibility.Hidden;
            _check = new Thread(StartCheck) { IsBackground = true };
            _check.SetApartmentState(ApartmentState.STA);
            MinButtonVisibility = Visibility.Hidden;
            TitleVisibility = Visibility.Hidden;
            Visibility = Visibility.Visible;
            _check.Start();
        }
        private void StartCheck()
        {
            WriteTitle("正在初始化...");
            软件配置检查();
            if (State)
                游戏配置读取();
            if (State)
                数据库检查();
            if (State)
               // 软件更新检查();
            if (State)
                检查数据完整性();
            if (State)
                检查更新版本数据库();
            if (State)
                检查ClientZips();
            结束后台进程();
        }
        private void 软件配置检查()
        {
            WriteMessage("读取配置文件...");
            Config.SoftSet();
        }
        private void 游戏配置读取()
        {
            var game = Config.GameSet();
            Set = game;
            if (Set.State)
                if (File.Exists(Game.ClientZipsPath))
                {
                    WriteMessage("配置文件读取成功...");
                    return;
                }

            WriteMessage("配置文件读取出错");
            WriteTitle("尝试重建配置文件！");
            if (Game.ReadReg())
            {
                WriteTitle("尝试重建配置文件");
                WriteMessage("注册表读取成功");
                return;
            }
            WriteTitle("注册表读取失败");
            WriteMessage("请在弹出窗口选择英雄联盟路径");
            DialogShow();
            if (State)
                WriteMessage("配置文件重建完成");
            else
            {
                WriteTitle("配置重建失败！");
                WriteMessage("请尝试重新启动本程序进行初始化设置！");
                State = false;
            }
        }
        private void 软件更新检查()
        {
            WriteTitle("检查更新");
            Set = _lumei.CheckUpdate();
            if (Set == null)
            {
                WriteTitle("更新检查失败！");
                WriteMessage("无法连接至服务器！");
            }
            else if (Set.State)
            {
                if (AyMessageBox.Show("拥有新版本" + Set.Title + "，是否更新？", "检查更新", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    State = false;
                    this.Dispatcher.BeginInvoke(new Action(() => { _lumei.StartUpdate(); }), null);
                    this.Dispatcher.BeginInvoke(new Action(() => { this.Hide(); }), null);
                }
                else
                    WriteMessage("取消更新！");
            }
        }
        private void 数据库检查()
        {
            _lumei.CreateLuMeiTxt();
            try
            {
                Soft.CreateDb();
                var list = _skin.GetDataTable();
                if (list == null)
                    Soft.CreateTable();
            }
            catch
            {
                WriteTitle("数据库读取出错！");
                WriteMessage("请确认程序拥有对软件目录的读写权限！");
            }

        }
        private void 检查数据完整性()
        {
            WriteTitle("数据完整性检查....");
            FileOperations.CreateDir(Game.SkinPath);
            var skincount = _lumei.CheckMountSkin();
            if (skincount > 0)
            {
                var isReBuild = AyMessageBox.Show(string.Format("当前客户端存在{0}条空挂载记录{1}是否清除？", skincount, Environment.NewLine), "挂载文件异常", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
                if (isReBuild)
                {
                    if (_lumei.ClearCheckSkin())
                    {
                        WriteMessage("数据清理成功！");
                        WriteMessage(string.Format("共清理{0}条无效数据！", skincount));
                        WriteMessage("数据完整性检查完成");
                    }
                    else
                    {
                        AyMessageBox.Show("请确认程序拥有足够权限或游戏未运行", "清理失败");
                        WriteMessage("清理失败！");
                        WriteMessage("数据完整性检查失败");
                        State = false;
                    }

                }
            }
            else
                WriteMessage("数据完整性检查完成");
        }
        private void 检查更新版本数据库()
        {
            //TODO 检查数据库是否存在AMSkins数据库
            if (_skin.CheckAmSkins())
            {
                WriteTitle("开启数据整理！");
                //TODO　如果存在则复制此表的数据到Skin表
                var oldlist = _skin.AmSkinList();
                var list = _lumei.AllSkins();
                var i = 0;

                try
                {
                    var path = "";
                    foreach (var item in oldlist)
                    {
                        i++;
                        WriteMessage("数据整理中……[" + i + "/" + oldlist.Count + "]");
                        if (list.Any(d => d.SkinName == item.SkinName)) continue;
                        //TODO 将文件移动到新的文件夹
                        var oldpath = Soft.SkinOldPath(item);
                        if (!Directory.Exists(oldpath)) continue;
                        path = Soft.SkinPath(item);
                        if (Directory.Exists((path)))
                            Directory.Delete(path, true);
                        FileOperations.CreateDir(Soft.Skins);
                        Directory.Move(oldpath, path);
                        var load = Soft.SkinLoadPath(item);
                        if (File.Exists(load) && !load.EndsWith(".png"))
                        {
                            try
                            {
                                using (var image = DevIL.DevIL.LoadBitmap(load))
                                {
                                    var rep = load.Substring(load.Length - 3, 3);
                                    var newimg = load.Replace(rep, "png");
                                    DevIL.DevIL.SaveBitmap(newimg, image);
                                    item.LoadPic = item.LoadPic.Replace(rep, "png");
                                }
                                File.Delete(load);
                            }
                            catch (Exception ex)
                            {
                                Log.LogError("载入图转换失败", ex);
                            }
                        }
                        _skin.Add(item);
                    }
                    //TODO  并删除AMSkins表
                    path = string.Format("{0}Skins\\", AppDomain.CurrentDomain.BaseDirectory);
                    if (Directory.Exists(path))
                        Directory.Delete(path, true);
                    WriteMessage(_skin.DeleAmSkin() ? "数据整理完成！" : "数据整理失败！");
                }
                catch (Exception ex)
                {
                    Log.LogError("数据整理失败！", ex);
                    AyMessageBox.Show("请确认程序拥有对软件目录的读写权限", "数据整理失败！");
                }

            }
        }
        private void 检查ClientZips()
        {
            WriteMessage("检查ClientZips.txt");
            var list = _lumei.AllSkins().ToList();
            foreach (var item in list)
            {
                var writefile = Game.OldMountSkin(item);
                var skin = GamePath + writefile;
                if (!File.Exists(skin)) continue;
                File.Delete(skin);
                OldWriteFile(writefile);
            }
        }
        /// <summary>
        /// 读写ClientZips文件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="addOrRemove"></param>
        /// <returns></returns>
        public bool OldWriteFile(string writefile)
        {

            var textpath = Game.ClientZipsPath;
            var list = File.ReadAllLines(textpath, Encoding.Default).ToList();
            try
            {
                if (list.Any(d => string.Equals(d, writefile, StringComparison.CurrentCultureIgnoreCase)))
                    list.Remove(writefile);
                File.Delete(textpath);
                File.WriteAllLines(textpath, list, Encoding.Default);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void 重新挂载皮肤()
        {
            WriteMessage("检查皮肤挂载情况");
            _lumei.CheckMountSkinFile();
            WriteMessage("等待检查完成");
        }

        /// <summary>
        /// 读写ClientZips文件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="addOrRemove"></param>
        /// <returns></returns>
        public bool OldWriteFile(Skin model, bool addOrRemove)
        {
            var textpath = Game.ClientZipsPath;
            var skin = Game.OldMountSkin(model);
            var list = File.ReadAllLines(textpath, Encoding.Default).ToList();
            try
            {
                if (list.Any(d => string.Equals(d, skin, StringComparison.CurrentCultureIgnoreCase)))
                {
                    if (!addOrRemove)
                        list.Remove(skin);
                }
                else
                    if (addOrRemove)
                    list.Add(skin);

                File.Delete(textpath);
                File.WriteAllLines(textpath, list, Encoding.Default);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void 结束后台进程()
        {
            if (State)
                Dispatcher.BeginInvoke(new Action(() => { DialogResult = true; }), null);
            if (_check != null) _check.Abort();
            this.Close();
        }
        private void WriteMessage(string message)
        {
            Message.Dispatcher.BeginInvoke(new Action(() =>
            {
                StatusMessage msg = new StatusMessage(); msg.Message = message; msg.IsAnimated = true;
                Message.SetStatus(msg.Message, msg.IsAnimated);
            }), null);
        }
        private void WriteTitle(string title)
        {
            Title.Dispatcher.BeginInvoke(new Action(() =>
            {
                StatusMessage msg = new StatusMessage(); msg.Message = title; msg.IsAnimated = false;
                Title.SetStatus(msg.Message, msg.IsAnimated);
            }), null);
        }

        #region  弹出对话框
        private void DialogShow()
        {
            if (_time < 5)
                _time++;
            else
            {
                AyMessageBox.Show("错误次数过多，请重新运行程序！", "警告");
                State = false;
                return;
            }
            var set = new Set();

            var dialog = new FolderBrowserDialog { Description = "选择英雄联盟路径，例：C：\\英雄联盟", ShowNewFolderButton = false };
            dialog.ShowDialog();
            var gamePath = dialog.SelectedPath;
            dialog.Dispose();
            if (string.IsNullOrWhiteSpace(gamePath))
            {
                AyMessageBox.Show("路径不合法，请重新选择！", "路径错误！");
                DialogShow();
            }
            else
            {
                gamePath = gamePath + "\\";
                if (!File.Exists(gamePath + "Game\\Characters.zip"))
                {
                    set.State = false;
                    set.Title = "游戏路径错误";
                    set.Message = string.Format("游戏路径选择错误{0}请重新选择！{0}", Environment.NewLine);
                    AyMessageBox.Show(set.Message, set.Title);
                    DialogShow();
                }
                else
                {
                    State = Config.GameSave("GamePath", gamePath);
                }
            }

        }

        /// <summary>
        /// 界面可以拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        #endregion

    }
}
