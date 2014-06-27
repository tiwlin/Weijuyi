using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Collections;
using System.Net;
using System.Collections.Specialized;

namespace CommonHelper
{
    public class FileHelper
    {
        public static bool OpenCSVFile(DataTable mycsvdt, string filepath, int headerNum)
        {
            string strpath = filepath; //csv文件的路径
            try
            {
                int intColCount = 0;

                DataColumn mydc;
                DataRow mydr;

                string strline;
                string[] aryline;
                StreamReader mysr = new StreamReader(strpath, System.Text.Encoding.Default);

                int rowIndex = 0;

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new char[] { ',' });

                    if (headerNum > rowIndex)
                    {
                        rowIndex++;
                        continue;
                    }

                    if (rowIndex == headerNum)
                    {
                        intColCount = aryline.Length;

                        for (int i = 0; i < intColCount; i++)
                        {
                            mydc = new DataColumn(aryline[i]);
                            mycsvdt.Columns.Add(mydc);
                        }
                    }
                    else
                    {
                        mydr = mycsvdt.NewRow();
                        for (int i = 0; i < intColCount; i++)
                        {
                            mydr[i] = aryline[i];
                        }
                        mycsvdt.Rows.Add(mydr);
                    }

                    rowIndex++;
                }
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static IList<string> OpenCSVFile(string filepath, ref string errorMsg)
        {
            IList<string> lstCsv = new List<string>();

            try
            {
                using (StreamReader mysr = new StreamReader(filepath, System.Text.Encoding.Default))
                {
                    string strline = string.Empty;

                    while ((strline = mysr.ReadLine()) != null)
                    {
                        lstCsv.Add(strline);
                    }
                }
            }

            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }

            return lstCsv;
        }

        public static string UploadFileEx(string uploadfile, string url,
    string fileFormName, string contenttype, NameValueCollection querystring,
    CookieContainer cookies)
        {
            if (string.IsNullOrEmpty(fileFormName))
            {
                fileFormName = "file";
            }

            if (string.IsNullOrEmpty(contenttype))
            {
                contenttype = "application/octet-stream";
            }

            string postdata;
            postdata = "?";
            if (querystring != null)
            {
                foreach (string key in querystring.Keys)
                {
                    postdata += key + "=" + querystring.Get(key) + "&";
                }
            }
            Uri uri = new Uri(url + postdata);


            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
            webrequest.CookieContainer = cookies;
            webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webrequest.Method = "POST";


            // Build up the post message header   
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append(fileFormName);
            sb.Append("\"; filename=\"");
            sb.Append(Path.GetFileName(uploadfile));
            sb.Append("\"");
            sb.Append("");
            sb.Append("Content-Type: ");
            sb.Append(contenttype);
            sb.Append("");
            sb.Append("");

            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            // Build the trailing boundary string as a byte array   
            // ensuring the boundary appears on a line by itself   
            byte[] boundaryBytes =
                   Encoding.UTF8.GetBytes("--" + boundary + "");

            FileStream fileStream = new FileStream(uploadfile,
                                        FileMode.Open, FileAccess.Read);
            long length = postHeaderBytes.Length + fileStream.Length +
                                                   boundaryBytes.Length;
            webrequest.ContentLength = length;

            Stream requestStream = webrequest.GetRequestStream();

            // Write out our post header   
            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            // Write out the file contents   
            byte[] buffer = new Byte[checked((uint)Math.Min(4096,
                                     (int)fileStream.Length))];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                requestStream.Write(buffer, 0, bytesRead);

            // Write out the trailing boundary   
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            WebResponse responce = webrequest.GetResponse();
            Stream s = responce.GetResponseStream();
            StreamReader sr = new StreamReader(s);

            return sr.ReadToEnd();
        }

        /// <summary> 
        /// 将本地文件上传到指定的服务器(HttpWebRequest方法) 
        /// </summary> 
        /// <param name="address">文件上传到的服务器</param> 
        /// <param name="fileNamePath">要上传的本地文件（全路径）</param> 
        /// <param name="saveName">文件上传后的名称</param> 
        /// <returns>成功返回1，失败返回0</returns> 
        public static int Upload_Request(string address, string fileNamePath, string saveName)
        {
            int returnValue = 0;     // 要上传的文件 
            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);     //时间戳 
            string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");     //请求头部信息 
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(strBoundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append("file");
            sb.Append("\"; filename=\"");
            sb.Append(saveName);
            sb.Append("\";");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append("application/octet-stream");
            sb.Append("\r\n");
            sb.Append("\r\n");
            string strPostHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);     // 根据uri创建HttpWebRequest对象 
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(address));
            httpReq.Method = "POST";     //对发送的数据不使用缓存 
            httpReq.AllowWriteStreamBuffering = false;     //设置获得响应的超时时间（300秒） 
            httpReq.Timeout = 300000;
            httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;
            long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;
            long fileLength = fs.Length;
            httpReq.ContentLength = length;
            try
            {
                //progressBar.Maximum = int.MaxValue;
                //progressBar.Minimum = 0;
                //progressBar.Value = 0;
                //每次上传4k
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength]; //已上传的字节数 
                long offset = 0;         //开始上传时间 
                DateTime startTime = DateTime.Now;
                int size = r.Read(buffer, 0, bufferLength);
                Stream postStream = httpReq.GetRequestStream();         //发送请求头部消息 
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                while (size > 0)
                {
                    postStream.Write(buffer, 0, size);
                    offset += size;
                    //progressBar.Value = (int)(offset * (int.MaxValue / length));
                    TimeSpan span = DateTime.Now - startTime;
                    double second = span.TotalSeconds;
                    //lblTime.Text = "已用时：" + second.ToString("F2") + "秒";
                    //if (second > 0.001)
                    //{
                    //    lblSpeed.Text = "平均速度：" + (offset / 1024 / second).ToString("0.00") + "KB/秒";
                    //}
                    //else
                    //{
                    //    lblSpeed.Text = " 正在连接…";
                    //}
                    //lblState.Text = "已上传：" + (offset * 100.0 / length).ToString("F2") + "%";
                    //lblSize.Text = (offset / 1048576.0).ToString("F2") + "M/" + (fileLength / 1048576.0).ToString("F2") + "M";
                    //Application.DoEvents();
                    size = r.Read(buffer, 0, bufferLength);
                }
                //添加尾部的时间戳 
                postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                postStream.Close();         //获取服务器端的响应 
                WebResponse webRespon = httpReq.GetResponse();
                Stream s = webRespon.GetResponseStream();
                //读取服务器端返回的消息
                StreamReader sr = new StreamReader(s);
                String sReturnString = sr.ReadLine();
                s.Close();
                sr.Close();
                if (sReturnString == "Success")
                {
                    returnValue = 1;
                }
                else if (sReturnString == "Error")
                {
                    returnValue = 0;
                }
            }
            catch
            {
                returnValue = 0;
            }
            finally
            {
                fs.Close();
                r.Close();
            }
            return returnValue;
        }

        public static void post(string url, string content)
        {
            Encoding encoding = Encoding.GetEncoding("GB2312 ");
            byte[] data = encoding.GetBytes("xml= " + content);

            // 准备请求... 
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST ";
            myRequest.ContentType = "application/x-www-form-urlencoded ";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();
            // 发送数据 
            newStream.Write(data, 0, data.Length);
            newStream.Close();
        }
    }
}
