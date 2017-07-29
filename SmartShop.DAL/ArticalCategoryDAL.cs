using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SmartShop.DAL
{
    public class ArticalCategoryDAL
    {
        public static ArticalCategory GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.ArticalCategories.SingleOrDefault(c => c.ArticalCatID == id);
        }
        public static IQueryable<ArticalCategory> GetAllItem()
        {
            var dc = new SmartShopEntities();
            return dc.ArticalCategories.AsQueryable();
        }
        public static void bindArticalCatToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllItem();
            ddl.DataTextField = "ArticalCatName";
            ddl.DataValueField = "ArticalCatID";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn chuyên mục -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
        public static void bindArticalCatToDdlTree(ref DropDownList ddl, int? selectedValue, bool required)
        {
            var dc = new SmartShopEntities();
            ddl.Items.Clear();
            var query = GetAllItem().OrderBy(c => c.Order);
            var ddlNodeList = query.Select(c => new Utility.TreeNode
            {
                Value = c.ArticalCatID,
                Display = c.ArticalCatName,
                Parent = c.ParentID
            }).ToList();

            Utility.BuildTree(ddlNodeList, 0, ref ddl, 0);

            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (selectedValue.HasValue)
                    ddl.SelectedValue = selectedValue.ToString();
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn chuyên mục -", "0"));
                }
            }
        }
        public static void bindArticalCatNonFixToDdlTree(ref DropDownList ddl, int? selectedValue, bool required)
        {
            var dc = new SmartShopEntities();
            ddl.Items.Clear();
            var query = GetAllItem().Where(e => e.IsFix != true).OrderBy(c => c.Order);
            var ddlNodeList = query.Select(c => new Utility.TreeNode
            {
                Value = c.ArticalCatID,
                Display = c.ArticalCatName,
                Parent = c.ParentID
            }).ToList();

            Utility.BuildTree(ddlNodeList, 0, ref ddl, 0);

            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (selectedValue.HasValue)
                    ddl.SelectedValue = selectedValue.ToString();
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn chuyên mục -", "0"));
                }
            }
        }
        public static bool Insert(ArticalCategory obj)
        {
            try
            {
                var dc = new SmartShopEntities();
                dc.ArticalCategories.Add(obj);
                dc.SaveChanges();
                return true;
            }
            catch (Exception e)
            { return false; }
        }

        public static bool Update(ArticalCategory obj)
        {
            try
            {
                var dc = new SmartShopEntities();
                var AC = dc.ArticalCategories.FirstOrDefault(e => e.ArticalCatID == obj.ArticalCatID);
                AC.ArticalCatName = obj.ArticalCatName;
                AC.ParentID = obj.ParentID;
                AC.Order = obj.Order;
                dc.SaveChanges();
                return true;
            }
            catch (Exception e)
            { return false; }
        }
        public static bool Delete(int ArticalCatID)
        {
            try
            {
                var dc = new SmartShopEntities();
                var AC = dc.ArticalCategories.FirstOrDefault(e => e.ArticalCatID == ArticalCatID);
                dc.ArticalCategories.Remove(AC);
                dc.SaveChanges();
                return true;
            }
            catch (Exception e)
            { return false; }
        }


    }
}