using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.UI
{
    public static class ProductCatSvr
    {
        public static List<Category> GetAllCategory()
        {
            return Product_CategoryDAL.SelectAll();
        }

        public static Category GetById(int id)
        {
            return Product_CategoryDAL.GetById(id);
        }

        public static Category Insert(Category obj)
        {
            return Product_CategoryDAL.Insert(obj);
        }

        public static Category Update(Category obj)
        {
            return Product_CategoryDAL.Update(obj);
        }

        public static bool Delete(int id)
        {
            return Product_CategoryDAL.Delete(id);
        }

        public static List<spCategory_Paging_Result> getCategory_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            return Product_CategoryDAL.getCategory_Paging(pageSize, currentPage, ref totalRecord);
        }

        public static Category getCategoryByID(int pCategoryID)
        {
            return Product_CategoryDAL.getCategoryByID(pCategoryID);
        }

        public static List<spProductSize_Paging_Result> getProductSize_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            return Product_CategoryDAL.getProductSize_Paging(pageSize, currentPage, ref totalRecord);
        }

        public static List<spProductType_Paging_Result> getProductType_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            return Product_CategoryDAL.getProductType_Paging(pageSize, currentPage, ref totalRecord);
        }

        public static List<spProductGroup_Paging_Result> getProductGroup_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            return Product_CategoryDAL.getProductGroup_Paging(pageSize, currentPage, ref totalRecord);
        }

        public static List<spProductStyle_Paging_Result> getProductStyle_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            return Product_CategoryDAL.getProductStyle_Paging(pageSize, currentPage, ref totalRecord);
        }

        public static List<spProductDesign_Paging_Result> getProductDesign_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            return Product_CategoryDAL.getProductDesign_Paging(pageSize, currentPage, ref totalRecord);
        }

        public static List<spColor_Paging_Result> getColor_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            return Product_CategoryDAL.getColor_Paging(pageSize, currentPage, ref totalRecord);
        }

        public static List<spProvider_Paging_Result> getProvider_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            return Product_CategoryDAL.getProvider_Paging(pageSize, currentPage, ref totalRecord);
        }

        public static List<spProductMaterial_Paging_Result> getProductMaterial_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            return Product_CategoryDAL.getProductMaterial_Paging(pageSize, currentPage, ref totalRecord);
        }
    }
}