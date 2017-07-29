using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SmartShop.Utilities;

namespace SmartShop.DAL
{
    public class AlbumDAL
    {
        public AlbumDAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public SmartShopEntities db = new SmartShopEntities();
        public int InsertAlbum(int pProductID, string pTitle, string pMasterImg, string pResizeImg, string pMember, DateTime pPublishDate, string pTag, bool pActive)
        {
            try
            {
                Album art = new Album()
                {
                    ProductID = pProductID,
                    Title = pTitle,
                    MasterImage = pMasterImg,
                    ResizeImage = pResizeImg,
                    Member = pMember,
                    PublishDate = pPublishDate,
                    Tag = pTag,
                    Active = pActive
                };
                db.Albums.Add(art);
                db.SaveChanges();
                string direct = HttpContext.Current.Request.PhysicalApplicationPath + @"\images\album\" + art.AlbumID;
                System.IO.Directory.CreateDirectory(direct);
                return art.AlbumID;
            }
            catch (Exception e)
            { return 0; }
        }
        public bool UpdateAlbum(int pAlbumID, int pProductID, string pTitle, string pMasterImg, string pResizeImg, string pMember, DateTime pPublishDate, string pTag, bool pActive)
        {
            try
            {
                Album art = db.Albums.FirstOrDefault(e => e.AlbumID == pAlbumID);
                art.ProductID = pProductID;
                art.Title = pTitle;
                if (!string.IsNullOrEmpty(pMasterImg.Trim()))
                    art.MasterImage = pMasterImg;
                if (!string.IsNullOrEmpty(pResizeImg.Trim()))
                    art.ResizeImage = pResizeImg;
                art.Member = pMember;
                art.PublishDate = pPublishDate;
                art.Tag = pTag;
                art.Active = pActive;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            { return false; }
        }
        public bool DeleteAlbum(int pAlbumID)
        {
            try
            {
                IQueryable<Image> img = db.Images.Where(e => e.AlbumID == pAlbumID);
                foreach (Image i in img)
                {
                    db.Images.Remove(i);
                    db.SaveChanges();
                }
                Album art = db.Albums.FirstOrDefault(e => e.AlbumID == pAlbumID);
                db.Albums.Remove(art);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            { return false; }
        }
        public bool LockAlbum(int pAlbumID)
        {
            try
            {
                Album art = db.Albums.FirstOrDefault(e => e.AlbumID == pAlbumID);
                art.Active = false;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            { return false; }
        }
        public int UpdateAlbumViewed(int pAlbumID)
        {
            try
            {
                Album art = db.Albums.FirstOrDefault(e => e.AlbumID == pAlbumID);
                art.Viewed = art.Viewed + 1;
                db.SaveChanges();
                return art.Viewed;
            }
            catch (Exception e)
            { return 0; }
        }
        public Album GetAlbumByID(int pAlbumID)
        {
            try
            {
                return db.Albums.FirstOrDefault(e => e.AlbumID == pAlbumID);
            }
            catch (Exception e)
            { return null; }
        }
        public IQueryable<Album> GetAlbum(string pTitle, string pPublishFrom, string pPublishTo)
        {
            try
            {
                IQueryable<Album> art = from a in db.Albums
                                        select a;
                if (!string.IsNullOrEmpty(pTitle))
                {
                    art = from a in art
                          where a.Title.Contains(pTitle)
                          select a;
                }
                if (!string.IsNullOrEmpty(pPublishFrom))
                {
                    art = from a in art
                          where a.PublishDate > DateTime.Parse(pPublishFrom)
                          select a;
                }
                if (!string.IsNullOrEmpty(pPublishTo))
                {
                    art = from a in art
                          where a.PublishDate < DateTime.Parse(pPublishTo)
                          select a;
                }
                return art;
            }
            catch (Exception e)
            { return null; }
        }
        public Album GetAlbumByProduct(int pProductID)
        {
            try
            {
                return db.Albums.FirstOrDefault(e => e.ProductID == pProductID);
            }
            catch (Exception e)
            { return null; }
        }
        public DataTable GetAlbum()
        {
            try
            {
                IQueryable<Album> art = from a in db.Albums
                                        select a;
                return LTable.ConvertToDataTable(art.ToList());
            }
            catch (Exception e)
            { return null; }
        }
        public IQueryable<Album> GetNewAlbum(int pNumberRecord)
        {
            try
            {
                IQueryable<Album> art = from a in db.Albums
                                        orderby a.PublishDate descending
                                        select a;
                return art.Skip(0).Take(pNumberRecord);
            }
            catch (Exception e)
            { return null; }
        }
        public IQueryable<Album> GetHotAlbum(int pNumberRecord)
        {
            try
            {
                IQueryable<Album> art = from a in db.Albums
                                        orderby a.Viewed descending
                                        select a;
                return art.Skip(0).Take(pNumberRecord);
            }
            catch (Exception e)
            { return null; }
        }
        public bool CheckExistTitle(string pTitle)
        {
            try
            {
                IQueryable<Album> art = from a in db.Albums
                                        where a.Title.Equals(pTitle)
                                        select a;
                if (art.Count() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return true;
            }
        }
        public bool CheckExistProductID(int pProductID)
        {
            try
            {
                IQueryable<Album> art = from a in db.Albums
                                        where a.ProductID == pProductID
                                        select a;
                if (art.Count() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return true;
            }
        }


    }
}