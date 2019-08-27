using System;
using System.IO;
using System.Net;
using System.Text;

namespace LuMei.Helper
{
    public static class Log
    {
        private readonly static string PostUrl = "http://www.lolskin.cc/errormessage";
        public static void LogInfo(string title)
        {
            LogInfo(title, title);
        }
        public static void LogInfo(string title, string message)
        {

            if (!Directory.Exists(Soft.LogInfo))
            {
                Directory.CreateDirectory(Soft.LogInfo);
            }
            var time = DateTime.Now;
            var loginfo = Soft.LogInfo + string.Format("{0:yyyyMMdd}.txt", time);
            using (var file = new StreamWriter(loginfo, true))
            {
                file.WriteLine("[{0:yyyy-MM-dd HH:mm}]---《{1}》---{2}", time, title, message);
                file.WriteLine();// 直接追加文件末尾，换行   
            }
            //SendQuery(title, message);
        }

        public static void LogError(string title, Exception ex)
        {
            if (!Directory.Exists(Soft.LogError))            
                Directory.CreateDirectory(Soft.LogError);            
            var time = DateTime.Now;
            var loginfo = Soft.LogError + string.Format("{0:yyyyMMdd}.txt", time);
            using (var file = new StreamWriter(loginfo, true))
            {
                file.WriteLine("记录时间：" + string.Format("{0:T}", time));// 直接追加文件末尾，换行   
                file.WriteLine("记录标题：" + title);// 直接追加文件末尾，换行   
                file.WriteLine("程序名称：" + ex.Source);// 直接追加文件末尾，换行   
                file.WriteLine("方法名称：" + ex.TargetSite);// 直接追加文件末尾，换行   
                file.WriteLine("记录内容：" + ex.Message);// 直接追加文件末尾，换行   
                file.WriteLine();// 直接追加文件末尾，换行   
            }
            //   SendQuery(title, message, set.Version);
        }
        /************************************************************************/
        /* UrlEncode
        /* 对指定字符串进行URL标准化转码
        /************************************************************************/
        private static string UrlEncode(string text, Encoding encoding)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byData = encoding.GetBytes(text);
            for (int i = 0; i < byData.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byData[i], 16));
            }
            return sb.ToString();
        }
        /************************************************************************/
        /* sendQuery
        /* 向指定的接口地址POST数据并返回响应数据
        /************************************************************************/
        public static string SendQuery(string title, string message)
        {
            string version = Config.SoftSet("Version");
            StringBuilder sb = new StringBuilder();
            sb.Append("&v=");
            sb.Append(version);
            sb.Append("&t=");
            sb.Append(title);
            sb.Append("&m=");
            sb.Append(UrlEncode(message, Encoding.UTF8));

            var param = sb.ToString();

            // 准备要POST的数据
            byte[] byData = Encoding.UTF8.GetBytes(param);

            // 设置发送的参数
            HttpWebRequest req = WebRequest.Create(PostUrl) as HttpWebRequest;
            req.Method = "POST";
            req.Timeout = 5000;
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = byData.Length;

            // 提交数据
            Stream rs = req.GetRequestStream();
            rs.Write(byData, 0, byData.Length);
            rs.Close();

            // 取响应结果
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);

            try
            {
                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            sr.Close();
            return null;
        }
    }
}
