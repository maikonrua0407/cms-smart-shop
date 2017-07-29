using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartShop.DAL;
using SmartShop.Utilities;

namespace SmartShop.UI.Controllers
{
    public class NhomsanphamController : Controller
    {
        //
        // GET: /Nhomsanpham/

        public ActionResult Index(string groupId)
        {
            ProductGroup model = new ProductGroup();
            model = ProductGroupSvr.GetByID(groupId.StringToInt32());
            return View(model);
        }

    }
}
