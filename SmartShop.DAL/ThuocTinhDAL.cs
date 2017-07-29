using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace SmartShop.DAL
{
    public class ThuocTinhDAL
    {
        public ThuocTinhDAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Lấy đối tượng SP_TTINH_GTRI
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>đối tượng SP_TTINH_GTRI</returns>
        public SP_TTINH_GTRI getDanhMucGTriByID(int ID)
        {
            var db = new SmartShopEntities();
            SP_TTINH_GTRI dmGiaTri = null;
            try
            {
                dmGiaTri = db.SP_TTINH_GTRI.FirstOrDefault(e => e.ID == ID); ;
                if (dmGiaTri != null)
                {
                    return dmGiaTri;
                }
            }
            catch (System.Exception ex)
            {
                dmGiaTri = null;
            }
            finally
            {
                db = null;
            }
            return dmGiaTri;
        }

        /// <summary>
        /// Lấy List các đối tượng SP_TTINH_GTRI theo Loại
        /// </summary>
        /// <param name="IDLoai">ID loại danh mục</param>
        /// <returns>List các đối tượng SP_TTINH_GTRI</returns>
        public List<SP_TTINH_GTRI> getDanhMucGTriByIDLoai(int IDLoai)
        {
            var db = new SmartShopEntities();
            List<SP_TTINH_GTRI> dmGiaTri = null;
            try
            {
                dmGiaTri = db.SP_TTINH_GTRI.Where(e => e.ID_TTINH_LOAI == IDLoai).ToList();
                if (dmGiaTri != null)
                {
                    return dmGiaTri;
                }
            }
            catch (System.Exception ex)
            {
                dmGiaTri = null;

            }
            finally
            {
                db = null;
            }
            return dmGiaTri;
        }

        /// <summary>
        /// Lấy SP_TTINH_GTRI theo Mã loại, Mã danh mục
        /// </summary>
        /// <param name="maLoai">Mã loại</param>
        /// <param name="maDMuc">Mã danh mục</param>
        /// <returns></returns>
        public SP_TTINH_GTRI getDanhMucGTriByMaLoaiMaDMuc(string maLoai, string maDMuc)
        {
            var db = new SmartShopEntities();
            SP_TTINH_GTRI dmGiaTri = null;
            try
            {
                dmGiaTri = db.SP_TTINH_GTRI.FirstOrDefault(e => e.MA_TTINH_LOAI == maLoai && e.MA_TTINH == maDMuc);
                if (dmGiaTri != null)
                {
                    return dmGiaTri;
                }
            }
            catch (System.Exception ex)
            {
                dmGiaTri = null;

            }
            finally
            {
                db = null;
            }
            return dmGiaTri;
        }

        /// <summary>
        /// Thêm mới 1 đối tượng SP_TTINH_GTRI
        /// </summary>
        /// <param name="obj">object SP_TTINH_GTRI</param>
        /// <returns>ID object SP_TTINH_GTRI mới được thêm</returns>
        public SP_TTINH_GTRI ThemGTri(SP_TTINH_GTRI obj)
        {
            SP_TTINH_GTRI kq = null;
            var db = new SmartShopEntities();
            try
            {

                db.SP_TTINH_GTRI.Add(obj);
                db.SaveChanges();
                kq = obj;
            }
            catch (System.Exception ex)
            {
                kq = null;

            }
            finally
            {
                db = null;
            }
            return kq;
        }

        /// <summary>
        /// Sửa thông tin 1 đối tượng SP_TTINH_GTRI
        /// </summary>
        /// <param name="obj">object SP_TTINH_GTRI</param>
        /// <returns>ID object SP_TTINH_GTRI mới được thêm</returns>
        public SP_TTINH_GTRI SuaGTri(SP_TTINH_GTRI obj)
        {
            SP_TTINH_GTRI kq = null;
            var db = new SmartShopEntities();

            try
            {
                SP_TTINH_GTRI objToUpdate = db.SP_TTINH_GTRI.Where(x => x.ID == obj.ID).FirstOrDefault();
                objToUpdate.ID_TTINH_LOAI = obj.ID_TTINH_LOAI;
                objToUpdate.MA_TTINH = obj.MA_TTINH;
                objToUpdate.MA_TTINH_LOAI = obj.MA_TTINH_LOAI;
                objToUpdate.TEN_TTINH = obj.TEN_TTINH;
                db.SaveChanges();

                kq = obj;
                return kq;
            }
            catch (System.Exception ex)
            {
                kq = null;

                throw ex;
            }
            finally
            {
                db = null;
            }
        }

        /// <summary>
        /// Xóa 1 đối tượng SP_TTINH_GTRI
        /// </summary>
        /// <param name="obj">object SP_TTINH_GTRI</param>
        /// <returns>True nếu xóa thành công, ngược lại trả về False</returns>
        public bool XoaGTri(int Id)
        {
            bool kq = true;
            try
            {
                SP_TTINH_GTRI obj = getDanhMucGTriByID(Id);
                var db = new SmartShopEntities();
                db.SP_TTINH_GTRI.Attach(obj);
                db.SP_TTINH_GTRI.Remove(obj);
                db.SaveChanges();
            }
            catch (System.Exception ex)
            {
                kq = false;

            }
            return kq;
        }

        /// <summary>
        /// Xóa 1 list đối tượng SP_TTINH_GTRI
        /// </summary>
        /// <param name="listID">List ID các đối tượng SP_TTINH_GTRI</param>
        /// <returns>True nếu xóa thành công, ngược lại trả về False</returns>
        public bool XoaListGTriTheoID(List<int> listID)
        {
            bool kq = true;
            var db = new SmartShopEntities();
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    foreach (int id in listID)
                    {
                        SP_TTINH_GTRI obj = db.SP_TTINH_GTRI.FirstOrDefault(e => e.ID == id);
                        db.SP_TTINH_GTRI.Attach(obj);
                        db.SP_TTINH_GTRI.Remove(obj);
                        db.SaveChanges();
                    }
                    trans.Complete();
                }
            }
            catch (System.Exception ex)
            {
                kq = false;

            }
            finally
            {
                db = null;
            }
            return kq;
        }


        /// <summary>
        /// Lấy đối tượng SP_TTINH_LOAI
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>đối tượng SP_TTINH_LOAI</returns>
        public SP_TTINH_LOAI getDanhMucLoaiByID(int ID)
        {
            var db = new SmartShopEntities();
            SP_TTINH_LOAI dmGiaTri = null;
            try
            {
                dmGiaTri = db.SP_TTINH_LOAI.FirstOrDefault(e => e.ID == ID);
                if (dmGiaTri != null)
                {
                    return dmGiaTri;
                }
            }
            catch (System.Exception ex)
            {
                dmGiaTri = null;

            }
            finally
            {
                db = null;
            }
            return dmGiaTri;
        }

        /// <summary>
        /// Lấy đối tượng SP_TTINH_LOAI
        /// </summary>
        /// <param name="Ma">Ma</param>
        /// <returns>đối tượng SP_TTINH_LOAI</returns>
        public SP_TTINH_LOAI getDanhMucLoaiByMa(string Ma)
        {
            var db = new SmartShopEntities();
            SP_TTINH_LOAI dmGiaTri = null;
            try
            {
                dmGiaTri = db.SP_TTINH_LOAI.FirstOrDefault(e => e.MA_TTINH_LOAI == Ma); ;
                if (dmGiaTri != null)
                {
                    return dmGiaTri;
                }
            }
            catch (System.Exception ex)
            {
                dmGiaTri = null;

            }
            finally
            {
                db = null;
            }
            return dmGiaTri;
        }

        /// <summary>
        /// Thêm mới 1 đối tượng SP_TTINH_LOAI
        /// </summary>
        /// <param name="obj">object SP_TTINH_LOAI</param>
        /// <returns>ID object SP_TTINH_LOAI mới được thêm</returns>
        public SP_TTINH_LOAI ThemDanhMucLoai(SP_TTINH_LOAI obj)
        {
            SP_TTINH_LOAI kq = null;
            var db = new SmartShopEntities();
            try
            {

                db.SP_TTINH_LOAI.Add(obj);
                db.SaveChanges();
                kq = obj;
            }
            catch (System.Exception ex)
            {
                kq = null;

            }
            finally
            {
                db = null;
            }
            return kq;
        }

        /// <summary>
        /// Sửa thông tin 1 đối tượng SP_TTINH_LOAI
        /// </summary>
        /// <param name="obj">object SP_TTINH_LOAI</param>
        /// <returns>ID object SP_TTINH_LOAI mới được thêm</returns>
        public SP_TTINH_LOAI SuaDanhMucLoai(SP_TTINH_LOAI obj)
        {
            SP_TTINH_LOAI kq = null;

            object original = null;
            var db = new SmartShopEntities();

            try
            {
                SP_TTINH_LOAI objToUpdate = db.SP_TTINH_LOAI.Where(x => x.ID == obj.ID).FirstOrDefault();
                objToUpdate = obj;
                db.SaveChanges();

                kq = obj;
                return kq;
            }
            catch (System.Exception ex)
            {
                kq = null;

                throw ex;
            }
            finally
            {
                db = null;
            }
        }

        /// <summary>
        /// Xóa 1 đối tượng SP_TTINH_LOAI
        /// </summary>
        /// <param name="obj">object SP_TTINH_LOAI</param>
        /// <returns>True nếu xóa thành công, ngược lại trả về False</returns>
        public bool XoaDanhMucLoai(int id)
        {
            bool kq = true;
            var db = new SmartShopEntities();
            try
            {
                SP_TTINH_LOAI obj = getDanhMucLoaiByID(id);
                db.SP_TTINH_LOAI.Attach(obj);
                db.SP_TTINH_LOAI.Remove(obj);
                db.SaveChanges();
                return kq;
            }
            catch (System.Exception ex)
            {
                kq = false;

                throw ex;
            }
            finally
            {
                db = null;
            }
        }

        /// <summary>
        /// Xóa 1 list đối tượng SP_TTINH_LOAI
        /// </summary>
        /// <param name="listID">List ID các đối tượng SP_TTINH_LOAI</param>
        /// <returns>True nếu xóa thành công, ngược lại trả về False</returns>
        public bool XoaListDanhMucLoaiTheoID(List<int> listID)
        {
            bool kq = true;
            var db = new SmartShopEntities();
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    foreach (int id in listID)
                    {
                        SP_TTINH_LOAI obj = db.SP_TTINH_LOAI.FirstOrDefault(e => e.ID == id);
                        db.SP_TTINH_LOAI.Attach(obj);
                        db.SP_TTINH_LOAI.Remove(obj);
                        db.SaveChanges();
                    }
                    trans.Complete();
                }
            }
            catch (System.Exception ex)
            {
                kq = false;

            }
            finally
            {
                db = null;
            }
            return kq;
        }

        public List<SP_SAN_PHAM_TTINH> getThuocTinhTheoSP(int ID_SanPham)
        {
            var db = new SmartShopEntities();
            List<SP_SAN_PHAM_TTINH> thuocTinh = null;
            try
            {
                thuocTinh = db.SP_SAN_PHAM_TTINH.Where(e => e.ID_SAN_PHAM == ID_SanPham).ToList();
                if (thuocTinh != null)
                {
                    return thuocTinh;
                }
            }
            catch (System.Exception ex)
            {
                thuocTinh = null;

            }
            finally
            {
                db = null;
            }
            return thuocTinh;
        }

        public List<SP_SAN_PHAM_TTINH> ThemThuocTinhSP(List<SP_SAN_PHAM_TTINH> lstThuocTinh)
        {
            var db = new SmartShopEntities();
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    List<SP_SAN_PHAM_TTINH> lstDelete = db.SP_SAN_PHAM_TTINH.Where(e => e.ID_SAN_PHAM == lstThuocTinh.FirstOrDefault().ID_SAN_PHAM).ToList();
                    foreach (SP_SAN_PHAM_TTINH obj in lstDelete)
                    {
                        db.SP_SAN_PHAM_TTINH.Remove(obj);
                        db.SaveChanges();
                    }
                    foreach (SP_SAN_PHAM_TTINH obj in lstThuocTinh)
                    {
                        db.SP_SAN_PHAM_TTINH.Add(obj);
                        db.SaveChanges();
                    }
                    trans.Complete();
                }
                return lstThuocTinh;
            }
            catch (System.Exception ex)
            {
                lstThuocTinh = null;

            }
            finally
            {
                db = null;
            }
            return lstThuocTinh;
        }

        public List<string> getThuongHieuSP(int CatID = 0)
        {
            List<string> lstThuongHieu = new List<string>();
            var db = new SmartShopEntities();
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {

                    lstThuongHieu = db.sp_TTINH_SPHAM_THUONGHIEU(CatID).ToList();
                    trans.Complete();
                }
                return lstThuongHieu;
            }
            catch (System.Exception ex)
            {
                lstThuongHieu = null;

            }
            finally
            {
                db = null;
            }
            return lstThuongHieu;
        }
    }
}