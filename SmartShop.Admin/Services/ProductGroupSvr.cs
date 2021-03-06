﻿using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.Admin
{
    public static class ProductGroupSvr
    {
        public static ProductGroup GetById(int id)
        {
            return ProductGroupDAL.GetByID(id);
        }

        public static ProductGroup Insert(ProductGroup obj)
        {
            return ProductGroupDAL.Insert(obj);
        }

        public static ProductGroup Update(ProductGroup obj)
        {
            return ProductGroupDAL.Update(obj);
        }

        public static bool Delete(int id)
        {
            return ProductGroupDAL.Delete(id);
        }

        public static ProductGroup GetByNameNoSymbol(string strName)
        {
            return ProductGroupDAL.GetByNameNoSymbol(strName);
        }

        public static IQueryable<ProductGroup> GetAllItem()
        {
            return ProductGroupDAL.GetAllItem();
        }

        public static IQueryable<ProductGroup> GetByCategory(int pCategoryID)
        {
            return ProductGroupDAL.GetByCategory(pCategoryID);
        }

        public static IQueryable<ProductGroup> GetByParrent(int pParrentID)
        {
            return ProductGroupDAL.GetByParrent(pParrentID);
        }

        public static ProductGroup getParrent(int pGroupID)
        {
            return ProductGroupDAL.getParrent(pGroupID);
        }
    }
}