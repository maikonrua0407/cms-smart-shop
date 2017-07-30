using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Admin.Controllers
{
    public class NhomSanPhamController : BaseController
    {
        //
        // GET: /NhomSanPham/

        public ActionResult Index()
        {
            List<ProductGroup> data = new List<ProductGroup>();
            return View(data);
        }

        public JsonResult Delete(int id)
        {
            ProductGroupSvr.Delete(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(ProductGroup model)
        {
            model.NoSymbolName = Util.converToUnsign(model.GroupName);
            if (model.ProductGroupID == 0)
            {
                ProductGroupSvr.Insert(model);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                ProductGroupSvr.Update(model);
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult GetData()
        {
            List<ProductGroup> data = ProductGroupSvr.GetAllItem().ToList();
            return PartialView(data);
        }

        public JsonResult GetDetail(int id)
        {
            ProductGroup data = new ProductGroup();
            if (id > 0)
                data = ProductGroupSvr.GetById(id);
            string ret = RenderPartialHelper.RenderPartialToString(this.ControllerContext, Url.Content("~/Views/NhomSanPham/GetDetail.cshtml"), data);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

    }
}
