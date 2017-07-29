using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace SmartShop.DAL
{
    public class ArticalDAL
    {
        public static Artical GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.Articals.SingleOrDefault(c => c.ArticalID == id);
        }
        public static IQueryable<Artical> GetAllItem()
        {
            var dc = new SmartShopEntities();
            return dc.Articals.AsQueryable();
        }
        public static IQueryable<Artical> GetSameCat(int pArticalCatID)
        {
            var dc = new SmartShopEntities();
            var cat = dc.ArticalCategories.Where(e => e.ParentID == pArticalCatID).Select(e => e.ArticalCatID);
            if (cat == null)
                return dc.Articals.Where(e => e.ArticalCatID == pArticalCatID).AsQueryable();
            else
                return dc.Articals.Where(e => e.ArticalCatID == pArticalCatID || cat.Contains(e.ArticalCatID)).AsQueryable();
        }
        public static bool Insert(Artical obj)
        {
            try
            {

                string content = Regex.Replace((Regex.Replace(obj.ArticalContent, "<div.*?>", string.Empty)), "</div.*?>", string.Empty);
                //content = Regex.Replace((Regex.Replace(content, "<font.*?>", string.Empty)), "</font.*?>", string.Empty);
                //content = Regex.Replace((Regex.Replace(content, "<span.*?>", string.Empty)), "</span.*?>", string.Empty);
                obj.ArticalContent = content;
                var dc = new SmartShopEntities();
                dc.Articals.Add(obj);
                dc.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Update(Artical objArtical)
        {
            try
            {
                var dc = new SmartShopEntities();
                var obj = dc.Articals.SingleOrDefault(c => c.ArticalID == objArtical.ArticalID);
                if (obj != null)
                {
                    obj.ArticalCatID = objArtical.ArticalCatID;
                    obj.Title = objArtical.Title;
                    obj.Summery = objArtical.Summery;
                    string content = Regex.Replace((Regex.Replace(objArtical.ArticalContent, "<div.*?>", string.Empty)), "</div.*?>", string.Empty);
                    //content = Regex.Replace((Regex.Replace(content, "<font.*?>", string.Empty)), "</font.*?>", string.Empty);
                    //content = Regex.Replace((Regex.Replace(content, "<span.*?>", string.Empty)), "</span.*?>", string.Empty);
                    //content = Regex.Replace((Regex.Replace(content, "<p.*?>", string.Empty)), "</p.*?>", string.Empty);
                    obj.ArticalContent = content;
                    if (!string.IsNullOrEmpty(objArtical.MasterImage))
                        obj.MasterImage = objArtical.MasterImage;
                    if (!string.IsNullOrEmpty(objArtical.ResizeImage))
                        obj.ResizeImage = objArtical.ResizeImage;
                    obj.Member = objArtical.Member;
                    obj.PublishDate = objArtical.PublishDate;
                    obj.Tag = objArtical.Tag;
                    obj.Active = objArtical.Active;
                    dc.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static IQueryable<Artical> GetExist_Ma(int ArticalCatID, string Ma_Exist)
        {
            try
            {
                var dc = new SmartShopEntities();
                var list = dc.Articals.Where(c => c.Title != Ma_Exist && c.ArticalCatID != ArticalCatID);
                return list.AsQueryable();
            }
            catch
            {
                return null;
            }
        }
        public static bool checkExits_Ma_Update(int ArticalCatID, string Ma_Exist, string Ma_From)
        {
            try
            {
                var listExist = GetExist_Ma(ArticalCatID, Ma_Exist);
                var list = listExist.Where(c => c.Title == Ma_From && c.ArticalCatID == ArticalCatID);
                return list.Count() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool CheckExist_Ma_Insert(int ArticalCatID, string Main)
        {
            try
            {
                var dc = new SmartShopEntities();
                return dc.Articals.Where(c => c.Title == Main && c.ArticalCatID == ArticalCatID).Count() > 0;
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
                var obj = dc.Articals.SingleOrDefault(c => c.ArticalID == id);
                dc.Articals.Remove(obj);
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static List<vwArtical> GetP_vwArtical_SearchPaging(string keyword, int? ArticalCatID, DateTime? FromDate, DateTime? ToDate, int pageSize, int currentPage, ref int? totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.P_vwArtical_SearchPaging(keyword, ArticalCatID, FromDate, ToDate, pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord",totalRecord)).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<Artical> GetTopTen(int count)
        {
            var dc = new SmartShopEntities();
            return dc.Articals.Where(e => e.Active).OrderBy(e => e.ArticalID).OrderByDescending(e => e.ArticalID).Take(count).ToList();
        }
    }
}