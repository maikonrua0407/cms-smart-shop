using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.UI
{
    public static class ArticalSvr
    {
        public static Artical GetByID(int id)
        {
            return ArticalDAL.GetByID(id);
        }

        public static IQueryable<Artical> GetAllItem()
        {
            return ArticalDAL.GetAllItem();
        }

        public static IQueryable<Artical> GetSameCat(int id)
        {
            return ArticalDAL.GetSameCat(id);
        }

        public static bool Insert(Artical obj)
        {
            return ArticalDAL.Insert(obj);
        }

        public static bool Update(Artical obj)
        {
            return ArticalDAL.Update(obj);
        }

        public static IQueryable<Artical> GetExist_Ma(int ArticalCatID, string Ma_Exist)
        {
            return ArticalDAL.GetExist_Ma(ArticalCatID, Ma_Exist);
        }

        public static bool checkExits_Ma_Update(int ArticalCatID, string Ma_Exist, string Ma_From)
        {
            return ArticalDAL.checkExits_Ma_Update(ArticalCatID, Ma_Exist, Ma_From);
        }

        public static bool CheckExist_Ma_Insert(int ArticalCatID, string Main)
        {
            return ArticalDAL.CheckExist_Ma_Insert(ArticalCatID, Main);
        }

        public static bool Delete(int id)
        {
            return ArticalDAL.Delete_ID(id);
        }

        public static List<vwArtical> GetP_vwArtical_SearchPaging(string keyword, int? ArticalCatID, DateTime? FromDate, DateTime? ToDate, int pageSize, int currentPage, ref int? totalRecord)
        {
            return ArticalDAL.GetP_vwArtical_SearchPaging(keyword, ArticalCatID, FromDate, ToDate, pageSize, currentPage, ref totalRecord);
        }

        public static List<Artical> GetTopTen(int count)
        {
            return ArticalDAL.GetTopTen(count);
        }
    }
}