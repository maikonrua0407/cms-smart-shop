using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace SmartShop.DAL
{
    public class ProductSetDAL
    {
        public static ProductSet GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.ProductSets.SingleOrDefault(c => c.ProductSetID == id);
        }
        public static IQueryable<ProductSet> GetAllItem()
        {
            var dc = new SmartShopEntities();
            return dc.ProductSets.AsQueryable();
        }      
        public static IQueryable<Member> GetExist_Ma(string Ma_Exist)
        {
            try
            {
                var dc = new SmartShopEntities();
                var list = dc.Members.Where(n => n.AcountLogin != Ma_Exist);
                return list.AsQueryable();
            }
            catch
            {
                return null;
            }
        }
        public static bool checkExits_Ma_Update(string Ma_Exist, string Ma_From)
        {
            try
            {
                var listExist = GetExist_Ma(Ma_Exist);
                var list = listExist.Where(n => n.AcountLogin == Ma_From);
                return list.Count() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool CheckExist_Ma_Insert(string Main)
        {
            try
            {
                var dc = new SmartShopEntities();
                return dc.Members.Where(So => So.AcountLogin == Main).Count() > 0;
            }
            catch
            {
                return false;
            }
        }
        public static bool Insert(ProductSet obj)
        {
            try
            {
                var dc = new SmartShopEntities();
                string content = Regex.Replace((Regex.Replace(obj.Description, "<font.*?>", string.Empty)), "</font.*?>", string.Empty);
                content = Regex.Replace((Regex.Replace(content, "<div.*?>", string.Empty)), "</div.*?>", string.Empty);
                content = Regex.Replace((Regex.Replace(content, "<span.*?>", string.Empty)), "</span.*?>", string.Empty);
                obj.Description = content;
                dc.ProductSets.Add(obj);
                dc.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Update(ProductSet objProductSet)
        {
            try
            {
                var dc = new SmartShopEntities();
                var obj = dc.ProductSets.SingleOrDefault(c => c.ProductSetID == objProductSet.ProductSetID);
                if (obj != null)
                {
                    string content = Regex.Replace((Regex.Replace(obj.Description, "<font.*?>", string.Empty)), "</font.*?>", string.Empty);
                    content = Regex.Replace((Regex.Replace(content, "<div.*?>", string.Empty)), "</div.*?>", string.Empty);
                    content = Regex.Replace((Regex.Replace(content, "<span.*?>", string.Empty)), "</span.*?>", string.Empty);
                    obj.Description = content;
                    obj.ProductSetCode = objProductSet.ProductSetCode;
                    obj.ProductSetName = objProductSet.ProductSetName;
                    obj.CategoryID = objProductSet.CategoryID;
                    obj.ProductGroupID = objProductSet.ProductGroupID;
                    obj.ProductStyleID = objProductSet.ProductStyleID;
                    obj.ProductDesignID = objProductSet.ProductDesignID;
                    obj.ProductMaterialID = objProductSet.ProductMaterialID;
                    obj.ProductTypeID = objProductSet.ProductTypeID;
                    obj.Img = objProductSet.Img;
                    obj.Pocket = objProductSet.Pocket;
                    obj.ProviderID = objProductSet.ProviderID;
                    obj.ForGender = objProductSet.ForGender;
                    obj.Price = objProductSet.Price;
                    obj.PriceSaled = objProductSet.PriceSaled;
                    obj.ProductUnit = objProductSet.ProductUnit;
                    obj.ColorID = objProductSet.ColorID;
                    dc.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Delete_ID(int id)
        {
            try
            {
                var dc = new SmartShopEntities();
                var obj = dc.ProductSets.SingleOrDefault(c => c.ProductSetID == id);
                dc.ProductSets.Remove(obj);
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static List<vwProductSetAllName> GetP_vwProductSetAllName_SearchPaging(string keyword, int categoryid, int productgroupid, int productstyleid, int productmaterialid, int producttypeid, int pocket, int providerid, int forgenderid, int pageSize, int currentPage, ref int? totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var query = dc.P_vwProductSetAllName_SearchPaging(keyword, categoryid, productgroupid, productstyleid, productmaterialid, producttypeid, pocket, providerid, forgenderid, pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord",totalRecord));
                var lst = query.ToList();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static void bindProductSetToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllItem();
            ddl.DataTextField = "ProductSetCode";
            ddl.DataValueField = "ProductSetID";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn bộ sản phẩm -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
        public static IQueryable<ProductSet> GetProductSet_Paging(string keyword, int pageSize, int currentPage, ref int totalRecord)
        {
            var dc = new SmartShopEntities();
            var query = dc.ProductSets.AsQueryable();
            if (!String.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(c => c.ProductSetName.ToLower().Contains(keyword)
                                    || c.ProductSetCode.ToString().ToLower().Contains(keyword)
                                       );
            }
            query = query.OrderBy(n => n.ProductSetCode);
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
