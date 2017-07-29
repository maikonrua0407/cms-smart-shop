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
    public class ProductMaterialDAL
    {
        public static ProductMaterial GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.ProductMaterials.SingleOrDefault(c => c.ProductMaterialID == id);
        }
        public static IQueryable<ProductMaterial> GetAllItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.ProductMaterials.AsQueryable();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static IQueryable<ProductMaterial> GetAllItemByCategory(int pCategoryID)
        {
            try
            {
                var dc = new SmartShopEntities();
                if (pCategoryID > 0)
                    return dc.ProductMaterials.Where(e => e.CategoryID == pCategoryID).AsQueryable();
                else
                    return dc.ProductMaterials.AsQueryable();
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
                var obj = dc.ProductMaterials.SingleOrDefault(c => c.ProductMaterialID == id);
                dc.ProductMaterials.Remove(obj);
                dc.SaveChanges();               
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void bindProductMaterialToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllItem();
            ddl.DataTextField = "MaterialName";
            ddl.DataValueField = "ProductMaterialID";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn vật liệu sản phẩm -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
    }
}
