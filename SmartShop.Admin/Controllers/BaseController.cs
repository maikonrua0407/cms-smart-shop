using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            Helper.AuthenSystem();
        }
    }
}