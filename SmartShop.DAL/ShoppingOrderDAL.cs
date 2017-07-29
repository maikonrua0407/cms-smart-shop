using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

/// <summary>
/// Summary description for ShoppingOrderDAL
/// </summary>
namespace SmartShop.DAL
{
    public class ShoppingOrderDAL
    {
        public ShoppingOrderDAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static ShoppingOrder GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.ShoppingOrders.SingleOrDefault(c => c.OrderID == id);
        }
        public static IQueryable<ShoppingOrder> GetAllItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.ShoppingOrders.AsQueryable();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static IQueryable<ShoppingOrder> GetExist_Ma(string Ma_Exist)
        {
            try
            {
                var dc = new SmartShopEntities();
                var list = dc.ShoppingOrders.Where(n => n.OrderCode != Ma_Exist);
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
                var list = listExist.Where(n => n.OrderCode == Ma_From);
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
                return dc.ShoppingOrders.Where(So => So.OrderCode == Main).Count() > 0;
            }
            catch
            {
                return false;
            }
        }
        public static bool Insert(ShoppingOrder obj, List<ShoppingOrderDetail> lstDetail, ref string pOrderCode)
        {
            try
            {
                var dc = new SmartShopEntities();
                using (TransactionScope trans = new TransactionScope())
                {
                    ShoppingOrder so = new ShoppingOrder();
                    so = obj;
                    dc.ShoppingOrders.Add(so);
                    dc.SaveChanges();
                    so.OrderCode = "DH" + so.OrderID;
                    dc.SaveChanges();
                    var objO = dc.ShoppingOrders.OrderByDescending(c => c.OrderID).Take(1).SingleOrDefault();

                    StockOrder StO = new StockOrder();
                    StO.ApprovedBy = "Auto";
                    StO.ApprovedDate = DateTime.Today;
                    StO.CreateBy = "Auto";
                    StO.CreateDate = DateTime.Today;
                    StO.Date = DateTime.Today;
                    StO.Deliverer = "Auto";
                    StO.EffectType = true;
                    StO.ModifiedBy = "Auto";
                    StO.ModifiedDate = DateTime.Today;
                    StO.PeriodID = dc.InventoryPeriods.Select(e => e.PeriodID).Max();
                    StO.Receiver = "Customer";
                    StO.ShoppingOrderID = so.OrderID;
                    StO.StockOrderType=1;
                    dc.StockOrders.Add(StO);
                    dc.SaveChanges();
                    StO.StockOrderCode = "XK" + StO.StockOrderID;
                    dc.SaveChanges();
                    foreach (var item in lstDetail)
                    {
                        ShoppingOrderDetail objDt = new ShoppingOrderDetail();
                        objDt.OrderID = objO.OrderID;
                        objDt.ProductID = item.ProductID;
                        objDt.Price = item.Price;
                        objDt.Amount = item.Amount;
                        objDt.Discount = item.Discount;
                        dc.ShoppingOrderDetails.Add(objDt);
                        dc.SaveChanges();

                        StockOrderDetail soDetail = new StockOrderDetail();
                        soDetail.ProductID = item.ProductID;
                        soDetail.Quantity = item.Amount;
                        soDetail.Reason = "Xuất cho HĐ đặt hàng";
                        soDetail.StockOrderID = StO.StockOrderID;
                        dc.StockOrderDetails.Add(soDetail);
                        dc.SaveChanges();
                    }

                    pOrderCode = so.OrderCode;
                    trans.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Update(ShoppingOrder objOrder, List<ShoppingOrderDetail> lstDetail)
        {
            try
            {
                var dc = new SmartShopEntities();
                using (TransactionScope trans = new TransactionScope())
                {
                    var obj = dc.ShoppingOrders.FirstOrDefault(c => c.OrderID == objOrder.OrderID);
                    StockOrder StO = new StockOrder();
                    if (obj != null)
                    {
                        obj.OrderID = objOrder.OrderID;
                        //obj.OrderCode = objOrder.OrderCode;
                        obj.OrderDate = objOrder.OrderDate;
                        obj.RequiredDate = objOrder.RequiredDate;
                        obj.ShippedDate = objOrder.ShippedDate;
                        obj.Total = objOrder.Total;
                        obj.ShipAddress = objOrder.ShipAddress;
                        obj.ShipCity = objOrder.ShipCity;
                        obj.ShipCountry = objOrder.ShipCountry;
                        obj.Status = objOrder.Status;
                        dc.SaveChanges();

                        IQueryable<StockOrder> SO;
                        if (obj.Status == 2)
                            SO = dc.StockOrders.Where(e => e.ShoppingOrderID == obj.OrderID && e.StockOrderType==3);
                        else
                            SO = dc.StockOrders.Where(e => e.ShoppingOrderID == obj.OrderID && e.StockOrderType == 1);
                        if (SO.Any())
                            StO = SO.FirstOrDefault();
                        else
                        {
                            StO.ApprovedBy = "Auto";
                            StO.ApprovedDate = DateTime.Today;
                            StO.CreateBy = "Auto";
                            StO.CreateDate = DateTime.Today;
                            StO.Date = DateTime.Today;
                            StO.Deliverer = "Auto";
                            StO.EffectType = true;
                            StO.ModifiedBy = "Auto";
                            StO.ModifiedDate = DateTime.Today;
                            StO.PeriodID = dc.InventoryPeriods.Select(e => e.PeriodID).Max();
                            StO.Receiver = "Customer";
                            StO.ShoppingOrderID = obj.OrderID;
                            dc.StockOrders.Add(StO);
                            dc.SaveChanges();
                            if (obj.Status == 2)
                            {
                                StO.StockOrderType = 3;
                                StO.StockOrderCode = "NK" + StO.StockOrderID;
                            }
                            else
                            {
                                StO.StockOrderType = 1;
                                StO.StockOrderCode = "XK" + StO.StockOrderID;
                            }
                            dc.SaveChanges();
                        }
                    }
                    var lstExist = dc.ShoppingOrderDetails.Where(c => c.OrderID == objOrder.OrderID);
                    var lst_Del = lstExist.Where(b => !lstDetail.Select(c => c.OrderID).Contains(b.OrderID));
                    foreach (var item in lst_Del)
                        dc.ShoppingOrderDetails.Remove(item);
                    dc.SaveChanges();
                    if (lstDetail.Count() > 0)
                    {
                        foreach (var item in lstDetail)
                        {
                            var objDt = dc.ShoppingOrderDetails.SingleOrDefault(c => c.OrderDetailID == item.OrderDetailID);
                            if (objDt != null)
                            {
                                objDt.OrderID = objOrder.OrderID;
                                objDt.ProductID = item.ProductID;
                                objDt.Price = item.Price;
                                objDt.Amount = item.Amount;
                                objDt.Discount = item.Discount;
                                dc.SaveChanges();
                            }
                            else
                            {
                                var objInsert = new ShoppingOrderDetail();
                                objInsert.OrderID = objOrder.OrderID;
                                objInsert.ProductID = item.ProductID;
                                objInsert.Price = item.Price;
                                objInsert.Amount = item.Amount;
                                objInsert.Discount = item.Discount;
                                dc.ShoppingOrderDetails.Add(objInsert);
                                dc.SaveChanges();
                            }
                            StockOrderDetail soDetail = dc.StockOrderDetails.FirstOrDefault(e => e.StockOrderID == StO.StockOrderID && e.ProductID == item.ProductID);
                            if (soDetail != null)
                            {
                                soDetail.ProductID = item.ProductID;
                                soDetail.Quantity = item.Amount;
                                soDetail.StockOrderID = StO.StockOrderID;
                                dc.SaveChanges();
                            }
                            else
                            {
                                soDetail = new StockOrderDetail();
                                soDetail.ProductID = item.ProductID;
                                soDetail.Quantity = item.Amount;
                                if (obj.Status == 2)
                                    soDetail.Reason = "Nhập bù cho HĐ đặt hàng bị hủy";
                                else
                                    soDetail.Reason = "Xuất cho HĐ đặt hàng";
                                soDetail.StockOrderID = StO.StockOrderID;
                                dc.StockOrderDetails.Add(soDetail);
                                dc.SaveChanges();
                            }
                        }
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
        public static bool Process(int pOrderID)
        { 
            try
            {
                var dc = new SmartShopEntities();
                var order = dc.ShoppingOrders.FirstOrDefault(e => e.OrderID == pOrderID);
                order.Status = 1;
                dc.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<vwShoppingOrder_Member> GetP_vwShoppingOrder_Member_SearchPaging(string userName, int memberGroupID, string keyword, DateTime? FromDate, DateTime? ToDate, DateTime? OrderInDate, int? Status, int pageSize, int currentPage, ref int? totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.P_vwShoppingOrder_Member_SearchPaging(userName, memberGroupID, keyword, FromDate, ToDate, OrderInDate, Status, pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord",totalRecord)).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<vwShoppingOrder_Member> GetP_vwShoppingOrder_User_SearchPaging(string userName, int? Status, int pageSize, int currentPage, ref int? totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.P_vwShoppingOrder_User_SearchPaging(userName, Status, pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord",totalRecord)).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static bool Delete_ID(int id)
        {
            try
            {
                var dc = new SmartShopEntities();
                using (TransactionScope trans = new TransactionScope())
                {
                    var lstDetail = dc.ShoppingOrderDetails.Where(c => c.OrderID == id);
                    foreach(var item in lstDetail)
                        dc.ShoppingOrderDetails.Remove(item);
                    var obj = dc.ShoppingOrders.SingleOrDefault(c => c.OrderID == id);
                    dc.ShoppingOrders.Remove(obj);
                    dc.SaveChanges();
                    trans.Complete();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static IQueryable<vwShoppingOrder_Member> GetvwShoppingOrder_Member_Report(int memberGroupID, string keyword, DateTime? FromDate, DateTime? ToDate, DateTime? OrderInDate, int? Status)
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.vwShoppingOrder_Member.AsQueryable();
                if (memberGroupID > 0)
                {
                    lst = lst.Where(c => c.MemberGroupID == memberGroupID);
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    lst = lst.Where(c => c.AcountLogin.ToLower().Contains(keyword)
                                        || c.OrderDate.ToString().ToLower().Contains(keyword)
                                        || c.RequiredDate.ToString().ToLower().Contains(keyword)
                                        || c.ShippedDate.ToString().ToLower().Contains(keyword)
                                        || c.Total.ToString().ToLower().Contains(keyword)
                                        || c.ShipAddress.ToString().ToLower().Contains(keyword)
                                        || c.ShipCity.ToString().ToLower().Contains(keyword)
                                        || c.ShipCountry.ToString().ToLower().Contains(keyword)
                                        || c.Status.ToString().ToLower().Contains(keyword)
                                           );
                }
                if (!string.IsNullOrEmpty(FromDate.ToString()))
                {
                    lst = lst.Where(c => c.OrderDate >= FromDate);
                }
                if (!string.IsNullOrEmpty(ToDate.ToString()))
                {
                    lst = lst.Where(c => c.OrderDate <= ToDate);
                }
                if (!string.IsNullOrEmpty(OrderInDate.ToString()))
                {
                    lst = lst.Where(c => c.OrderDate.Value.ToString("MM/dd/yyyy") == OrderInDate.Value.ToString("MM/dd/yyyy"));
                }
                if (Status > 0)
                {
                    lst = lst.Where(c => c.Status == Status);
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
