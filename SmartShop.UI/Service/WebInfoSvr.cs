using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.UI
{
    public static class WebInfoSvr
    {
        public static HT_THONG_TIN GetByID(int id)
        {
            return ThongTinDAL.GetByID(id);
        }

        public static IQueryable<HT_THONG_TIN> GetAllItem()
        {
            return ThongTinDAL.GetAllItem();
        }

        public static bool Delete(int id)
        {
            return ThongTinDAL.Delete_ID(id);
        }
    }
}