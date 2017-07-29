using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Transactions;

/// <summary>
/// Summary description for AdvDAL
/// </summary>
namespace SmartShop.DAL
{
    public class AdvDAL
    {
        public static Category GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.Categories.SingleOrDefault(c => c.CategoryID == id);
        }
        public static IQueryable<Advertisement> GetByPosition(string pPosition)
        {
            var dc = new SmartShopEntities();
            return dc.Advertisements.Where(c => c.Position.Equals(pPosition) && c.Active==true).OrderBy(c=>c.Orders);
        }
        public static IQueryable<Category> GetAllItem()
        {
            var dc = new SmartShopEntities();
            return dc.Categories.AsQueryable();
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
                    ddl.Items.Insert(0, new ListItem("- Danh mục chung -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }

        public static void buildPositionToDdl(ref DropDownList ddlPosition, string donViTypeSelected, bool required)
        {
            ddlPosition.Items.Clear();
            foreach (KeyValuePair<string, string> item in PositionEx.List)
            {
                ddlPosition.Items.Add(new ListItem(item.Value, item.Key));
            }

            if (!string.IsNullOrEmpty(donViTypeSelected))
                ddlPosition.SelectedValue = donViTypeSelected;

            if (!required)
                ddlPosition.Items.Insert(0, new ListItem("- Chọn vị trí hiển thị -", string.Empty));
        }

        public static Advertisement GetAdvertisementByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.Advertisements.SingleOrDefault(c => c.AdvertisementID == id);
        }

        public static IQueryable<vwAdv_Category> viewAdv_Category_Paging(string strKeyword, int piCategoryId, int pageSize, int currentPage, ref int totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var query = dc.vwAdv_Category.AsQueryable();
                if (!string.IsNullOrEmpty(strKeyword))
                {
                    query = query.Where(n => n.Title.Contains(strKeyword) || n.Description.Contains(strKeyword));
                }
                if (piCategoryId > 0)
                {
                    query = query.Where(n => n.CategoryID == piCategoryId);
                }
                totalRecord = query.Count();
                if (pageSize > 0 && currentPage > 0)
                {
                    int start = (currentPage - 1) * pageSize;
                    query = query.Skip(start).Take(pageSize);
                }
                return query;
            }
            catch
            {
                return null;
            }
        }

        public static bool Insert(Advertisement obj)
        {
            try
            {
                var dc = new SmartShopEntities();
                dc.Advertisements.Add(obj);
                dc.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Update(Advertisement obj)
        {
            try
            {
                var dc = new SmartShopEntities();
                Advertisement adv = dc.Advertisements.Single(a => a.AdvertisementID == obj.AdvertisementID);
                adv.Title = obj.Title;
                adv.Description = obj.Description;
                adv.Image = obj.Image;
                adv.Orders = obj.Orders;
                adv.Active = obj.Active;
                adv.Position = obj.Position;
                adv.Link = obj.Link;
                adv.CategoryID = obj.CategoryID;
                dc.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                var dc = new SmartShopEntities();
                var obj = dc.Advertisements.SingleOrDefault(c => c.AdvertisementID == id);
                dc.Advertisements.Remove(obj);
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}