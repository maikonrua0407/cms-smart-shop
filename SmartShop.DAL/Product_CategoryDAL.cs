using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using SmartShop.Utilities;

namespace SmartShop.DAL
{
    public class Product_CategoryDAL
    {
        public static List<Category> SelectAll()
        {
            SmartShopEntities entities = new SmartShopEntities();
            return entities.Categories.ToList();
        }

        public static Category GetById(int id)
        {
            SmartShopEntities entities = new SmartShopEntities();
            return entities.Categories.First(e => e.CategoryID == id);
        }

        public static Category Insert(Category obj)
        {

            Category kq = null;
            SmartShopEntities entities = new SmartShopEntities();
            try
            {
                entities.Categories.Add(obj);
                entities.SaveChanges();
                kq = obj;
            }
            catch (System.Exception ex)
            {
                kq = null;
            }
            finally
            {
                entities = null;
            }
            return kq;
        }

        public static Category Update(Category obj)
        {
            Category kq = null;
            SmartShopEntities entities = new SmartShopEntities();
            try
            {
                var original = entities.Categories.Find(obj.CategoryID);
                if (original != null)
                {
                    foreach (PropertyInfo propertyInfo in original.GetType().GetProperties())
                    {
                        if (propertyInfo.GetValue(obj, null) == null)
                            propertyInfo.SetValue(obj, propertyInfo.GetValue(original, null), null);
                    }

                    entities.Entry(original).CurrentValues.SetValues(obj);
                    entities.SaveChanges();
                    kq = obj;
                }
            }
            catch (System.Exception ex)
            {
                kq = null;
            }
            finally
            {
                entities = null;
            }
            return kq;
        }

        public static bool Delete(int id)
        {
            bool kq = true;
            SmartShopEntities entities = new SmartShopEntities();
            Category obj = GetById(id);
            try
            {
                entities.Categories.Attach(obj);
                entities.Categories.Remove(obj);
                entities.SaveChanges();
            }
            catch (System.Exception ex)
            {
                kq = false;
            }
            finally
            {
                entities = null;
            }
            return kq;
        }

        public static List<spCategory_Paging_Result> getCategory_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var query = dc.spCategory_Paging(pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord", totalRecord));
                var lst = query.ToList();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Category getCategoryByID(int pCategoryID)
        {
            try
            {
                var dc = new SmartShopEntities();
                return dc.Categories.FirstOrDefault(e => e.CategoryID == pCategoryID);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<spProductSize_Paging_Result> getProductSize_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var query = dc.spProductSize_Paging(pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord", totalRecord));
                var lst = query.ToList();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<spProductType_Paging_Result> getProductType_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var query = dc.spProductType_Paging(pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord", totalRecord));
                var lst = query.ToList();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<spProductGroup_Paging_Result> getProductGroup_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var query = dc.spProductGroup_Paging(pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord", totalRecord));
                var lst = query.ToList();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<spProductStyle_Paging_Result> getProductStyle_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var query = dc.spProductStyle_Paging(pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord", totalRecord));
                var lst = query.ToList();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<spProductDesign_Paging_Result> getProductDesign_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var query = dc.spProductDesign_Paging(pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord", totalRecord));
                var lst = query.ToList();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<spColor_Paging_Result> getColor_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var query = dc.spColor_Paging(pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord", totalRecord));
                var lst = query.ToList();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<spProvider_Paging_Result> getProvider_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var query = dc.spProvider_Paging(pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord", totalRecord));
                var lst = query.ToList();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<spProductMaterial_Paging_Result> getProductMaterial_Paging(int pageSize, int currentPage, ref int totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var query = dc.spProductMaterial_Paging(pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord", totalRecord));
                var lst = query.ToList();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}