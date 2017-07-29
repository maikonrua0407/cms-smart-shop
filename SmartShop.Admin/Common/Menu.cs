using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using SmartShop.DAL;
using SmartShop.Utilities;

namespace SmartShop.Admin.Common
{
    public static class Menu
    {
        public static void CreateMenu()
        {
            SYS_VERSION version = SysVersionSvr.GetLastestVersion(Constant.PHIEN_BAN.MENU.layGiaTri());
            string currentVersionMenu = GlobalVariables.ConfigInfo.First(e => e.Key.Equals("VersionMenu")).Value;
            if (!version.PHIEN_BAN.Equals(currentVersionMenu))
            {
                string path = Utilities.Common.XmlMenuPath();
                LList.Read2XmlFile(SysMenuSvr.GetAllMenu(), path);
                Utilities.Common.SuaFileCauHinh(currentVersionMenu, new ThongTinCauHinh { Key = "VersionMenu", Value = version.PHIEN_BAN });
            }
        }

        public static List<SYS_MENU> GetMenuFromFile()
        {
            string path = Utilities.Common.XmlMenuPath();
            XDocument doc = XDocument.Load(path);
            List<SYS_MENU> listMenu = LList.DeserializeParams<SYS_MENU>(doc);
            return listMenu;
        }
    }
}