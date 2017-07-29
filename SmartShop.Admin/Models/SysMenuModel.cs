using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.Admin
{
    public static class SysMenuModel
    {
        private static List<DAL.SYS_MENU> _lstMenu;

        public static List<DAL.SYS_MENU> LstMenu
        {
            get { return _lstMenu; }
            set { _lstMenu = value; }
        }
    }
}