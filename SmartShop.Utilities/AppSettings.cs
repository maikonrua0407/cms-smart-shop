using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace SmartShop.Utilities
{
    public class AppSettings
    {
        public static string GetSetting(string key)
        {
            return GetSetting(key, false);
        }

        public static string GetSetting(string key, bool coTinhThanh)
        {
            return GetSetting(key, coTinhThanh, string.Empty);
        }

        public static string GetSetting(string key, bool coTinhThanh, string ttId)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch
            {
                return string.Empty;
            }
        }

        private static readonly Dictionary<string, string> DicConfigs = new Dictionary<string, string>();

        public static string GetConfigString(string key)
        {
            try
            {
                if (DicConfigs.ContainsKey(key))
                    return DicConfigs[key];
            }
            catch (Exception ex)
            {
                Common.OutputLog(ex);
            }
            return string.Empty;
        }

        public static string SysConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SysConnectionString"] != null ? ConfigurationManager.ConnectionStrings["SysConnectionString"].ConnectionString : string.Empty;
        }
    }
}
