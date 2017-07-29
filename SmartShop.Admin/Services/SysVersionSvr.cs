using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.Admin
{
    public static class SysVersionSvr
    {
        public static List<SYS_VERSION> GetAllMenu()
        {
            return SysVersionDal.GetAllVersion();
        }


        public static SYS_VERSION GetLastestVersion(string loaiVersion)
        {
            return SysVersionDal.GetLastestVersion(loaiVersion);
        }
    }
}