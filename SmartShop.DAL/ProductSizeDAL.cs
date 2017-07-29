using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SmartShop.DAL
{
    public class ProductSizeDAL
    {
        public static ProductSize GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.ProductSizes.SingleOrDefault(c => c.SizeID == id);
        }
        public static IQueryable<ProductSize> GetAllItem()
        {
            var dc = new SmartShopEntities();
            return dc.ProductSizes.AsQueryable();
        }
        public static bool Insert(ProductSize objProductSize)
        {
            try
            {
                var dc = new SmartShopEntities();
                dc.ProductSizes.Add(objProductSize);
                dc.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void bindProductSizeToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllItem();
            ddl.DataTextField = "Code";
            ddl.DataValueField = "SizeID";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn cỡ sản phẩm -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
        public static void bindProductSizeToDdl(ref DropDownList ddl, int pProductSetID, string valueSelected, bool required)
        {
            var dc = new SmartShopEntities();
            IQueryable<Product> product = dc.Products.Where(e => e.ProductSetID == pProductSetID);
            ddl.Items.Clear();
            ddl.DataSource = dc.ProductSizes.Where(e => product.Select(c => c.SizeID).Contains(e.SizeID));
            ddl.DataTextField = "Code";
            ddl.DataValueField = "SizeID";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Hết hàng -", "0"));
            }
            else
            {
                if (required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn cỡ sản phẩm -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
    }
}
