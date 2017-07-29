using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.DAL
{
    public class ImageDAL
    {
        public ImageDAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public SmartShopEntities db = new SmartShopEntities();
        public int InsertImage(int pAlbumID, string pTitle, string pMasterImg, string pResizeImg, string pMember, DateTime pPublishDate, string pTag)
        {
            try
            {
                Image art = new Image()
                {
                    AlbumID = pAlbumID,
                    Title = pTitle,
                    MasterImage = pMasterImg,
                    ResizeImage = pResizeImg,
                    Member = pMember,
                    PublishDate = pPublishDate,
                    Tag = pTag,
                    Active = true,
                    Selected = false
                };
                db.Images.Add(art);
                db.SaveChanges();
                if (db.Images.Where(e => e.AlbumID == art.AlbumID && e.Selected == true).Count() == 0)
                {
                    Image img = db.Images.FirstOrDefault(e => e.AlbumID == art.AlbumID);
                    img.Selected = true;
                    db.Albums.FirstOrDefault(e => e.AlbumID == art.AlbumID).MasterImage = img.MasterImage;
                    db.SaveChanges();
                }
                return art.ImageID;
            }
            catch (Exception e)
            { return 0; }
        }
        public bool UpdateImage(int pImageID, int pAlbumID, string pTitle, string pMasterImg, string pResizeImg, string pMember, DateTime pPublishDate, string pTag)
        {
            try
            {
                Image art = db.Images.FirstOrDefault(e => e.ImageID == pImageID);

                art.AlbumID = pAlbumID;
                art.Title = pTitle;
                art.MasterImage = pMasterImg;
                art.ResizeImage = pResizeImg;
                art.Member = pMember;
                art.PublishDate = pPublishDate;
                art.Tag = pTag;
                art.Active = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            { return false; }
        }
        public bool DeleteImage(int pImageID)
        {
            try
            {
                Image art = db.Images.FirstOrDefault(e => e.ImageID == pImageID);
                db.Images.Remove(art);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            { return false; }
        }

        public bool DefaultImage(int pImageID)
        {
            try
            {
                Image art = db.Images.FirstOrDefault(e => e.ImageID == pImageID);
                foreach (Image img in db.Images.Where(e => e.AlbumID == art.AlbumID))
                {
                    if (img.ImageID == pImageID)
                        img.Selected = true;
                    else
                        img.Selected = false;
                }
                db.Albums.Where(e => e.AlbumID == art.AlbumID).FirstOrDefault().MasterImage = art.MasterImage;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            { return false; }
        }
        public bool LockImage(int pImageID)
        {
            try
            {
                Image art = db.Images.FirstOrDefault(e => e.ImageID == pImageID);
                art.Active = false;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            { return false; }
        }
        public int UpdateImageViewed(int pImageID)
        {
            try
            {
                Image art = db.Images.FirstOrDefault(e => e.ImageID == pImageID);
                art.Viewed = art.Viewed + 1;
                db.SaveChanges();
                return art.Viewed;
            }
            catch (Exception e)
            { return 0; }
        }
        public Image GetImageByID(int pImageID)
        {
            try
            {
                return db.Images.FirstOrDefault(e => e.ImageID == pImageID);
            }
            catch (Exception e)
            { return null; }
        }
        public IQueryable<Image> GetImage(int pAlbumID, string pTitle, string pPublishFrom, string pPublishTo)
        {
            try
            {
                IQueryable<Image> art = from a in db.Images
                                        select a;
                if (pAlbumID > 0)
                {
                    art = from a in art
                          where a.AlbumID == pAlbumID
                          select a;
                }
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
        public IQueryable<Image> GetNewImage(int pNumberRecord)
        {
            try
            {
                IQueryable<Image> art = from a in db.Images
                                        orderby a.PublishDate descending
                                        select a;
                return art.Skip(0).Take(pNumberRecord);
            }
            catch (Exception e)
            { return null; }
        }
        public IQueryable<Image> GetHotImage(int pNumberRecord)
        {
            try
            {
                IQueryable<Image> art = from a in db.Images
                                        orderby a.Viewed descending
                                        select a;
                return art.Skip(0).Take(pNumberRecord);
            }
            catch (Exception e)
            { return null; }
        }
        public bool CheckExistTitle(int pAlbumID, string pTitle)
        {
            try
            {
                IQueryable<Image> art = from a in db.Images
                                        where a.AlbumID == pAlbumID && a.Title.Equals(pTitle)
                                        select a;
                if (art.Count() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            { return true; }
        }
    }
}