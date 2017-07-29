using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace SmartShop.DAL
{
    public class ProductDAL
    {
        public static Product GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.Products.SingleOrDefault(c => c.ProductID == id);
        }

        public static Product GetByNameNoSymbol(int idGroup, string strName)
        {
            try
            {
                var dc = new SmartShopEntities();
                List<int> lstGroupId = new List<int>();
                lstGroupId.Add(idGroup);
                GetListProGroup(idGroup, ref lstGroupId);
                Product product = dc.Products.SingleOrDefault(c => c.NoSymbolName.Equals(strName) && lstGroupId.Contains(c.ProductGroupID.Value));
                if (product != null)
                    return product;
                else
                    return dc.Products.SingleOrDefault(c => c.NoSymbolName.Replace("-", "").Equals(strName));
            }
            catch (Exception)
            {
                return null;
            }
        }

        static void GetListProGroup(int parrentId, ref List<int> lstGroupId)
        {
            SmartShopEntities dc = new SmartShopEntities();
            var lstgr = dc.ProductGroups.Where(e => e.GroupParrentID == parrentId).Select(e => e.ProductGroupID);
            if (lstgr != null && lstgr.Count() > 0)
            {
                foreach (var item in lstgr)
                {
                    lstGroupId.Add(item);
                    GetListProGroup(item, ref lstGroupId);
                }
            }
        }

        public static IQueryable<Product> GetAllItem()
        {
            var dc = new SmartShopEntities();
            return dc.Products.AsQueryable();
        }

        public static bool Insert(ref Product objProduct)
        {
            try
            {
                var dc = new SmartShopEntities();
                if (string.IsNullOrWhiteSpace(objProduct.ProductCode))
                    objProduct.ProductCode = "SP_" + (dc.Products.Where(e => !e.ProductCode.Contains("SP_")).Count() + 1);
                objProduct.NoSymbolName = Util.converToUnsign(objProduct.ProductName);
                dc.Products.Add(objProduct);
                dc.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Update(ref Product objProduct)
        {
            try
            {
                var dc = new SmartShopEntities();
                int id = objProduct.ProductID;
                var obj = dc.Products.SingleOrDefault(c => c.ProductID == id);
                if (obj != null)
                {
                    obj.ProductCode = objProduct.ProductCode;
                    obj.ProductName = objProduct.ProductName;
                    obj.ProductSetID = objProduct.ProductSetID;
                    obj.ProductGroupID = objProduct.ProductGroupID;
                    obj.Quantity = objProduct.Quantity;
                    obj.SizeID = objProduct.SizeID;
                    obj.Available = objProduct.Available;
                    obj.NoSymbolName = Util.converToUnsign(obj.ProductName);
                    dc.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IQueryable<vwProductSet_Product> GetvwProductSet_Product_Paging(string keyword, int pageSize, int currentPage, ref int totalRecord)
        {
            var dc = new SmartShopEntities();
            var query = dc.vwProductSet_Product.AsQueryable();
            if (!String.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(c => c.ProductSetName.ToLower().Contains(keyword)
                                    || c.Price.ToString().ToLower().Contains(keyword)
                                    || c.Quantity.ToString().ToLower().Contains(keyword)
                                       );
            }
            query = query.OrderBy(n => n.ProductSetName);
            totalRecord = query.Count();
            if (pageSize > 0 && currentPage > 0)
            {
                int start = (currentPage - 1) * pageSize;
                query = query.Skip(start).Take(pageSize).AsQueryable();
            }
            return query;
        }

        public static IQueryable<vwProductSet_Product> GetvwProductSet_Product(List<int> ListID)
        {
            var dc = new SmartShopEntities();
            var query = dc.vwProductSet_Product.AsQueryable();
            query = query.Where(id => ListID.Contains(id.ProductID)).OrderBy(id => id.ProductSetName);
            return query;
        }

        public static IQueryable<Product> GetExist_Ma(string Ma_Exist)
        {
            try
            {
                var dc = new SmartShopEntities();
                var list = dc.Products.Where(n => n.ProductCode != Ma_Exist);
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
                var list = listExist.Where(n => n.ProductCode == Ma_From);
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
                return dc.Products.Where(So => So.ProductCode == Main).Count() > 0;
            }
            catch
            {
                return false;
            }
        }

        public static List<vwProduct> GetP_vwProduct_SearchPaging(string keyword, int? ProductGroupID, int? ProductSetID, int SizeID, int pageSize, int currentPage, ref int? totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.P_vwProduct_SearchPaging(keyword, ProductGroupID, ProductSetID, SizeID, pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord",totalRecord)).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<vwProductSetAllName> SelectProductsByFindShort(string pKeyword, int pProductGroupID, int pStyleID, int pTyleID, int pDesignID, int pMaterialID, int ProviderID, double pPriceFrom, double pPriceTo, int pShort, int pPageSize, int pCurrentPage, ref int? pTotalRecord)
        {
            var dc = new SmartShopEntities();
            try
            {
                if (pPriceFrom == 0 && pPriceTo == 0)
                    return dc.P_vwProductSet_FindSort(pKeyword, pProductGroupID, pStyleID, pMaterialID, pTyleID, pDesignID, ProviderID, pPriceFrom, pPriceTo, pShort, pPageSize, pCurrentPage, new System.Data.Objects.ObjectParameter("pTotalRecord",pTotalRecord)).ToList();
                else if (pPriceFrom > 0 && pPriceTo > 0)
                    return dc.P_vwProductSet_FindSort_PriceBeetwen(pKeyword, pProductGroupID, pStyleID, pMaterialID, pTyleID, pDesignID, ProviderID, pPriceFrom, pPriceTo, pShort, pPageSize, pCurrentPage, new System.Data.Objects.ObjectParameter("pTotalRecord",pTotalRecord)).ToList();
                else if (pPriceFrom > 0)
                    return dc.P_vwProductSet_FindSort_PriceUp(pKeyword, pProductGroupID, pStyleID, pMaterialID, pTyleID, pDesignID, ProviderID, pPriceFrom, pPriceTo, pShort, pPageSize, pCurrentPage, new System.Data.Objects.ObjectParameter("pTotalRecord",pTotalRecord)).ToList();
                else
                    return dc.P_vwProductSet_FindSort_PriceDown(pKeyword, pProductGroupID, pStyleID, pMaterialID, pTyleID, pDesignID, ProviderID, pPriceFrom, pPriceTo, pShort, pPageSize, pCurrentPage, new System.Data.Objects.ObjectParameter("pTotalRecord",pTotalRecord)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<vwProductSetAllName> SelectProductsByFindShort(string pKeyword, int pProductGroupID, int ProviderID, int pShort)
        {
            var dc = new SmartShopEntities();
            try
            {
                return dc.P_vwProductSet_FindSort_NotPaging(pKeyword, pProductGroupID, ProviderID, pShort).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<vwProductSetAllName> SelectTopNew()
        {
            var dc = new SmartShopEntities();
            try
            {
                return dc.vwProductSetAllNames.OrderBy(e => e.DeliveryDate).Take(8).ToList();
            }
            catch (Exception e)
            { return null; }
        }

        public static List<vwProductSetAllName> SelectTopSame(int pProductSetID)
        {
            var dc = new SmartShopEntities();
            try
            {
                ProductSet proset = ProductSetDAL.GetByID(pProductSetID);
                var proSame = dc.vwProductSetAllNames.Where(e => e.ProductGroupID == proset.ProductGroupID);
                if (proSame.Count() > 8)
                {
                    proSame = proSame.Where(e => e.ProductTypeID == proset.ProductTypeID);
                }
                if (proSame.Count() > 8)
                {
                    proSame = proSame.Where(e => e.ProductStyleID == proset.ProductStyleID);
                }
                if (proSame.Count() > 8)
                {
                    proSame = proSame.Where(e => e.ProviderID == proset.ProviderID);
                }
                if (proSame.Count() > 8)
                {
                    proSame = proSame.Where(e => e.ColorID == proset.ColorID);
                }
                return proSame.OrderBy(e => e.DeliveryDate).Take(8).ToList();
            }
            catch (Exception e)
            { return null; }
        }

        public static List<vwProductSetAllName> SelectTopTutorial()
        {
            var dc = new SmartShopEntities();
            try
            {
                List<vwProductSetAllName> sale = dc.vwProductSetAllNames.ToList();
                for (int i = 0; i < sale.Count; i++)
                {
                    if (i > 0)
                    {
                        var curent = sale.ElementAt(i);
                        for (int j = 0; j < i; j++)
                        {
                            var last = sale.ElementAt(j);
                            if (curent.PriceSaled / curent.Price > last.PriceSaled / last.Price)
                            {
                                var temp = new vwProductSetAllName();
                                temp = curent;
                                curent = last;
                                last = temp;
                                break;
                            }
                        }
                    }
                }
                return sale.Take(8).ToList();
            }
            catch (Exception e)
            { return null; }
        }

        public static bool Delete(int id)
        {
            try
            {
                var dc = new SmartShopEntities();
                var obj = dc.Products.SingleOrDefault(c => c.ProductID == id);
                dc.Products.Remove(obj);
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Product GetByCode(string strCode)
        {
            var dc = new SmartShopEntities();
            return dc.Products.SingleOrDefault(c => c.ProductCode.Equals(strCode));
        }

        public static IQueryable<Product> GetAllItem(string strKeyWord)
        {
            var dc = new SmartShopEntities();
            if (!string.IsNullOrEmpty(strKeyWord))
                return dc.Products.Where(e => e.ProductCode.ToUpper().Contains(strKeyWord.Trim().ToUpper()) || e.ProductName.ToUpper().Contains(strKeyWord.Trim().ToUpper())).AsQueryable();
            else
                return dc.Products.AsQueryable();
        }

        public static IQueryable<vwProduct> SelectAll()
        {
            var dc = new SmartShopEntities();
            return dc.vwProducts.AsQueryable();
        }

        public static ProductSet GetProductSetByProductID(int id)
        {
            var dc = new SmartShopEntities();
            int productSetID = dc.Products.SingleOrDefault(e => e.ProductID == id).ProductSetID.Value;
            return dc.ProductSets.SingleOrDefault(c => c.ProductSetID == productSetID);
        }

        public static bool CheckProductCodeExist(string strCode)
        {
            var dc = new SmartShopEntities();
            if (dc.Products.Where(p => p.ProductCode.Contains(strCode)).Count() > 0)
                return true;
            else
                return false;
        }

        public static IQueryable<Product> GetProduct_Paging(string keyword, int pageSize, int currentPage, ref int totalRecord)
        {
            var dc = new SmartShopEntities();
            var query = dc.Products.AsQueryable();
            if (!String.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(c => c.ProductName.ToLower().Contains(keyword)
                                    || c.ProductCode.ToString().ToLower().Contains(keyword)
                                       );
            }
            query = query.OrderBy(n => n.ProductCode);
            totalRecord = query.Count();
            if (pageSize > 0 && currentPage > 0)
            {
                int start = (currentPage - 1) * pageSize;
                query = query.Skip(start).Take(pageSize).AsQueryable();
            }
            return query;
        }

        public static void bindProductQuantityToDdl(ref DropDownList ddl, int pProductSetID, int pSizeID, string valueSelected, bool required)
        {
            var dc = new SmartShopEntities();
            Product product = dc.Products.FirstOrDefault(e => e.ProductSetID == pProductSetID && e.SizeID == pSizeID);
            ddl.Items.Clear();
            if (product != null)
            {
                DataTable dt = new DataTable();
                dt = new StockDAL().GetCurentProductInStock(product.ProductID);
                if (dt.Rows.Count > 0)
                {
                    int quantity = Convert.ToInt32(dt.Rows[0][5]);
                    if (quantity > 0)
                    {
                        int max = 10;
                        if (quantity <= 10)
                            max = quantity;
                        for (int i = 1; i <= max; i++)
                        {
                            ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                        }
                    }
                }
            }
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Hết hàng -", "0"));
            }
            else
            {
                if (required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn số lượng sản phẩm -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }

        public static bool updateViewed(int id)
        {
            try
            {
                var dc = new SmartShopEntities();
                var obj = dc.Products.SingleOrDefault(c => c.ProductID == id);
                if (obj != null)
                {
                    obj.Viewed += 1;
                    dc.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DataSet GetSanPhamTheoDieuKien(string IdNhom, string orderField, string orderType, int pageSize, int currentPage, ref int totalRecord, int id = 0)
        {
            try
            {
                var dc = new SmartShopEntities();
                CommonDAL common = new CommonDAL();
                //SqlParameter output = new SqlParameter("@TotalRecord", SqlDbType.Int);
                string sql = "EXEC sp_Select_SanPham '" + id.ToString() + "', '" + IdNhom + "', '" + orderField + "', '" + orderType + "', " + pageSize + ", " + currentPage + ", 0";
                DataSet ds = common.ExecuteQuery(sql);
                if (IdNhom != null && IdNhom != "")
                {
                    int ProductGroupID = Convert.ToInt32(IdNhom);
                    List<int> lstID = dc.ProductGroups.Where(e => e.GroupParrentID == ProductGroupID).Select(e => e.ProductGroupID).ToList();
                    totalRecord = dc.Products.Where(e => e.ProductGroupID == ProductGroupID || lstID.Contains(e.ProductGroupID.Value)).Count();
                }
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataSet GetTimKiemSanPham(string IdNhom, string KeyWord, string orderField, string orderType, int pageSize, int currentPage, ref int totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                CommonDAL common = new CommonDAL();
                //SqlParameter output = new SqlParameter("@TotalRecord", SqlDbType.Int);
                string sql = "EXEC sp_TimKiem_SanPham N'" + KeyWord + "', '" + IdNhom + "', '" + orderField + "', '" + orderType + "', " + pageSize + ", " + currentPage + ", 0";
                DataSet ds = common.ExecuteQuery(sql);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
