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
    public class ProviderDAL
    {
        public static Provider GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.Providers.SingleOrDefault(c => c.ProviderID == id);
        }

        public static IQueryable<Provider> GetAllItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.Providers.AsQueryable();
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
                var obj = dc.Providers.SingleOrDefault(c => c.ProviderID == id);
                dc.Providers.Remove(obj);
                dc.SaveChanges();               
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void bindProviderToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllItem();
            ddl.DataTextField = "ProviderName";
            ddl.DataValueField = "ProviderID";
            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Hãng sản xuất -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }

        public static IQueryable<Provider> GetProvider_Paging(string keyword, int pageSize, int currentPage, ref int totalRecord)
        {
            var dc = new SmartShopEntities();
            var query = dc.Providers.AsQueryable();
            if (!String.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(c => c.ProviderName.ToLower().Contains(keyword)
                                    || c.Address.ToString().ToLower().Contains(keyword)
                                    || c.Phone.ToString().ToLower().Contains(keyword)
                                       );
            }
            query = query.OrderBy(n => n.ProviderName);
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
