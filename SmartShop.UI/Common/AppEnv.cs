using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SmartShop.UI
{
    public static class AppEnv
    {
        /// <summary>
        /// Get database's connection string
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["SmartShopEntities"].ToString();
            }
        }

        public static string ssUserName = "UserName";

        public static string ssMemberID = "MemberID";

        public static string ssLanguageID = "LanguageID";

        public static string Scheme
        {
            get
            {
                string cheme = HttpContext.Current.Request.Url.Scheme.ToString();
                if (cheme == "/")
                {
                    return string.Empty;
                }
                else { return cheme; }
            }

        }

        public static string Host
        {
            get
            {
                string host = HttpContext.Current.Request.Url.Host.ToString();
                if (host == "/")
                {
                    return string.Empty;
                }
                else { return host; }
            }

        }

        public static string Port
        {
            get
            {
                string port = HttpContext.Current.Request.Url.Port.ToString();
                if (port == "/")
                {
                    return string.Empty;
                }
                else { return port; }
            }

        }

        public static string ApplicationPath
        {
            get
            {
                string applicationPath = HttpContext.Current.Request.ApplicationPath;
                if (applicationPath == "/")
                {
                    return string.Empty;
                }
                else { return applicationPath; }
            }

        }       
    }
}