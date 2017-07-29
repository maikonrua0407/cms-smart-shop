using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Transactions;

namespace SmartShop.DAL
{
    public class StockDAL
    {
        DataTable dt;
        SmartShopEntities db = new SmartShopEntities();
        public int CreateNewPeriod(string pName)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required, System.TimeSpan.MaxValue))
                {
                    DateTime today = DateTime.Now;
                    var period = (from p in db.InventoryPeriods select p.PeriodID).Max();
                    var oldPeriod = (from p in db.InventoryPeriods
                                     where p.PeriodID == period
                                     select p).FirstOrDefault();
                    oldPeriod.EndDate = today;
                    db.SaveChanges();
                    InventoryPeriod newPeriod = new InventoryPeriod
                    {
                        PeriodName = pName,
                        BeginDate = today
                    };
                    db.InventoryPeriods.Add(newPeriod);
                    db.SaveChanges();
                    trans.Complete();
                    return newPeriod.PeriodID;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int GetCurrentPeriod()
        {
            var period = from p in db.InventoryPeriods select p.PeriodID;
            return period.Max();
        }
        public InventoryPeriod GetPeriodByID(int pPeriodID)
        {
            var period = (from p in db.InventoryPeriods where p.PeriodID == pPeriodID select p).FirstOrDefault();
            return period;
        }
        public List<InventoryPeriod> GetAllPeriod()
        {
            var period = from p in db.InventoryPeriods
                         orderby p.PeriodID descending
                         select p;
            return period.ToList();
        }
        public int GetIndentityPeriod()
        {
            var period = from p in db.InventoryPeriods
                         select p;
            return period.Count();
        }
        public DataTable GetAllInputByPeriod(IQueryable<Product> pro, int pPeriodID, bool InOut)
        {
            var StockOrders = from o in db.StockOrders
                              where o.PeriodID == pPeriodID
                              select o;

            if (InOut == true)
            {
                StockOrders = from o in StockOrders
                              where o.StockOrderType == 1 || o.StockOrderType == 3
                              select o;
            }
            else
            {
                StockOrders = from o in StockOrders
                              where o.StockOrderType == 2
                              select o;
            }
            dtInitial();
            foreach (var pStockOrder in StockOrders)
            {
                var odd = from od in db.StockOrderDetails
                          where od.StockOrderID == pStockOrder.StockOrderID
                          select od;
                if (pro != null)
                {
                    int[] proID = new int[pro.Count()];
                    int c = 0;
                    foreach (var i in pro)
                    {
                        proID[c] = i.ProductID;
                        c = c + 1;
                    }
                    odd = from o in odd
                          where proID.Contains(o.ProductID.Value)
                          select o;
                }
                foreach (var od in odd)
                {
                    string ProductCode = "", ProductName = "", UnitName = "";
                    int ProductID = 0;
                    double Quantity = 0;
                    //{
                    Product Product = ProductDAL.GetByID(od.ProductID.Value);
                    if (Product != null)
                    {
                        if (ProductID == 0)
                            ProductID = Product.ProductID;
                        if (string.IsNullOrEmpty(ProductCode))
                            ProductCode = Product.ProductCode;
                        if (string.IsNullOrEmpty(ProductName))
                            ProductName = Product.ProductName;
                        if (string.IsNullOrEmpty(UnitName))
                            UnitName = ProductUnit.Name(ProductDAL.GetProductSetByProductID(ProductID).ProductUnit.ToString());
                        Quantity = od.Quantity.Value;
                        bool exist = true;
                        for (int r = 0; r < dt.Rows.Count; r++)
                        {
                            if (Convert.ToInt32(dt.Rows[r][1]) == od.ProductID)
                            {
                                dt.Rows[r][5] = Convert.ToDouble(dt.Rows[r][5]) + Quantity;
                                exist = false;
                            }
                        }
                        if (exist)
                        {
                            dt.NewRow();
                            dt.Rows.Add(new object[] { dt.Rows.Count + 1, ProductID, ProductCode, ProductName, UnitName, Quantity });
                        }
                    }
                }
            }
            return dt;

        }
        public DataTable GetQuantityInStock(int pPeriodID, string pKeyword, int pageSize, int currentPage, ref int totalRecord)
        {
            IQueryable<Product> pro = ProductDAL.GetAllItem(pKeyword);
            DataTable dtIn = GetAllInputByPeriod(pro, pPeriodID, true);
            DataTable dtOut = GetAllInputByPeriod(pro, pPeriodID, false);
            int c = dtIn.Columns.Count - 3;
            for (int i = 0; i < dtIn.Rows.Count; i++)
            {
                for (int j = 0; j < dtOut.Rows.Count; j++)
                {
                    if (Convert.ToInt32(dtOut.Rows[j][1]) == Convert.ToInt32(dtIn.Rows[i][1]) && Convert.ToDouble(dtOut.Rows[j][6]) == Convert.ToDouble(dtIn.Rows[i][6]) && Convert.ToDouble(dtOut.Rows[j][7]) == Convert.ToDouble(dtIn.Rows[i][7]))
                    {
                        dtIn.Rows[i][c] = Convert.ToInt32(dtIn.Rows[i][c]) - Convert.ToInt32(dtOut.Rows[j][c]);
                    }
                }
            }
            if (pageSize > 0)
            {
                totalRecord = dtIn.Rows.Count;
                if (pageSize > 0 && currentPage > 0)
                {
                    int start = (currentPage - 1) * pageSize;
                }
            }
            int tong = dtIn.Rows.Count;
            return dtIn;
        }
        public DataTable GetQuantityInStock(int pPeriodID)
        {
            DataTable dtIn = GetAllInputByPeriod(null, pPeriodID, true);
            DataTable dtOut = GetAllInputByPeriod(null, pPeriodID, false);
            int c = dtIn.Columns.Count - 1;
            for (int i = 0; i < dtIn.Rows.Count; i++)
            {
                for (int j = 0; j < dtOut.Rows.Count; j++)
                {
                    if (Convert.ToInt32(dtOut.Rows[j][1]) == Convert.ToInt32(dtIn.Rows[i][1]))
                    {
                        dtIn.Rows[i][c] = Convert.ToInt32(dtIn.Rows[i][c]) - Convert.ToInt32(dtOut.Rows[j][c]);
                    }
                }
            }
            return dtIn;
        }
        public DataTable GetCurentProductInStock(int pProductID)
        {
            DataTable CurentStock = GetQuantityInStock(GetCurrentPeriod());
            DataTable dtCurentProduct = new DataTable();
            foreach (DataColumn c in dt.Columns)
            {
                DataColumn newC = new DataColumn();
                newC.ColumnName = c.ColumnName;
                newC.DataType = c.DataType;
                dtCurentProduct.Columns.Add(newC);
            }
            foreach (DataRow r in CurentStock.Rows)
            {
                if (Convert.ToInt32(r[1]) == pProductID)
                    dtCurentProduct.ImportRow(r);
            }
            return dtCurentProduct;
        }
        public DataTable GetCurentSpecialProductInStock(int pProductID)
        {
            DataTable CurentStock = GetQuantityInStock(GetCurrentPeriod());
            DataTable dtCurentProduct = new DataTable();
            foreach (DataColumn c in dt.Columns)
            {
                DataColumn newC = new DataColumn();
                newC.ColumnName = c.ColumnName;
                newC.DataType = c.DataType;
                dtCurentProduct.Columns.Add(newC);
            }
            foreach (DataRow r in CurentStock.Rows)
            {
                if (Convert.ToInt32(r[1]) == pProductID)
                    dtCurentProduct.ImportRow(r);
            }
            return dtCurentProduct;
        }
        //public bool CreateStock(string pPeriodName)
        //{
        //    try
        //    {
        //        StockOrderDAL OData = new StockOrderDAL();
        //        DateTime today = DateTime.Now;
        //        int NewPeriod = CreateNewPeriod(pPeriodName);
        //        if (NewPeriod > 0)
        //        {
        //            string CreateBy = HttpContext.Current.User.Identity.Name;
        //            int StockOrderID = OData.AddnewStockOrder("IFP" + NewPeriod, 3, true, today, CreateBy, null, null, null);
        //            if (StockOrderID > 0)
        //            {
        //                    DataTable dtIFP = GetQuantityInStock(NewPeriod);
        //                    int countCol = dtIFP.Columns.Count;
        //                    if (dtIFP.Rows.Count > 0)
        //                    {
        //                        for (int i = 0; i < dtIFP.Rows.Count; i++)
        //                        {
        //                            for (int j = 10; j < countCol - 1; j++)
        //                            {
        //                                int quality = 1;
        //                                if (b.BranchID == 2)
        //                                {
        //                                    if (j == 10 || j == 11)
        //                                        quality = 1;
        //                                    else if (j == 12 || j == 13)
        //                                        quality = 1;
        //                                    else if (j == 14 || j == 15)
        //                                        quality = 3;
        //                                    else if (j == 16 || j == 17)
        //                                        quality = 4;
        //                                }
        //                                else
        //                                {
        //                                    if (j == 10 || j == 11)
        //                                        quality = 5;
        //                                    else if (j == 12 || j == 13)
        //                                        quality = 6;
        //                                    else if (j == 14 || j == 15)
        //                                        quality = 7;
        //                                }
        //                                bool fromMinisty = true;
        //                                if (j % 2 == 0)
        //                                    fromMinisty = true;
        //                                else
        //                                    fromMinisty = false;
        //                                if (Convert.ToDouble(dtIFP.Rows[i][j]) != 0)
        //                                    OData.AddnewStockOrderDetail(StockOrderID, Convert.ToInt32(dtIFP.Rows[i][countCol - 1]), quality, fromMinisty, Convert.ToDouble(dtIFP.Rows[i][j]));
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}
        private void dtInitial()
        {
            dt = new DataTable();
            dt.Columns.Add("DetailStockOrderID", typeof(string));
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("ProductCode", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("UnitName", typeof(string));
            dt.Columns.Add("Quantity", typeof(double));
        }
    }
}