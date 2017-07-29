using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Transactions;

namespace SmartShop.DAL
{
    public class CommentDAL
    {
        public static Comment GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.Comments.SingleOrDefault(c => c.CommentID == id);
        }
        public static IQueryable<Comment> Get(int pParentID, int pProductSetID, int pType)
        {
            var dc = new SmartShopEntities();
            IQueryable<Comment> cm = dc.Comments.AsQueryable();
            if (pParentID > 0)
                cm = from c in cm
                     where c.Parent == pParentID
                     select c;
            if (pProductSetID > 0)
                cm = from c in cm
                     where c.ProductSetID == pProductSetID
                     select c;
            if (pType > 0)
                cm = from c in cm
                     where c.Type == pType
                     select c;
            return cm;

        }
        public static IQueryable<Comment> GetAllItem()
        {
            var dc = new SmartShopEntities();
            return dc.Comments.AsQueryable();
        }
        public static bool Insert(Comment obj)
        {
            try
            {
                var dc = new SmartShopEntities();
                string content = Regex.Replace((Regex.Replace(obj.Content, "<font.*?>", string.Empty)), "</font.*?>", string.Empty);
                content = Regex.Replace((Regex.Replace(content, "<div.*?>", string.Empty)), "</div.*?>", string.Empty);
                content = Regex.Replace((Regex.Replace(content, "<span.*?>", string.Empty)), "</span.*?>", string.Empty);
                obj.Content = content;
                dc.Comments.Add(obj);
                dc.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Update(Comment objComment)
        {
            try
            {
                var dc = new SmartShopEntities();
                var obj = dc.Comments.SingleOrDefault(c => c.CommentID == objComment.CommentID);
                if (obj != null)
                {
                    string content = Regex.Replace((Regex.Replace(obj.Content, "<font.*?>", string.Empty)), "</font.*?>", string.Empty);
                    content = Regex.Replace((Regex.Replace(content, "<div.*?>", string.Empty)), "</div.*?>", string.Empty);
                    content = Regex.Replace((Regex.Replace(content, "<span.*?>", string.Empty)), "</span.*?>", string.Empty);
                    obj.Content = content;
                    obj.CommentID = objComment.CommentID;
                    obj.Title = objComment.Title;
                    obj.ComentBy = "Admin";
                    obj.Email = objComment.Email;
                    obj.Date = objComment.Date;
                    obj.Type = objComment.Type;
                    obj.ProductSetID = objComment.ProductSetID;
                    obj.AdminView = objComment.AdminView;
                    obj.Active = objComment.Active;
                    dc.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool InSert_Update(Comment objComment)
        {
            try
            {
                var dc = new SmartShopEntities();
                using (TransactionScope trans = new TransactionScope())
                {
                    string content = Regex.Replace((Regex.Replace(objComment.Content, "<font.*?>", string.Empty)), "</font.*?>", string.Empty);
                    content = Regex.Replace((Regex.Replace(content, "<div.*?>", string.Empty)), "</div.*?>", string.Empty);
                    content = Regex.Replace((Regex.Replace(content, "<span.*?>", string.Empty)), "</span.*?>", string.Empty);
                    objComment.Content = content;
                    dc.Comments.Add(objComment);
                    dc.SaveChanges();
                    var obj = dc.Comments.SingleOrDefault(c => c.CommentID == objComment.CommentID);
                    if (obj != null)
                    {
                        obj.AdminView = 1;
                        dc.SaveChanges();
                    }
                    trans.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static IQueryable<Comment> GetExist_Ma(int CommentCatID, string Ma_Exist)
        {
            try
            {
                var dc = new SmartShopEntities();
                var list = dc.Comments.Where(c => c.Title != Ma_Exist && c.CommentID != CommentCatID);
                return list.AsQueryable();
            }
            catch
            {
                return null;
            }
        }
        public static bool checkExits_Ma_Update(int CommentCatID, string Ma_Exist, string Ma_From)
        {
            try
            {
                var listExist = GetExist_Ma(CommentCatID, Ma_Exist);
                var list = listExist.Where(c => c.Title == Ma_From && c.CommentID == CommentCatID);
                return list.Count() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool CheckExist_Ma_Insert(int CommentCatID, string Main)
        {
            try
            {
                var dc = new SmartShopEntities();
                return dc.Comments.Where(c => c.Title == Main && c.CommentID == CommentCatID).Count() > 0;
            }
            catch
            {
                return false;
            }
        }
        public static bool Delete_ID(int id)
        {
            try
            {
                var dc = new SmartShopEntities();
                var obj = dc.Comments.SingleOrDefault(c => c.CommentID == id);
                dc.Comments.Remove(obj);
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void bindTypeToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = Type.ListType();
            ddl.DataTextField = "name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn kiểu Comment -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
        public static List<vwComment> GetP_vwComment_SearchPaging(string keyword, int? Type, int? AdminView, DateTime? FromDate, DateTime? ToDate, int pageSize, int currentPage, ref int? totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.P_vwComment_SearchPaging(keyword, Type, AdminView, FromDate, ToDate, pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord",totalRecord)).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<Comment> GetP_Comment_SearchPaging(int? ProductSetID, int? Type, int pageSize, int currentPage, ref int? totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.P_Comment_SearchPaging(ProductSetID, Type, pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord",totalRecord)).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<Comment> GetByProductSetID(int pProductSetID)
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.Comments.Where(e => e.ProductSetID == pProductSetID).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}