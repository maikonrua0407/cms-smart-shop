using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using SmartShop.Utilities;

namespace SmartShop.DAL
{
    public class InventoryDAL
    {
        SmartShopEntities db = new SmartShopEntities();
        public int CreateNewInventory(string pCode, string pInvenBy, DateTime pDate)
        {
            try
            {
                StockDAL SData = new StockDAL();
                int CurrentPeriodID = SData.GetCurrentPeriod();
                DateTime today = DateTime.Now;
                var NewInventory = new Inventory
                {
                    InventoryCode = pCode,
                    PeriodID = CurrentPeriodID,
                    InventoryDate = pDate,
                    InventoryBy = pInvenBy,
                    CreateBy = HttpContext.Current.User.Identity.Name,
                    CreateDate = today
                };
                db.Inventories.Add(NewInventory);
                db.SaveChanges();
                return NewInventory.InventoryID;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int GetCurrentInventory()
        {
            StockDAL SData = new StockDAL();
            int CurrentPeriodID = SData.GetCurrentPeriod();
            var CurrentInventory = from i in db.Inventories
                                   where i.PeriodID == CurrentPeriodID
                                   select i;
            if (CurrentInventory.Count() > 0)
                return CurrentInventory.FirstOrDefault().InventoryID;
            else
                return 0;
        }
        public int CreateNewInventoryDetail(int pInventoryID,
            int pProductID, double pQuantity)
        {
            try
            {
                var IvenDetail = new InventoryDetail
                {
                    InventoryID = pInventoryID,
                    ProductID = pProductID,
                    Quantity = pQuantity
                };
                db.InventoryDetails.Add(IvenDetail);
                db.SaveChanges();
                return IvenDetail.InventoryDetailID;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public bool UpdateInventory(int pInventoryID, string pCode, string pInvenBy, DateTime pDate, DataTable pdt)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required, System.TimeSpan.MaxValue))
                {
                    int CurrentPeriodID = (from p in db.InventoryPeriods select p.PeriodID).Max();
                    DateTime today = DateTime.Now;
                    var oldInventory = (from i in db.Inventories
                                        where i.InventoryID == pInventoryID
                                        select i).FirstOrDefault();
                    oldInventory.InventoryCode = pCode;
                    oldInventory.PeriodID = CurrentPeriodID;
                    oldInventory.InventoryDate = pDate;
                    oldInventory.InventoryBy = pInvenBy;
                    oldInventory.CreateBy = HttpContext.Current.User.Identity.Name;
                    oldInventory.ModifiedDate = today;
                    db.SaveChanges();

                    var oldIvenDetail = from id in db.InventoryDetails
                                        where id.InventoryID == pInventoryID
                                        select id;
                    foreach (var i in oldIvenDetail)
                    {
                        db.InventoryDetails.Remove(i);
                        db.SaveChanges();
                    }
                    for (int i = 0; i < pdt.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(pdt.Rows[i][1].ToString().Trim()))
                        {
                            var NewIvenDetail = new InventoryDetail
                            {
                                InventoryID = pInventoryID,
                                ProductID = Convert.ToInt32(pdt.Rows[i][0].ToString().Trim()),
                                Quantity = Convert.ToInt32(pdt.Rows[i][1].ToString().Trim())
                            };
                            db.InventoryDetails.Add(NewIvenDetail);
                            db.SaveChanges();
                        }
                    }
                    trans.Complete();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool UpdateInventory(int pInventoryID, string pCode, string pInvenBy, DateTime pDate)
        {
            try
            {
                DateTime today = DateTime.Now;
                var oldInventory = (from i in db.Inventories
                                    where i.InventoryID == pInventoryID
                                    select i).FirstOrDefault();
                oldInventory.InventoryCode = pCode;
                oldInventory.InventoryDate = pDate;
                oldInventory.InventoryBy = pInvenBy;
                oldInventory.CreateBy = HttpContext.Current.User.Identity.Name;
                oldInventory.ModifiedDate = today;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public DataTable CompareInventoryWithStock(int pInventoryID)
        {
            StockDAL SData = new StockDAL();
            int CurrentPeriodID = (from p in db.InventoryPeriods select p.PeriodID).Max();
            DataTable dtInStock = SData.GetQuantityInStock(CurrentPeriodID);
            int column = dtInStock.Columns.Count;
            for (int i = 0; i < dtInStock.Rows.Count; i++)
            {
                var inventDetail = from id in db.InventoryDetails
                                   where id.InventoryID == pInventoryID
                                   select id;
                foreach (var inv in inventDetail)
                {
                    if (inv.ProductID == Convert.ToInt32(dtInStock.Rows[i][1]))
                    {
                        dtInStock.Rows[i][5] = Convert.ToDouble(dtInStock.Rows[i][5]) - inv.Quantity;
                    }
                }
                bool SameSystem = true;
                if (Convert.ToInt32(dtInStock.Rows[i][5]) != 0)
                {
                    SameSystem = false;
                }
                if (SameSystem == true)
                    dtInStock.Rows.RemoveAt(i);
            }
            return dtInStock;
        }
        public DataTable GetCompensate(int pInventoryID)
        {
            StockDAL SData = new StockDAL();
            DataTable dtCompensate = new DataTable();
            dtCompensate = DataTabelInitial(dtCompensate);
            int CurrentPeriodID = (from p in db.InventoryPeriods select p.PeriodID).Max();
            DataTable dtInStock = SData.GetQuantityInStock(CurrentPeriodID);
            int column = dtInStock.Columns.Count;
            for (int i = 0; i < dtInStock.Rows.Count; i++)
            {
                var inventDetail = from id in db.InventoryDetails
                                   where id.InventoryID == pInventoryID
                                   select id;

                Product Product = ProductDAL.GetByID(Convert.ToInt32(dtInStock.Rows[i][1]));
                foreach (var inv in inventDetail)
                {
                    if (inv.ProductID == Product.ProductID)
                    {
                        dtInStock.Rows[i][5] = Convert.ToDouble(dtInStock.Rows[i][5]) - inv.Quantity;
                    }
                }
                bool SameSystem = true;
                double quantity = Convert.ToDouble(dtInStock.Rows[i][5]);
                if (quantity > 0)
                {
                    dtCompensate.NewRow();
                    dtCompensate.Rows.Add(new object[] { false, Product.ProductID, Math.Abs(quantity) });
                    SameSystem = false;
                }
                if (quantity < 0)
                {
                    dtCompensate.NewRow();
                    dtCompensate.Rows.Add(new object[] { true, Product.ProductID, Math.Abs(quantity) });
                    SameSystem = false;
                }
                if (SameSystem == true)
                    dtInStock.Rows.RemoveAt(i);
            }
            return dtCompensate;
        }
        public DataTable CompareInventoryWithStock(int pInventoryID, int pageSize, int currentPage, ref int totalRecord)
        {
            DataTable dtInStock = CompareInventoryWithStock(pInventoryID);
            if (pageSize > 0)
            {
                totalRecord = dtInStock.Rows.Count;
                if (pageSize > 0 && currentPage > 0)
                {
                    int start = (currentPage - 1) * pageSize;
                }
            }
            return dtInStock;
        }
        public List<InventoryDetail> GetInventoryDetail(int pInventoryID)
        {
            var InvenDetail = from i in db.InventoryDetails
                              where i.InventoryID == pInventoryID
                              select i;
            return InvenDetail.ToList();
        }
        public DataTable GetInventory(string pInventoryCode, DateTime pFrom, DateTime pTo, int pageSize, int currentPage, ref int totalRecord)
        {
            var Inven = from i in db.Inventories
                        join p in db.InventoryPeriods on i.PeriodID equals (p.PeriodID)
                        select new
                        {
                            _ID = i.InventoryID,
                            _Code = i.InventoryCode,
                            _PerID = i.PeriodID,
                            _PerName = p.PeriodName,
                            _Date = i.InventoryDate,
                            _InvenBy = i.InventoryBy,
                            _CreateBy = i.CreateBy,
                            _CreateDate = i.CreateDate,
                            _ModifiedBy = i.ModifiedBy,
                            _MOdifiedDate = i.ModifiedDate
                        };
            if (!string.IsNullOrEmpty(pInventoryCode))
            {
                Inven = from i in Inven
                        where i._Code.Contains(pInventoryCode)
                        select i;
            }
            if (!string.IsNullOrEmpty(pFrom.ToString()))
            {
                Inven = from i in Inven
                        where i._CreateDate >= pFrom
                        select i;
            }
            if (!string.IsNullOrEmpty(pTo.ToString()))
            {
                Inven = from i in Inven
                        where i._CreateDate <= pTo
                        select i;
            }
            DataTable dt = LTable.ConvertToDataTable(Inven.ToList());
            totalRecord = dt.Rows.Count;
            if (pageSize > 0 && currentPage > 0)
            {
                int start = (currentPage - 1) * pageSize;
            }
            return dt;
        }
        public DataTable DataTabelInitial(DataTable dtCompensate)
        {
            dtCompensate = new DataTable();
            dtCompensate.Columns.Add("UpDown", typeof(bool));
            dtCompensate.Columns.Add("ProductID", typeof(int));
            dtCompensate.Columns.Add("Quantity", typeof(double));
            return dtCompensate;
        }

        public Inventory GetInventoryByID(int pInvenID)
        {
            var Inven = (from i in db.Inventories
                         where i.InventoryID == pInvenID
                         select i).FirstOrDefault();
            return Inven;
        }
    }
}