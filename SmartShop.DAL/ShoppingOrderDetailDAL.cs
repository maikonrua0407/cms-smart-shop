using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

/// <summary>
/// Summary description for ShoppingOrderDetailDAL
/// </summary>
namespace SmartShop.DAL
{
    public class ShoppingOrderDetailDetailDAL
    {
        public ShoppingOrderDetailDetailDAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static ShoppingOrderDetail GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.ShoppingOrderDetails.SingleOrDefault(c => c.OrderID == id);
        }
        public static IQueryable<ShoppingOrderDetail> GetAllItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.ShoppingOrderDetails.AsQueryable();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static IQueryable<ShoppingOrderDetail> GetBy_OrderID(int orderID)
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.ShoppingOrderDetails.Where(c => c.OrderID == orderID).AsQueryable();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}