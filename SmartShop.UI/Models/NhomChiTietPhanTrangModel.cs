using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SmartShop.UI
{
    public class NhomChiTietPhanTrangModel
    {
        List<DataRow> listRow;

        public List<DataRow> ListRow
        {
            get { return listRow; }
            set { listRow = value; }
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
    }
}