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
    public class ProductStyleDAL
    {
        public static ProductStyle GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.ProductStyles.SingleOrDefault(c => c.ProductStyleID == id);
        }
        public static IQueryable<ProductStyle> GetAllItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.ProductStyles.AsQueryable();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static IQueryable<ProductStyle> GetAllItemByCategory(int pCategoryID)
        {
            try
            {
                var dc = new SmartShopEntities();
                if(pCategoryID>0)
                    return dc.ProductStyles.Where(e => e.CategoryID == pCategoryID).AsQueryable();
                else
                    return dc.ProductStyles.AsQueryable();
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
                var obj = dc.ProductStyles.SingleOrDefault(c => c.ProductStyleID == id);
                dc.ProductStyles.Remove(obj);
                dc.SaveChanges();               
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void bindProductStyleToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllItem();
            ddl.DataTextField = "StyleName";
            ddl.DataValueField = "ProductStyleID";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn phong cách sản phẩm -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
    }
}
