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
    public class CategoryDAL
    {
        public static Category GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.Categories.SingleOrDefault(c => c.CategoryID == id);
        }

        public static Category GetByNameNoSymbol(string strName)
        {
            var dc = new SmartShopEntities();
            Category cat = dc.Categories.SingleOrDefault(c => c.NoSymbolName.Equals(strName));
            if (cat != null)
                return cat;
            else
                return dc.Categories.SingleOrDefault(c => c.NoSymbolName.Replace("-", "").Equals(strName));
        }

        public static IQueryable<Category> GetAllItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.Categories.AsQueryable();
                return lst;
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
                var obj = dc.Categories.SingleOrDefault(c => c.CategoryID == id);
                dc.Categories.Remove(obj);
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void bindCategoryToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllItem();
            ddl.DataTextField = "CategoryName";
            ddl.DataValueField = "CategoryID";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn loại sản phẩm -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
    }
}
