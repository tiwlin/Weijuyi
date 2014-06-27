using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace UploadTool
{
    public partial class RebootRouter : Form
    {
        public RebootRouter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RestartRouter("192.168.10.1", "admin", "admin");
        }

        public static void RestartRouter(string routerIP, string name, string pwd)
        {
            WebClient client = new WebClient();
            string str = Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}:{1}", name, pwd)));
            client.Headers["Authorization"] = "Basic " + str;
            client.DownloadString("http://" + routerIP + "/userRpm/SysRebootRpm.htm?Reboot=%D6%D8%C6%F4%C2%B7%D3%C9%C6%F7");
            client.Dispose();
        }

        private static void RestartRouter(string userName, string password)
        {
            CookieContainer container = new CookieContainer();
            string requestUriString = "http://192.168.10.1/userRpm/SysRebootRpm.htm?Reboot=%D6%D8%C6%F4%C2%B7%D3%C9%C6%F7";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUriString);
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0;CIBA)";
            request.CookieContainer = container;
            request.KeepAlive = true;
            request.Accept = "*/*";
            request.Timeout = 0xbb8;
            request.PreAuthenticate = true;
            CredentialCache cache = new CredentialCache();
            cache.Add(new Uri(requestUriString), "Basic", new NetworkCredential(userName, password));
            request.Credentials = cache;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = container.GetCookies(request.RequestUri);
                new StreamReader(response.GetResponseStream(), Encoding.Default).Close();
                response.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }  
    }
}
