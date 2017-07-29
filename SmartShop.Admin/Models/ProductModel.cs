using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.Admin
{
    public class ProductModel
    {
        public int ProductID { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string NoSymbolName { get; set; }

        public int ProductSetID { get; set; }

        public int ProductGroupID { get; set; }

        public double Quantity { get; set; }

        public int SizeID { get; set; }

        public int Viewed { get; set; }

        public int Available { get; set; }

        public string YTE_THUONG_HIEU { get; set; }

        public string YTE_XUAT_XU { get; set; }

        public string YTE_BAO_HANH { get; set; }

        public string YTE_GIA_BAN { get; set; }

        public string YTE_SO_TA { get; set; }

        public string YTE_TINH_NANG { get; set; }

        public string YTE_ANH_CHINH { get; set; }

        public string YTE_ANH_PHU_1 { get; set; }

        public string YTE_ANH_PHU_2 { get; set; }

        public string YTE_ANH_PHU_3 { get; set; }

        public string YTE_ANH_PHU_4 { get; set; }

        public string YTE_THONG_SO { get; set; }

        public string YTE_TAG { get; set; }

        public string YTE_DANH_GIA { get; set; }

        public string YTE_CHIET_KHAU { get; set; }
    }
}