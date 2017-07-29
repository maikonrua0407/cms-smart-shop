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
    public class ColorDAL
    {
        public static Color GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.Colors.SingleOrDefault(c => c.ColorID == id);
        }
        public static IQueryable<Color> GetAllItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.Colors.AsQueryable();
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
                var obj = dc.Colors.SingleOrDefault(c => c.ColorID == id);
                dc.Colors.Remove(obj);
                dc.SaveChanges();               
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void bindColorToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllItem();
            ddl.DataTextField = "ColorName";
            ddl.DataValueField = "ColorID";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn mầu sản phẩm -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }

        public static void bindColorToDdl(ref DropDownList ddl, int pProductSetID, string valueSelected, bool required)
        {
            var dc = new SmartShopEntities();
            IQueryable<ProductSet> productset = dc.ProductSets.Where(e => e.ProductSetID == pProductSetID);
            if (productset.Any())
            {
                ddl.Items.Clear();
                ddl.DataSource = dc.Colors.Where(e => productset.Select(c => c.ColorID).Contains(e.ColorID));
                ddl.DataTextField = "ColorName";
                ddl.DataValueField = "ColorID";
                ddl.DataBind();
                if (ddl.Items.Count == 0)
                {
                    ddl.Items.Insert(0, new ListItem("- Hết hàng -", "0"));
                }
                else
                {
                    if (required)
                    {
                        ddl.Items.Insert(0, new ListItem("- Chọn mầu sản phẩm -", "0"));
                    }
                    if (!string.IsNullOrEmpty(valueSelected))
                        ddl.SelectedValue = valueSelected;
                }
            }
        }
        public static IQueryable<Color> GetColor_Paging(string keyword, int pageSize, int currentPage, ref int totalRecord)
        {
            var dc = new SmartShopEntities();
            var query = dc.Colors.AsQueryable();
            if (!String.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(c => c.ColorName.ToLower().Contains(keyword)
                                    || c.Description.ToString().ToLower().Contains(keyword)
                                       );
            }
            query = query.OrderBy(n => n.ColorName);
            totalRecord = query.Count();
            if (pageSize > 0 && currentPage > 0)
            {
                int start = (currentPage - 1) * pageSize;
                query = query.Skip(start).Take(pageSize).AsQueryable();
            }
            return query;
        }
    }
}
