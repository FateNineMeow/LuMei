using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Ezhu.AutoUpdater.Base;
using Ezhu.AutoUpdater.Lib.ICCEmbedded.Zip;

namespace Ezhu.AutoUpdater.UI
{
    public partial class DownFileProcess : WindowBase
    {
        private string updateFileDir;//更新文件存放的文件夹
        private string callExeName;
        private string appDir;
        private string appName;
        private string appVersion;
        private string desc;
        private string fileurl;
        public DownFileProcess(string callExeName, string updateFileDir, string appDir, string appName, string appVersion, string desc, string fileurl)
        {
            InitializeComponent();
            Loaded += (sl, el) =>
            {
                YesButton.Content = "现在更新";
                NoButton.Content = "暂不更新";

                YesButton.Click += (sender, e) =>
                {
                    Process[] processes = Process.GetProcessesByName(this.callExeName);

                    if (processes.Length > 0)
                    {
                        foreach (var p in processes)
                        {
                            p.Kill();
                        }
                    }

                    DownloadUpdateFile();
                };

                NoButton.Click += (sender, e) =>
                {
                    Close();
                };

                txtProcess.Text = this.appName + "发现新的版本(" + this.appVersion + "),是否现在更新?";
                txtDes.Text = this.desc;
            };
            this.callExeName = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(callExeName));
            this.updateFileDir = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(updateFileDir));
            this.appDir = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(appDir));
            this.appName = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(appName));
            this.appVersion = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(appVersion));
            this.fileurl = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(fileurl));

            string sDesc = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(desc));
            if (sDesc.ToLower().Equals("null"))
            {
                this.desc = "";
            }
            else
            {
                this.desc = "更新内容如下:\r\n" + sDesc;
            }
        }

        public void DownloadUpdateFile()
        {
            var client = new System.Net.WebClient();
            client.DownloadProgressChanged += (sender, e) =>
            {
                UpdateProcess(e.BytesReceived, e.TotalBytesToReceive);
            };
            client.DownloadDataCompleted += (sender, e) =>
            {
                string zipFilePath = Path.Combine(updateFileDir, "update.zip");
                byte[] data = e.Result;
                BinaryWriter writer = new BinaryWriter(new FileStream(zipFilePath, FileMode.OpenOrCreate));
                writer.Write(data);
                writer.Flush();
                writer.Close();

                System.Threading.ThreadPool.QueueUserWorkItem((s) =>
                {
                    Action f = () =>
                    {
                        txtProcess.Text = "开始更新程序...";
                    };
                    Dispatcher.Invoke(f);
                    string tempDir = Path.Combine(updateFileDir, appName);
                    try
                    {
                        if (Directory.Exists(tempDir))
                            Directory.Delete(tempDir, true);
                        Directory.CreateDirectory(tempDir);
                        UnZipFile(zipFilePath, tempDir);
                    }
                    catch
                    {
                        System.Windows.MessageBox.Show("临时文件夹删除失败！！\r\n请确认软件拥有足够权限\r\n并且文件未被占用！", "文件夹操作失败！");
                        return;
                    }


                    var a = "F:\\Project\\Code\\LuMei\\代码\\LuMei\\bin\\Debug\\Temp\\Update\\temp";
                    //移动文件
                    //App
                    var mainpath = "";
                    if (Directory.Exists(tempDir))
                    {
                        CopyDirectory(tempDir, appDir);
                    }

                    f = () =>
                    {
                        txtProcess.Text = "更新完成!";

                        try
                        {
                            //清空缓存文件夹
                            string rootUpdateDir = updateFileDir.Substring(0, updateFileDir.LastIndexOf(Path.DirectorySeparatorChar));
                            foreach (string p in Directory.EnumerateDirectories(rootUpdateDir))
                            {
                                if (!p.ToLower().Equals(updateFileDir.ToLower()))
                                {
                                    Directory.Delete(p, true);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message);
                        }

                    };
                    Dispatcher.Invoke(f);

                    try
                    {
                        f = () =>
                        {
                            AlertWin alert = new AlertWin("更新完成,是否现在启动软件?") { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = this };
                            alert.Title = "更新完成";
                            alert.Loaded += (ss, ee) =>
                            {
                                alert.YesButton.Width = 40;
                                alert.NoButton.Width = 40;
                            };
                            alert.Width = 300;
                            alert.Height = 200;
                            alert.ShowDialog();
                            if (alert.YesBtnSelected)
                            {
                                //启动软件
                                string exePath = Path.Combine(appDir, callExeName + ".exe");
                                var info = new ProcessStartInfo(exePath);
                                info.UseShellExecute = true;
                                info.WorkingDirectory = appDir;// exePath.Substring(0, exePath.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
                                Process.Start(info);
                            }
                            else
                            {

                            }
                            Close();
                        };
                        Dispatcher.Invoke(f);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                });

            };
            client.DownloadDataAsync(new Uri(fileurl));
        }

        private static void UnZipFile(string zipFilePath, string targetDir)
        {
            FastZipEvents evt = new FastZipEvents();
            FastZip fz = new FastZip(evt);
            fz.ExtractZip(zipFilePath, targetDir, "");
        }

        public void UpdateProcess(long current, long total)
        {
            string status = (int)((float)current * 100 / (float)total) + "%";
            txtProcess.Text = status;
            rectProcess.Width = ((float)current / (float)total) * bProcess.ActualWidth;
        }

        public void CopyDirectory(string sourceDirName, string destDirName)
        {
            try
            {
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                    File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
                }
                if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                    destDirName = destDirName + Path.DirectorySeparatorChar;
                string[] files = Directory.GetFiles(sourceDirName);
                foreach (string file in files)
                {
                    File.Copy(file, destDirName + Path.GetFileName(file), true);
                    File.SetAttributes(destDirName + Path.GetFileName(file), FileAttributes.Normal);
                }
                string[] dirs = Directory.GetDirectories(sourceDirName);
                foreach (string dir in dirs)
                {
                    CopyDirectory(dir, destDirName + Path.GetFileName(dir));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("复制文件错误");
            }
        }
    }
}
