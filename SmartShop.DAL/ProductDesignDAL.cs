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
    /// <summary>
    /// Summary description for ProductDesignDAL
    /// </summary>
    public class ProductDesignDAL
    {
        public static ProductDesign GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.ProductDesigns.SingleOrDefault(c => c.ProductDesignID == id);
        }
        public static IQueryable<ProductDesign> GetAllItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.ProductDesigns.AsQueryable();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static IQueryable<ProductDesign> GetAllItemByCategory(int pCategoryID)
        {
            try
            {
                var dc = new SmartShopEntities();
                if (pCategoryID > 0)
                    return dc.ProductDesigns.Where(e => e.CategoryID == pCategoryID).AsQueryable();
                else
                    return dc.ProductDesigns.AsQueryable();
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
                var obj = dc.ProductDesigns.SingleOrDefault(c => c.ProductDesignID == id);
                dc.ProductDesigns.Remove(obj);
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void bindProductDesignToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllItem();
            ddl.DataTextField = "ProductDesignName";
            ddl.DataValueField = "ProductDesignID";
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