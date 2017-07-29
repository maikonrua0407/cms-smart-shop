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
    public class ThongTinDAL
    {
        public static HT_THONG_TIN GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.HT_THONG_TIN.SingleOrDefault(c => c.ID == id);
        }

        public static IQueryable<HT_THONG_TIN> GetAllItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.HT_THONG_TIN.AsQueryable();
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
                var obj = dc.HT_THONG_TIN.SingleOrDefault(c => c.ID == id);
                dc.HT_THONG_TIN.Remove(obj);
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