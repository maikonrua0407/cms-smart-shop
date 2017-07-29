using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartShop.Utilities;

namespace SmartShop.Admin.Common
{
    public static class GlobalVariables
    {
        static List<ThongTinCauHinh> configInfo = new List<ThongTinCauHinh>();

        public static List<ThongTinCauHinh> ConfigInfo
        {
            get { return GlobalVariables.configInfo; }
            set { GlobalVariables.configInfo = value; }
        }
    }
}