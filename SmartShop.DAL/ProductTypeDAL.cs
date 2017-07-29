using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;

namespace SmartShop.DAL
{
    public class ProductTypeDAL
    {
        public static ProductType GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.ProductTypes.SingleOrDefault(c => c.ProductTypeID == id);
        }
        public static IQueryable<ProductType> GetAllItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.ProductTypes.AsQueryable();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static IQueryable<ProductType> GetAllItemByCategory(int pCategoryID)
        {
            try
            {
                var dc = new SmartShopEntities();
                if (pCategoryID > 0)
                    return dc.ProductTypes.Where(e => e.CategoryID == pCategoryID).AsQueryable();
                else
                    return dc.ProductTypes.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool Delete_ID(int id)
        {
            try
            {
                var dc = new SmartShopEntities();
                var obj = dc.ProductTypes.SingleOrDefault(c => c.ProductTypeID == id);
                dc.ProductTypes.Remove(obj);
                dc.SaveChanges();               
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void bindProductTypeToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllItem();
            ddl.DataTextField = "TypeName";
            ddl.DataValueField = "ProductTypeID";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn kiểu sản phẩm -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
    }
}
