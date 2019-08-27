using System;
using System.Windows;

namespace Ezhu.AutoUpdater
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //var a = new string[] { "update", "THVNZWk=", "RjpcUHJvamVjdFxDb2RlXEx1TWVpXOS7o+eggVxMdU1laVxiaW5cRGVidWdcVGVtcFxVcGRhdGU=", "RjpcUHJvamVjdFxDb2RlXEx1TWVpXOS7o+eggVxMdU1laVxiaW5cRGVidWdc", "bHVtZWk=", "MC4wLjAuMw==", "5aaC5p6c5Ye6546w5peg6ZmQ5pu05pawDeivt+WwneivleS/ruaUuWNvbmZpZ+aWh+S7tuWGheeahOeJiOacrOWPt+S4uuacgOaWsOeJiOacrA== ", "aHR0cDovL2xvY2FsaG9zdDoyNjE5L2x1bWVpLzAuMC4wLjUuemlw" };
            // args = a;
            if (args.Length == 0)
            {
                App app = new App();
                //UI.DownFileProcess downUI = new UI.DownFileProcess("", "", "", "", "", "","");
                //app.Run(downUI);


                //MessageBox.Show("无参数");
                //UI.DownFileProcess downUI = new UI.DownFileProcess();

                //Ezhu.AutoUpdater.App app = new Ezhu.AutoUpdater.App();
                //app.Run(downUI);
                Application.Current.Shutdown();
            }
            else if (args[0] == "update")
            {
                try
                {
                    //MessageBox.Show("外部更新");
                    string callExeName = args[1];
                    string updateFileDir = args[2];
                    string appDir = args[3];
                    string appName = args[4];
                    string appVersion = args[5];
                    string desc = args[6];
                    string fileurl = args[7];

                    //Check If Have New Vision
                    App app = new App();
                    UI.DownFileProcess downUI = new UI.DownFileProcess(callExeName, updateFileDir, appDir, appName, appVersion, desc, fileurl) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
                    app.Run(downUI);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
