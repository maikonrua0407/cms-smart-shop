using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Admin.Controllers
{
    public class LoaiSanPhamController : BaseController
    {
        //
        // GET: /LoaiSanPham/

        public ActionResult Index()
        {
            List<Category> data = new List<Category>();
            return View(data);
        }

        public JsonResult Delete(int id)
        {
            ProductCatSvr.Delete(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(Category model)
        {
            model.NoSymbolName = Util.converToUnsign(model.CategoryName);
            if (model.CategoryID == 0)
            {
                ProductCatSvr.Insert(model);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                ProductCatSvr.Update(model);
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult GetData()
        {
            List<Category> data = ProductCatSvr.GetAllCategory();
            return PartialView(data);
        }

        public JsonResult GetDetail(int id)
        {
            Category data = new Category();
            if (id > 0)
                data = ProductCatSvr.GetById(id);
            string ret = RenderPartialHelper.RenderPartialToString(this.ControllerContext, Url.Content("~/Views/LoaiSanPham/GetDetail.cshtml"), data);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

    }
}
