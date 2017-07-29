using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartShop.DAL;
using SmartShop.Admin.Common;

namespace SmartShop.Admin.Controllers
{
    public class SysMenuController : BaseController
    {
        //
        // GET: /SysMenu/

        public ActionResult Index()
        {
            List<SYS_MENU> lstMenu = Menu.GetMenuFromFile();
            return View(lstMenu);
        }

    }
}
