using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.Admin
{
    public static class SysMenuSvr
    {
        public static List<SYS_MENU> GetAllMenu()
        {
            return SysMenuDal.GetAllMenu();
        }
    }
}