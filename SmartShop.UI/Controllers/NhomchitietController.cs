using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartShop.Utilities;

namespace SmartShop.UI.Controllers
{
    public class NhomchitietController : Controller
    {
        //
        // GET: /Nhomchitiet/
        static DataTable dt = new DataTable();

        public ActionResult Index(string idNhom)
        {
            CatGroupProductModel model = new CatGroupProductModel();
            model.ObjProductGroup = ProductGroupSvr.GetByID(idNhom.StringToInt32());
            model.ObjCategory = ProductCatSvr.GetById(model.ObjProductGroup.CategoryID.Value);
            int totalRecord = 0;
            DataSet ds = ProductSvr.GetSanPhamTheoDieuKien(idNhom, "ID", "GIAM", 10000, 1, ref totalRecord);
            if (!ds.IsNullOrEmpty() && ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return View(model);
        }

        public PartialViewResult GetData(string lstThuongHieu, string field = "ID", string type = "ASC", string currentPage = "1", string pageSize = "12")
        {
            string strThuongHieu = "";
            if (!lstThuongHieu.IsNullOrEmptyOrSpace())
            {
                foreach (string str in lstThuongHieu.Split('|'))
                    strThuongHieu += ",'" + str + "'";
                strThuongHieu = strThuongHieu.Substring(1, strThuongHieu.Length - 1);
            }
            string find = "";
            if (!strThuongHieu.IsNullOrEmptyOrSpace())
                find = "YTE_THUONG_HIEU in (" + strThuongHieu + ")";
            NhomChiTietPhanTrangModel data = new NhomChiTietPhanTrangModel();
            data.CurrentPage = currentPage.StringToInt32();
            data.PageSize = pageSize.StringToInt32();
            List<DataRow> lstRow = dt.Select(find, field + " " + type).ToList();
            data.ListRow = lstRow;
            return PartialView(data);
        }

    }
}
