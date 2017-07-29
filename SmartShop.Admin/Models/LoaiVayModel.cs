using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.Admin
{
    public class LoaiVaySearchPagingModel
    {
        string maLoaiVay;

        public string MaLoaiVay
        {
            get { return maLoaiVay; }
            set { maLoaiVay = value; }
        }
        string moTa;

        public string MoTa
        {
            get { return moTa; }
            set { moTa = value; }
        }
        string trangThai;

        public string TrangThai
        {
            get { return trangThai; }
            set { trangThai = value; }
        }
        int currentPage;

        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }
        int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        int totalRecord;

        public int TotalRecord
        {
            get { return totalRecord; }
            set { totalRecord = value; }
        }
    }
}