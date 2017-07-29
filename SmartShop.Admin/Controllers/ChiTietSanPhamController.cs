using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartShop.Utilities;

namespace SmartShop.Admin.Controllers
{
    public class ChiTietSanPhamController : BaseController
    {
        //
        // GET: /ChiTietSanPham/

        public ActionResult Index()
        {
            List<vwProduct> data = new List<vwProduct>();
            return View(data);
        }

        public JsonResult Delete(int id)
        {
            ProductSvr.Delete(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(ProductModel model)
        {
            if (Validate(model.ProductID, model.ProductCode))
            {
                model.NoSymbolName = Util.converToUnsign(model.ProductName);
                Product obj = new Product();
                List<SP_SAN_PHAM_TTINH> lstThuocTinh = new List<SP_SAN_PHAM_TTINH>();
                obj.ProductID = model.ProductID;
                obj.ProductCode = model.ProductCode;
                obj.ProductName = model.ProductName;
                obj.NoSymbolName = model.NoSymbolName;
                obj.ProductSetID = model.ProductSetID;
                obj.ProductGroupID = model.ProductGroupID;
                obj.Quantity = model.Quantity;
                obj.SizeID = model.SizeID;
                obj.Viewed = model.Viewed;
                obj.Available = model.Available;
                if (!model.YTE_THUONG_HIEU.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_THUONG_HIEU", GIA_TRI = model.YTE_THUONG_HIEU });
                if (!model.YTE_XUAT_XU.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_XUAT_XU", GIA_TRI = model.YTE_XUAT_XU });
                if (!model.YTE_BAO_HANH.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_BAO_HANH", GIA_TRI = model.YTE_BAO_HANH });
                if (!model.YTE_GIA_BAN.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_GIA_BAN", GIA_TRI = model.YTE_GIA_BAN });
                if (!model.YTE_SO_TA.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_SO_TA", GIA_TRI = model.YTE_SO_TA });
                if (!model.YTE_TINH_NANG.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_TINH_NANG", GIA_TRI = model.YTE_TINH_NANG });
                if (!model.YTE_ANH_CHINH.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_ANH_CHINH", GIA_TRI = model.YTE_ANH_CHINH });
                if (!model.YTE_ANH_PHU_1.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_ANH_PHU_1", GIA_TRI = model.YTE_ANH_PHU_1 });
                if (!model.YTE_ANH_PHU_2.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_ANH_PHU_2", GIA_TRI = model.YTE_ANH_PHU_2 });
                if (!model.YTE_ANH_PHU_3.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_ANH_PHU_3", GIA_TRI = model.YTE_ANH_PHU_3 });
                if (!model.YTE_ANH_PHU_4.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_ANH_PHU_4", GIA_TRI = model.YTE_ANH_PHU_4 });
                if (!model.YTE_THONG_SO.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_THONG_SO", GIA_TRI = model.YTE_THONG_SO });
                if (!model.YTE_TAG.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_TAG", GIA_TRI = model.YTE_TAG });
                if (!model.YTE_DANH_GIA.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_DANH_GIA", GIA_TRI = model.YTE_DANH_GIA });
                if (!model.YTE_CHIET_KHAU.IsNullOrEmptyOrSpace())
                    lstThuocTinh.Add(new SP_SAN_PHAM_TTINH { ID_SAN_PHAM = obj.ProductID, MA_SAN_PHAM = obj.ProductCode, MA_TTINH = "YTE_CHIET_KHAU", GIA_TRI = model.YTE_CHIET_KHAU });

                if (obj.ProductID == 0)
                {
                    ProductSvr.Insert(obj);
                    foreach (var item in lstThuocTinh)
                        item.ID_SAN_PHAM = obj.ProductID;
                    ProductSvr.ThemThuocTinhSP(lstThuocTinh);
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ProductSvr.Update(obj);
                    ProductSvr.ThemThuocTinhSP(lstThuocTinh);
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        private bool Validate(int id, string ma)
        {
            bool result = true;
            if (Utility.LaKyTuCoDau(ma.Trim())) return false;
            if (id > 0)
            {
                var objEXist = ProductSvr.GetByID(id);
                if (ProductSvr.checkExits_Ma_Update(objEXist.ProductCode, ma.Trim())) return false;
            }
            else
            {
                if (ProductSvr.CheckExist_Ma_Insert(ma.Trim())) return false;
            }
            return result;
        }

        public PartialViewResult GetData()
        {
            List<vwProduct> data = ProductSvr.SelectAll();
            return PartialView(data);
        }

        public JsonResult GetDetail(int id)
        {
            ProductModel data = new ProductModel();
            if (id > 0)
            {
                Product obj = ProductSvr.GetByID(id);
                List<SP_SAN_PHAM_TTINH> lstThuocTinh = ProductSvr.getThuocTinhTheoSP(id);
                data.ProductID = obj.ProductID;
                data.ProductCode = obj.ProductCode;
                data.ProductName = obj.ProductName;
                data.NoSymbolName = obj.NoSymbolName;
                data.ProductSetID = obj.ProductSetID.Value;
                data.ProductGroupID = obj.ProductGroupID.Value;
                data.Quantity = obj.Quantity.Value;
                data.SizeID = obj.SizeID.Value;
                data.Viewed = obj.Viewed;
                data.Available = obj.Available.Value;

                foreach (SP_SAN_PHAM_TTINH item in lstThuocTinh)
                {
                    if (item.MA_TTINH.Equals("YTE_THUONG_HIEU"))
                        data.YTE_THUONG_HIEU = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_XUAT_XU"))
                        data.YTE_XUAT_XU = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_BAO_HANH"))
                        data.YTE_BAO_HANH = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_SO_TA"))
                        data.YTE_SO_TA = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_TINH_NANG"))
                        data.YTE_TINH_NANG = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_THONG_SO"))
                        data.YTE_THONG_SO = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_GIA_BAN"))
                        data.YTE_GIA_BAN = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("TAG"))
                        data.YTE_TAG = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_ANH_CHINH"))
                        data.YTE_ANH_CHINH = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_ANH_PHU_1"))
                        data.YTE_ANH_PHU_1 = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_ANH_PHU_2"))
                        data.YTE_ANH_PHU_2 = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_ANH_PHU_3"))
                        data.YTE_ANH_PHU_3 = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_ANH_PHU_4"))
                        data.YTE_ANH_PHU_4 = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_DANH_GIA"))
                        data.YTE_DANH_GIA = item.GIA_TRI;
                    else if (item.MA_TTINH.Equals("YTE_CHIET_KHAU"))
                        data.YTE_CHIET_KHAU = item.GIA_TRI;
                }
            }
            string ret = RenderPartialHelper.RenderPartialToString(this.ControllerContext, Url.Content("~/Views/ChiTietSanPham/GetDetail.cshtml"), data);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

    }
}
