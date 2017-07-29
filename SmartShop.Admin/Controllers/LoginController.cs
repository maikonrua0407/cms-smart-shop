using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult DoLogin(string user, string pass)
        {
            string result = "";
            if (SysUserSvr.CheckLoginAdminPass(user, pass))
            {
                var obj = SysUserSvr.GetByUserName(user);
                Session["User"] = obj.AcountLogin;
                result = user;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DoLogout()
        {
            Session["User"] = null;
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}
