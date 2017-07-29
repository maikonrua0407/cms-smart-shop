using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.Admin.Services
{
    public static class ProductPropertySvr
    {
        /// <summary>
        /// Lấy đối tượng SP_TTINH_GTRI
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>đối tượng SP_TTINH_GTRI</returns>
        public static SP_TTINH_GTRI getDanhMucGTriByID(int ID)
        {
            return new ThuocTinhDAL().getDanhMucGTriByID(ID);
        }

        /// <summary>
        /// Lấy List các đối tượng SP_TTINH_GTRI theo Loại
        /// </summary>
        /// <param name="IDLoai">ID loại danh mục</param>
        /// <returns>List các đối tượng SP_TTINH_GTRI</returns>
        public static List<SP_TTINH_GTRI> getDanhMucGTriByIDLoai(int IDLoai)
        {
            return new ThuocTinhDAL().getDanhMucGTriByIDLoai(IDLoai);
        }

        /// <summary>
        /// Lấy SP_TTINH_GTRI theo Mã loại, Mã danh mục
        /// </summary>
        /// <param name="maLoai">Mã loại</param>
        /// <param name="maDMuc">Mã danh mục</param>
        /// <returns></returns>
        public static SP_TTINH_GTRI getDanhMucGTriByMaLoaiMaDMuc(string maLoai, string maDMuc)
        {
            return new ThuocTinhDAL().getDanhMucGTriByMaLoaiMaDMuc(maLoai, maDMuc);
        }

        /// <summary>
        /// Thêm mới 1 đối tượng SP_TTINH_GTRI
        /// </summary>
        /// <param name="obj">object SP_TTINH_GTRI</param>
        /// <returns>ID object SP_TTINH_GTRI mới được thêm</returns>
        public static SP_TTINH_GTRI Insert(SP_TTINH_GTRI obj)
        {
            return new ThuocTinhDAL().ThemGTri(obj);
        }

        /// <summary>
        /// Sửa thông tin 1 đối tượng SP_TTINH_GTRI
        /// </summary>
        /// <param name="obj">object SP_TTINH_GTRI</param>
        /// <returns>ID object SP_TTINH_GTRI mới được thêm</returns>
        public static SP_TTINH_GTRI Update(SP_TTINH_GTRI obj)
        {
            return new ThuocTinhDAL().SuaGTri(obj);
        }

        /// <summary>
        /// Xóa 1 đối tượng SP_TTINH_GTRI
        /// </summary>
        /// <param name="obj">object SP_TTINH_GTRI</param>
        /// <returns>True nếu xóa thành công, ngược lại trả về False</returns>
        public static bool Delete(int id)
        {
            return new ThuocTinhDAL().XoaGTri(id);
        }

        /// <summary>
        /// Xóa 1 list đối tượng SP_TTINH_GTRI
        /// </summary>
        /// <param name="listID">List ID các đối tượng SP_TTINH_GTRI</param>
        /// <returns>True nếu xóa thành công, ngược lại trả về False</returns>
        public static bool XoaListGTriTheoID(List<int> listID)
        {
            return new ThuocTinhDAL().XoaListGTriTheoID(listID);
        }


        /// <summary>
        /// Lấy đối tượng SP_TTINH_LOAI
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>đối tượng SP_TTINH_LOAI</returns>
        public static SP_TTINH_LOAI getDanhMucLoaiByID(int ID)
        {
            return new ThuocTinhDAL().getDanhMucLoaiByID(ID);
        }

        /// <summary>
        /// Lấy đối tượng SP_TTINH_LOAI
        /// </summary>
        /// <param name="Ma">Ma</param>
        /// <returns>đối tượng SP_TTINH_LOAI</returns>
        public static SP_TTINH_LOAI getDanhMucLoaiByMa(string Ma)
        {
            return new ThuocTinhDAL().getDanhMucLoaiByMa(Ma);
        }

        /// <summary>
        /// Thêm mới 1 đối tượng SP_TTINH_LOAI
        /// </summary>
        /// <param name="obj">object SP_TTINH_LOAI</param>
        /// <returns>ID object SP_TTINH_LOAI mới được thêm</returns>
        public static SP_TTINH_LOAI InsertLoai(SP_TTINH_LOAI obj)
        {
            return new ThuocTinhDAL().ThemDanhMucLoai(obj);
        }

        /// <summary>
        /// Sửa thông tin 1 đối tượng SP_TTINH_LOAI
        /// </summary>
        /// <param name="obj">object SP_TTINH_LOAI</param>
        /// <returns>ID object SP_TTINH_LOAI mới được thêm</returns>
        public static SP_TTINH_LOAI UpdateLoai(SP_TTINH_LOAI obj)
        {
            return new ThuocTinhDAL().SuaDanhMucLoai(obj);
        }

        /// <summary>
        /// Xóa 1 đối tượng SP_TTINH_LOAI
        /// </summary>
        /// <param name="obj">object SP_TTINH_LOAI</param>
        /// <returns>True nếu xóa thành công, ngược lại trả về False</returns>
        public static bool DeleteLoai(int id)
        {
            return new ThuocTinhDAL().XoaDanhMucLoai(id);
        }

        /// <summary>
        /// Xóa 1 list đối tượng SP_TTINH_LOAI
        /// </summary>
        /// <param name="listID">List ID các đối tượng SP_TTINH_LOAI</param>
        /// <returns>True nếu xóa thành công, ngược lại trả về False</returns>
        public static bool DeleteListLoai(List<int> listID)
        {
            return new ThuocTinhDAL().XoaListDanhMucLoaiTheoID(listID);
        }
    }
}