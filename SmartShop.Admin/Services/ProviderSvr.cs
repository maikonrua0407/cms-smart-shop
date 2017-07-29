using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.Admin
{
    public static class ProviderSvr
    {
        public static Provider GetByID(int id)
        {
            return ProviderDAL.GetByID(id);
        }

        public static IQueryable<Provider> GetAllItem()
        {
            return ProviderDAL.GetAllItem();
        }

        public static bool Delete(int id)
        {
            return ProviderDAL.Delete_ID(id);
        }

        public static IQueryable<Provider> GetProvider_Paging(string keyword, int pageSize, int currentPage, ref int totalRecord)
        {
            return ProviderDAL.GetProvider_Paging(keyword, pageSize, currentPage, ref totalRecord);
        }
    }
}