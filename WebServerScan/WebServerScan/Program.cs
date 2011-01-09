using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace WebServerScan
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

          //  HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://www.tuanlu.com");
          //  req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; zh-CN; rv:1.9.2.13) Gecko/20101203 Firefox/3.6.13";
          //  req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
          //  req.Headers[HttpRequestHeader.AcceptLanguage] = "zh-cn,zh;q=0.5";
          //  req.Headers[HttpRequestHeader.AcceptCharset] = "GB2312,utf-8;q=0.7,*;q=0.7";
          //  req.Method = "GET";
          //  req.Headers[HttpRequestHeader.AcceptEncoding] = "gzip,deflate";
          ////  req.Proxy = new WebProxy(bd.IP, 80);
          //  req.Timeout = 5000;
          //  req.CookieContainer = new CookieContainer();

          //  using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
          //  {
          //      Console.WriteLine(response.ContentLength);
          //      Console.WriteLine(response.StatusCode);
          //      Console.ReadLine();
          //  }


        }
    }
}
