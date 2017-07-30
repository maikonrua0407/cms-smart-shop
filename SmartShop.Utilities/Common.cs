using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace SmartShop.Utilities
{
    public class Common
    {

        /// <summary>
        /// cac ham trong common cua anh Khuong ap dung cho phan autoLoad News
        /// </summary>
        /// <param name="ex"></param>
        public static void OutputLog(Exception ex)
        {
            OutputLog(HttpContext.Current, ex);
        }

        /// <summary>
        /// cac ham trong common cua anh Khuong ap dung cho phan autoLoad News
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        public static void OutputLog(HttpContext context, Exception ex)
        {
            try
            {
                var dt = DateTime.Now;
                string lDir;
                string lFile;
                var sbLog = new StringBuilder();
                sbLog.AppendFormat("--------- {0:HH:mm:ss} ---------", dt).AppendLine();
                if (context != null)
                {
                    lDir = context.Request.MapPath(string.Format("/Log"));
                    lFile = context.Request.MapPath(string.Format("/Log/Log_{0:yyyyMMdd}.txt", dt));

                    sbLog.AppendFormat("Ref URL: {0}",
                                       (context.Request.UrlReferrer == null)
                                           ? ""
                                           : context.Request.UrlReferrer.ToString()).AppendLine();
                    sbLog.AppendFormat("Host: {0}", context.Request.Url.Authority).AppendLine();
                    sbLog.AppendFormat("URL: {0}", context.Request.RawUrl).AppendLine();
                    sbLog.AppendFormat("IP: {0}", context.Request.UserHostAddress).AppendLine();
                }
                else
                {
                    lDir = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("/Log"));
                    lFile = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("/Log/Log_{0:yyyyMMdd}.txt", dt));
                }
                if (lDir == null || lFile == null) return;
                if (!Directory.Exists(lDir))
                    Directory.CreateDirectory(lDir);
                sbLog.AppendFormat("Error: {0}", ex.Message).AppendLine();
                sbLog.AppendFormat("Source: {0}", ex.Source).AppendLine();
                sbLog.AppendFormat("Trace: {0}", ex.StackTrace).AppendLine();
                File.AppendAllText(lFile, sbLog.ToString());
            }
            catch { }
        }

        public static bool IsLogSql()
        {
            bool result = false;
            try
            {
                string systemPath = HttpContext.Current.Request.MapPath(string.Format("/config/"));
                string filePath = systemPath.Replace("config", "../config/") + "config.conf";

                //Create a file stream to read the encrypted file back.
                FileStream mStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                XElement xml = XElement.Load(mStream);
                DataTable dt = LTable.XElementToDataTable(xml);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("LogSql"))
                    {
                        if (dt.Rows[0]["LogSql"].ToString().Equals("1"))
                            result = true;
                        else
                            result = false;
                    }
                }
                mStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Doc thong tin cau hinh khong thanh cong; " + ex.ToString());
                result = false;
            }
            return result;
        }

        public static List<ThongTinCauHinh> DocThongTinCauHinh()
        {
            List<ThongTinCauHinh> result = new List<ThongTinCauHinh>();
            try
            {
                string systemPath = HttpContext.Current.Request.MapPath(string.Format("/config/"));
                string filePath = systemPath.Replace("config", "../config/") + "config.conf";

                //Create a file stream to read the encrypted file back.
                using (FileStream mStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    XElement xml = XElement.Load(mStream);
                    DataTable dt = LTable.XElementToDataTable(xml);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Columns.Contains("LogSql"))
                        {
                            result.Add(new ThongTinCauHinh {Key = "LogSql", Value = dt.Rows[0]["LogSql"].ToString()});
                        }
                        if (dt.Columns.Contains("VersionMenu"))
                        {
                            result.Add(new ThongTinCauHinh
                                           {Key = "VersionMenu", Value = dt.Rows[0]["VersionMenu"].ToString()});
                        }
                    }
                    //mStream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Doc thong tin cau hinh khong thanh cong; " + ex.ToString());
                result = null;
            }
            return result;
        }

        public static void SuaFileCauHinh(string giaTriCu ,ThongTinCauHinh cauHinhMoi)
        {
            string systemPath = HttpContext.Current.Request.MapPath(string.Format("/config/"));
            string filePath = systemPath.Replace("config", "../config/") + "config.conf";
            string strOld = "<" + cauHinhMoi.Key + ">" + giaTriCu + "</" + cauHinhMoi.Key + ">";
            string strNew = "<" + cauHinhMoi.Key + ">" + cauHinhMoi.Value + "</" + cauHinhMoi.Key + ">";
            string text = File.ReadAllText(filePath);
            text = text.Replace(strOld, strNew);
            File.WriteAllText(filePath, text);
        }

        public static string XmlMenuPath()
        {
            string filePath=HttpContext.Current.Request.MapPath(string.Format("/SysMenu.xml"));
            if (!File.Exists(filePath))
                using (var myFile = File.Create(filePath))
                {
                }
                //File.Open(filePath, FileMode.Create, FileAccess.ReadWrite);
            
            return filePath;
        }

        public static string Compress(string data)
        {
            return Convert.ToBase64String(Compress2Byte(data));
        }

        public static byte[] Compress2Byte(string data)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var ms = new MemoryStream();
            using (var zip = new GZipStream(ms, CompressionMode.Compress, true))
                zip.Write(buffer, 0, buffer.Length);
            ms.Position = 0;
            var compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);
            var gzBuffer = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return gzBuffer;
        }

        public static string Decompress(string cmpData)
        {
            return Decompress(Convert.FromBase64String(cmpData));
        }

        public static string Decompress(byte[] cmpData)
        {
            using (var ms = new MemoryStream())
            {
                var msgLength = BitConverter.ToInt32(cmpData, 0);
                ms.Write(cmpData, 4, cmpData.Length - 4);
                var buffer = new byte[msgLength];
                ms.Position = 0;
                using (var zip = new GZipStream(ms, CompressionMode.Decompress))
                    zip.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            }
        }
    }

    public class ThongTinCauHinh
    {
        private string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
