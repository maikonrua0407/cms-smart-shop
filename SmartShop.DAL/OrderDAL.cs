using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using SmartShop.Utilities;

namespace SmartShop.DAL
{
    public class OrderDAL
    {
        DataTable dt = new DataTable();
        SmartShopEntities db = new SmartShopEntities();
        /// <summary>
        /// Gets all order.
        /// </summary>
        /// <param name="PeriodID">The period ID.</param>
        /// <param name="pOrderType">Type of the p order.</param>
        /// <param name="pEffectType">if set to <c>true</c> [p effect type].</param>
        /// <param name="pFrom">The p from.</param>
        /// <param name="pTo">The p to.</param>
        /// <param name="pCreateBy">The p create by.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalRecord">The total record.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataTable GetAllStockOrder(int PeriodID, int pStockOrderType, bool pEffectType, DateTime pFrom, DateTime pTo, string pCreateBy, int pageSize, int currentPage, ref int totalRecord)
        {
            var StockOrder = from o in db.StockOrders
                             join p in db.InventoryPeriods on o.PeriodID.ToString() equals (p.PeriodID.ToString())
                             where o.StockOrderType == pStockOrderType && o.EffectType == pEffectType
                             select new
                             {
                                 StockOrderID = o.StockOrderID,
                                 StockOrderCode = o.StockOrderCode,
                                 PeriodId = o.PeriodID,
                                 PeriodName = p.PeriodName,
                                 StockOrderType = o.StockOrderType,
                                 EffectType = o.EffectType,
                                 CreateDate = o.CreateDate,
                                 CreateBy = o.CreateBy,
                                 Deliverer = o.Deliverer,
                                 Receiver = o.Receiver,
                                 ModifiedBy = o.ModifiedBy,
                                 ModifiedDate = o.ModifiedDate,
                             };
            if (PeriodID != 0)
            {
                StockOrder = from o in StockOrder
                             where o.PeriodId == PeriodID
                             select o;
            }
            if (!string.IsNullOrEmpty(pFrom.ToString().Trim()))
            {
                StockOrder = from o in StockOrder
                             where o.CreateDate >= pFrom
                             select o;
            }
            if (!string.IsNullOrEmpty(pTo.ToString().Trim()))
            {
                StockOrder = from o in StockOrder
                             where o.CreateDate <= pTo
                             select o;
            }
            if (!string.IsNullOrEmpty(pCreateBy.ToString().Trim()))
            {
                StockOrder = from o in StockOrder
                             where o.CreateBy.Contains(pCreateBy)
                             select o;
            }

            DataTable dt = LTable.ConvertToDataTable(StockOrder.ToList());
            totalRecord = dt.Rows.Count;
            if (pageSize > 0 && currentPage > 0)
            {
                int start = (currentPage - 1) * pageSize;
            }
            return dt;
        }
        /// <summary>
        /// Gets the order by ID.
        /// </summary>
        /// <param name="pOrderID">The p order ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public StockOrder GetStockOrderByID(int pStockOrderID)
        {
            var StockOrder = (from o in db.StockOrders
                              where o.StockOrderID == pStockOrderID
                              select o).FirstOrDefault();
            return StockOrder;
        }
        public int GetLastStockOrderDetial()
        {
            var odd = (from od in db.StockOrderDetails
                       select new { od.StockOrderDetailID }).Max();
            return odd.StockOrderDetailID;
        }
        public int AddnewStockOrder(string pStockOrderCode, int pStockOrderTy, bool pEffectType, DateTime pDate, string pCreateBy, string pDeliverer, string pReceiver, string pDescription)
        {
            try
            {
                StockDAL SData = new StockDAL();
                int CurrentPeriodID = SData.GetCurrentPeriod();
                DateTime now = DateTime.Now;
                var StockOrder = new StockOrder
                {
                    StockOrderCode = pStockOrderCode,
                    StockOrderType = pStockOrderTy,
                    EffectType = pEffectType,
                    PeriodID = CurrentPeriodID,
                    Date = pDate,
                    CreateBy = pCreateBy,
                    CreateDate = DateTime.Now,
                    Deliverer = pDeliverer,
                    Receiver = pReceiver
                };
                db.StockOrders.Add(StockOrder);
                db.SaveChanges();
                return StockOrder.StockOrderID;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public bool UpdateStockOrder(int pStockOrderID, string pStockOrderCode, int pStockOrderTy, bool pEffectType, DateTime pDate, string pCreateBy, string pDeliverer, string pReceiver, string pDescription, DataTable dtProduct)
        {


            try
            {
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required, System.TimeSpan.MaxValue))
                {
                    int CurrentPeriodID = (from p in db.InventoryPeriods select p.PeriodID).Max();
                    DateTime now = DateTime.Now;
                    var StockOrder = (from o in db.StockOrders
                                      where o.StockOrderID == pStockOrderID
                                      select o).FirstOrDefault();

                    StockOrder.StockOrderType = pStockOrderTy;
                    StockOrder.EffectType = pEffectType;
                    StockOrder.PeriodID = CurrentPeriodID;
                    StockOrder.Date = pDate;
                    StockOrder.ModifiedBy = pCreateBy;
                    StockOrder.ModifiedDate = DateTime.Now;
                    StockOrder.Deliverer = pDeliverer;
                    StockOrder.Receiver = pReceiver;

                    db.SaveChanges();
                    var StockOrderItems = from o in db.StockOrderDetails
                                          where o.StockOrderID == pStockOrderID
                                          select o;
                    foreach (var i in StockOrderItems)
                    {
                        db.StockOrderDetails.Remove(i);
                    }
                    db.SaveChanges();

                    for (int i = 0; i < dtProduct.Rows.Count; i++)
                    {
                        int ProductID = Convert.ToInt32(dtProduct.Rows[i][1]);
                        var Product = (from p in db.Products
                                       where p.ProductID == ProductID
                                       select p).FirstOrDefault();

                        if (Convert.ToInt32(dtProduct.Rows[i][5]) > 0)
                        {
                            var OrDetail = new StockOrderDetail
                            {
                                StockOrderID = pStockOrderID,
                                ProductID = ProductID,
                                Quantity = Convert.ToInt32(dtProduct.Rows[i][5])
                            };
                            db.StockOrderDetails.Add(OrDetail);
                            db.SaveChanges();
                        }
                    }
                    trans.Complete();
                }
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool DeleteStockOrder(int pStockOrderID)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required, System.TimeSpan.MaxValue))
                {
                    int CurrentPeriodID = (from p in db.InventoryPeriods select p.PeriodID).Max();
                    DateTime now = DateTime.Now;
                    var StockOrder = (from o in db.StockOrders
                                      where o.StockOrderID == pStockOrderID
                                      select o).FirstOrDefault();
                    var StockOrderItems = from o in db.StockOrderDetails
                                          where o.StockOrderID == pStockOrderID
                                          select o;
                    foreach (var i in StockOrderItems)
                    {
                        db.StockOrderDetails.Remove(i);
                        db.SaveChanges();
                    }
                    db.StockOrders.Remove(StockOrder);
                    db.SaveChanges();
                    trans.Complete();
                }
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool CheckExistStockOrderCode(string pCode)
        {
            var StockOrder = from o in db.StockOrders
                             where o.StockOrderCode.Equals(pCode)
                             select o;
            if (StockOrder.Count() > 0)
                return true;
            else
                return false;
        }
        public int AddnewStockOrderDetail(int pStockOrderID, int pProductID, double pQuantity)
        {
            try
            {
                Product pro = ProductDAL.GetByID(pProductID);
                var StockOrderDetail = new StockOrderDetail
                {
                    StockOrderID = pStockOrderID,
                    ProductID = pProductID,
                    Quantity = pQuantity
                };
                db.StockOrderDetails.Add(StockOrderDetail);
                db.SaveChanges();
                return StockOrderDetail.StockOrderDetailID;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public DataTable GetStockOrderDetailByStockOrder(int pStockOrderID)
        {
            var odd = from od in db.StockOrderDetails
                      where od.StockOrderID == pStockOrderID
                      select od;
            dtInitial();
            foreach (var item in odd)
            {
                string ProductCode = "", ProductName = "", UnitName = ""; double quantity = 0;
                Product Product = ProductDAL.GetByID(item.ProductID.Value);
                ProductCode = Product.ProductCode;
                ProductName = Product.ProductName;
                UnitName = ProductUnit.Name(ProductDAL.GetProductSetByProductID(Product.ProductID).ProductUnit.ToString());
                quantity = item.Quantity.Value;
                dt.NewRow();
                dt.Rows.Add(new object[] { dt.Rows.Count + 1, item.ProductID.Value, ProductCode, ProductName, UnitName, quantity });
            }
            return dt;
        }
        public int CreateStockOrderFirstPeriod(string pNewPeriodName)
        {
            try
            {
                StockDAL SData = new StockDAL();
                int CurrentPeriodID = SData.GetCurrentPeriod();
                SData.CreateNewPeriod(pNewPeriodName);
                int NewStockOrder = AddnewStockOrder("NDK" + (SData.GetIndentityPeriod() + 1), 3, true, DateTime.Today, HttpContext.Current.User.Identity.Name, null, null, null);

                DataTable dtInStock = SData.GetQuantityInStock(CurrentPeriodID);
                int dtInStockCount = dtInStock.Columns.Count;
                foreach (DataRow i in dtInStock.Rows)
                {
                    AddnewStockOrderDetail(NewStockOrder, Convert.ToInt32(i[1]), Convert.ToDouble(i[5]));
                }
                return NewStockOrder;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        private void dtInitial()
        {
            dt = new DataTable();
            dt.Columns.Add("StockOrderDetailID", typeof(string));
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("ProductCode", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("UnitName", typeof(string));
            dt.Columns.Add("Quantity", typeof(double));
        }
    }
}