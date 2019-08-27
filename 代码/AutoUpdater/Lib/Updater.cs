using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace Ezhu.AutoUpdater.Lib
{
    public class Updater
    {
        private static Updater _instance;
        public static Updater Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Updater();
                }
                return _instance;
            }
        }

        public static void CheckUpdateStatus()
        {
            System.Threading.ThreadPool.QueueUserWorkItem((s) =>
            {
                WebRequest req = WebRequest.Create(Constants.RemoteUrl);
                WebResponse res = req.GetResponse();
                if (res.ContentLength > 0)
                {
                    try
                    {
                        // ReSharper disable once AssignNullToNotNullAttribute
                        StreamReader sr = new StreamReader(res.GetResponseStream());
                        string jsonstr = sr.ReadToEnd();
                        var item = FormJson(jsonstr);
                        UpdateInfo model = new UpdateInfo() { Explain = item.Explain, FileName = item.FileName, FileUrl = item.FileUrl, MD5 = Guid.NewGuid(), UpdateTime = item.UpdateTime, UpdateVersion = new Version(item.Version) };
                        model.MD5 = Guid.NewGuid();
                        Instance.StartUpdate(model);
                    }
                    catch
                    {
                        return;
                    }
                }
            });
        }

        public void StartUpdate(UpdateInfo updateInfo)
        {
            //if (updateInfo.Version != null && Instance.CurrentVersion < updateInfo.UpdateVersion)
            //{
            //    //当前版本比需要的版本小，不更新
            //    return;
            //}

            if (Instance.CurrentVersion >= updateInfo.UpdateVersion)
            {
                //当前版本是最新的，不更新
                return;
            }
            //获取文件所在主目录
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            var updateFileDir = Path.Combine(appDir, "Temp\\Update");
            string exePath = Path.Combine(updateFileDir, "AutoUpdater.exe");
            try
            {
                if (Directory.Exists(updateFileDir))
                    Directory.Delete(updateFileDir, true);
                Directory.CreateDirectory(updateFileDir);
                File.Copy(Path.Combine(appDir, "AutoUpdater.exe"), exePath, true);
            }
            catch
            {
                System.Windows.MessageBox.Show("临时文件夹删除失败！！\r\n请确认软件拥有足够权限\r\n并且文件未被占用！", "文件夹操作失败！");
                return;
            }
         

            var info = new System.Diagnostics.ProcessStartInfo(exePath);
            info.UseShellExecute = true;
            info.WorkingDirectory = exePath.Substring(0, exePath.LastIndexOf(Path.DirectorySeparatorChar));
            updateInfo.Explain = updateInfo.Explain;
            info.Arguments = "update "
                + Convert.ToBase64String(Encoding.UTF8.GetBytes(CallExeName)) + " "
                + Convert.ToBase64String(Encoding.UTF8.GetBytes(updateFileDir)) + " "
                + Convert.ToBase64String(Encoding.UTF8.GetBytes(appDir)) + " "
                + Convert.ToBase64String(Encoding.UTF8.GetBytes(updateInfo.FileName)) + " "
                + Convert.ToBase64String(Encoding.UTF8.GetBytes(updateInfo.UpdateVersion.ToString())) + " "
                + (string.IsNullOrEmpty(updateInfo.Explain) ? "" : Convert.ToBase64String(Encoding.UTF8.GetBytes(updateInfo.Explain))) + " "
                + (string.IsNullOrEmpty(updateInfo.FileUrl) ? "" : Convert.ToBase64String(Encoding.UTF8.GetBytes(updateInfo.FileUrl)));
            System.Diagnostics.Process.Start(info);
        }

        public bool UpdateFinished = false;

        private string _callExeName;
        public string CallExeName
        {
            get
            {
                if (string.IsNullOrEmpty(_callExeName))
                {
                    _callExeName = System.Reflection.Assembly.GetEntryAssembly().Location.Substring(System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf(Path.DirectorySeparatorChar) + 1).Replace(".exe", "");
                }
                return _callExeName;
            }
        }

        /// <summary>
        /// 获得当前应用软件的版本
        /// </summary>
        public virtual Version CurrentVersion
        {
            get { return new Version(FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location).ProductVersion); }
        }

        /// <summary>
        /// 获得当前应用程序的根目录
        /// </summary>
        public virtual string CurrentApplicationDirectory
        {
            get { return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location); }
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
    }
}
