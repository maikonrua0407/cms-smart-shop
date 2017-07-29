using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SmartShop.UI
{
    public static class ProductSvr
    {
        public static List<vwProduct> SelectAll()
        {
            return ProductDAL.SelectAll().ToList();
        }

        public static IQueryable<Product> GetAllItem(string strKeyWord)
        {
            return ProductDAL.GetAllItem(strKeyWord);
        }

        public static Product GetByCode(string strCode)
        {
            return ProductDAL.GetByCode(strCode);
        }

        public static Product GetByID(int id)
        {
            return ProductDAL.GetByID(id);
        }

        public static Product GetByNameNoSymbol(int idGroup, string strName)
        {
            return ProductDAL.GetByNameNoSymbol(idGroup, strName);
        }

        public static IQueryable<Product> GetAllItem()
        {
            return ProductDAL.GetAllItem();
        }

        public static bool Insert(Product objProduct)
        {
            return ProductDAL.Insert(ref objProduct);
        }

        public static bool Update(Product objProduct)
        {
            return ProductDAL.Update(ref objProduct);
        }

        public static bool Delete(int id)
        {
            return ProductDAL.Delete(id);
        }

        public static List<SP_SAN_PHAM_TTINH> getThuocTinhTheoSP(int IdSanPham)
        {
            return new ThuocTinhDAL().getThuocTinhTheoSP(IdSanPham);
        }

        public static List<SP_SAN_PHAM_TTINH> ThemThuocTinhSP(List<SP_SAN_PHAM_TTINH> lstThuocTinh)
        {
            return new ThuocTinhDAL().ThemThuocTinhSP(lstThuocTinh);
        }

        public static List<string> getThuongHieuSP(int CatID = 0)
        {
            return new ThuocTinhDAL().getThuongHieuSP(CatID);
        }

        public static IQueryable<vwProductSet_Product> GetvwProductSet_Product_Paging(string keyword, int pageSize, int currentPage, ref int totalRecord)
        {
            return ProductDAL.GetvwProductSet_Product_Paging(keyword, pageSize, currentPage, ref totalRecord);
        }

        public static IQueryable<vwProductSet_Product> GetvwProductSet_Product(List<int> ListID)
        {
            return ProductDAL.GetvwProductSet_Product(ListID);
        }

        public static IQueryable<Product> GetExist_Ma(string Ma_Exist)
        {
            return ProductDAL.GetExist_Ma(Ma_Exist);
        }

        public static bool checkExits_Ma_Update(string Ma_Exist, string Ma_From)
        {
            return ProductDAL.checkExits_Ma_Update(Ma_Exist, Ma_From);
        }

        public static bool CheckExist_Ma_Insert(string Main)
        {
            return ProductDAL.CheckExist_Ma_Insert(Main);
        }

        public static List<vwProduct> GetP_vwProduct_SearchPaging(string keyword, int? ProductGroupID, int? ProductSetID, int SizeID, int pageSize, int currentPage, ref int? totalRecord)
        {
            return ProductDAL.GetP_vwProduct_SearchPaging(keyword, ProductGroupID, ProductSetID, SizeID, pageSize, currentPage, ref totalRecord);
        }

        public static List<vwProductSetAllName> SelectProductsByFindShort(string pKeyword, int pProductGroupID, int pStyleID, int pTyleID, int pDesignID, int pMaterialID, int ProviderID, double pPriceFrom, double pPriceTo, int pShort, int pPageSize, int pCurrentPage, ref int? pTotalRecord)
        {
            return ProductDAL.SelectProductsByFindShort(pKeyword, pProductGroupID, pStyleID, pTyleID, pDesignID, pMaterialID, ProviderID, pPriceFrom, pPriceTo, pShort, pPageSize, pCurrentPage, ref pTotalRecord);
        }

        public static List<vwProductSetAllName> SelectProductsByFindShort(string pKeyword, int pProductGroupID, int ProviderID, int pShort)
        {
            return ProductDAL.SelectProductsByFindShort(pKeyword, pProductGroupID, ProviderID, pShort);
        }

        public static List<vwProductSetAllName> SelectTopNew()
        {
            return ProductDAL.SelectTopNew();
        }

        public static List<vwProductSetAllName> SelectTopSame(int pProductSetID)
        {
            return ProductDAL.SelectTopSame(pProductSetID);
        }

        public static List<vwProductSetAllName> SelectTopTutorial()
        {
            return ProductDAL.SelectTopTutorial();
        }

        public static ProductSet GetProductSetByProductID(int id)
        {
            return ProductDAL.GetProductSetByProductID(id);
        }

        public static bool CheckProductCodeExist(string strCode)
        {
            return ProductDAL.CheckProductCodeExist(strCode);
        }

        public static IQueryable<Product> GetProduct_Paging(string keyword, int pageSize, int currentPage, ref int totalRecord)
        {
            return ProductDAL.GetProduct_Paging(keyword, pageSize, currentPage, ref totalRecord);
        }

        public static void bindProductQuantityToDdl(ref DropDownList ddl, int pProductSetID, int pSizeID, string valueSelected, bool required)
        {
            ProductDAL.bindProductQuantityToDdl(ref ddl, pProductSetID, pSizeID, valueSelected, required);
        }

        public static bool updateViewed(int id)
        {
            return ProductDAL.updateViewed(id);
        }

        public static DataSet GetSanPhamTheoDieuKien(string IdNhom, string orderField, string orderType, int pageSize, int currentPage, ref int totalRecord, int id = 0)
        {
            return ProductDAL.GetSanPhamTheoDieuKien(IdNhom, orderField, orderType, pageSize, currentPage, ref totalRecord, id);
        }

        public static DataSet GetTimKiemSanPham(string IdNhom, string KeyWord, string orderField, string orderType, int pageSize, int currentPage, ref int totalRecord)
        {
            return ProductDAL.GetTimKiemSanPham(IdNhom, KeyWord, orderField, orderType, pageSize, currentPage, ref totalRecord);
        }
    }
}