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
    public class ProductGroupDAL
    {
        public static ProductGroup GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.ProductGroups.SingleOrDefault(c => c.ProductGroupID == id);
        }

        public static ProductGroup GetByNameNoSymbol(string strName)
        {
            var dc = new SmartShopEntities();
            ProductGroup group = dc.ProductGroups.SingleOrDefault(c => c.NoSymbolName.Equals(strName));
            if (group != null)
                return group;
            else
                return dc.ProductGroups.SingleOrDefault(c => c.NoSymbolName.Replace("-", "").Equals(strName));
        }

        public static IQueryable<ProductGroup> GetAllItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.ProductGroups.AsQueryable();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IQueryable<ProductGroup> GetRootItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.ProductGroups.Where(e => e.GroupParrentID == null).AsQueryable();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IQueryable<ProductGroup> GetByCategory(int pCategoryID)
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.ProductGroups.AsQueryable();
                if (pCategoryID > 0)
                {
                    lst = from l in lst
                          where l.CategoryID == pCategoryID
                          select l;
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IQueryable<ProductGroup> GetByCategoryHasProduct(int pCategoryID)
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.ProductGroups.Where(e => e.CategoryID == pCategoryID && dc.Products.Select(s => s.ProductGroupID).Distinct().Contains(e.ProductGroupID));
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IQueryable<ProductGroup> GetByParrent(int pParrentID)
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.ProductGroups.AsQueryable();
                if (pParrentID > 0)
                {
                    lst = from l in lst
                          where l.GroupParrentID == pParrentID
                          select l;
                }
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
                var obj = dc.ProductGroups.SingleOrDefault(c => c.ProductGroupID == id);
                dc.ProductGroups.Remove(obj);
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void bindProductGroupToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllItem();
            ddl.DataTextField = "GroupName";
            ddl.DataValueField = "ProductGroupID";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn nhóm sản phẩm -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }

        public static ProductGroup getParrent(int pGroupID)
        {
            var obj = GetByID(pGroupID);
            var dc = new SmartShopEntities();
            ProductGroup result = dc.ProductGroups.SingleOrDefault(c => c.ProductGroupID == obj.GroupParrentID);
            return result;
        }
    }
}
