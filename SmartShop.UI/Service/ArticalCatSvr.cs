using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.UI
{
    public static class ArticalCatSvr
    {
        public static ArticalCategory GetByID(int id)
        {
            return ArticalCategoryDAL.GetByID(id);
        }

        public static IQueryable<ArticalCategory> GetAllItem()
        {
            return ArticalCategoryDAL.GetAllItem();
        }

        public static bool Insert(ArticalCategory obj)
        {
            return ArticalCategoryDAL.Insert(obj);
        }

        public static bool Update(ArticalCategory obj)
        {
            return ArticalCategoryDAL.Update(obj);
        }

        public static bool Delete(int id)
        {
            return ArticalCategoryDAL.Delete(id);
        }
    }
}