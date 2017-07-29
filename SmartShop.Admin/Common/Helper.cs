using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartShop.Utilities;

namespace SmartShop.Admin
{
    public static class Helper
    {
        public static void AuthenSystem()
        {
            if (HttpContext.Current.Session["User"].IsNullOrEmpty())
            {
                    HttpContext.Current.Response.Redirect("~/Login");
            }
        }
    }
}