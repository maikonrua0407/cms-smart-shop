using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace SmartShop.DAL
{
    public class Entity
    {
        private string _id;
        private string _name;

        public Entity() { }

        public Entity(string id, string name)
        {
            this._id = id;
            this._name = name;
        }

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
    public class ProductUnit
    {
        public static List<Entity> List
        {
            get;
            private set;
        }
        public static Entity PCS = new Entity("1", "Chiếc");
        public static Entity Set = new Entity("2", "bộ");
        static ProductUnit()
        {
            List = new List<Entity>();
            List.Add(PCS);
            List.Add(Set);
        }
        public static List<Entity> ListUnit()
        {
            List = new List<Entity>();
            List.Add(PCS);
            List.Add(Set);
            return List;
        }
        public static string Name(string key)
        {
            try
            {
                var entity = List.SingleOrDefault(item => item.id.Equals(key));
                return entity.name;
            }
            catch { return string.Empty; }
        }
        public static void bindUnitToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = ListUnit();
            ddl.DataTextField = "name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn đơn vị sản phẩm -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
    }
    public class ForGender
    {
        public static List<Entity> List
        {
            get;
            private set;
        }
        public static Entity Nam = new Entity("1", "Nam");
        public static Entity Nu = new Entity("2", "Nữ");
        static ForGender()
        {
            List = new List<Entity>();
            List.Add(Nam);
            List.Add(Nu);
        }
        public static List<Entity> ListForGender()
        {
            List = new List<Entity>();
            List.Add(Nam);
            List.Add(Nu);
            return List;
        }
        public static string Name(string key)
        {
            try
            {
                var entity = List.SingleOrDefault(item => item.id.Equals(key));
                return entity.name;
            }
            catch { return string.Empty; }
        }
        public static void bindForGenderToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = ListForGender();
            ddl.DataTextField = "name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn giới tính -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
    }
    public class Type
    {
        public static List<Entity> List
        {
            get;
            private set;
        }
        public static Entity BaiViet = new Entity("1", "Bài viết");
        public static Entity SanPham = new Entity("2", "Sản phẩm");
        public static Entity AdminChuaTraLoi = new Entity("3", "Admin chưa trả lời");
        static Type()
        {
            List = new List<Entity>();
            List.Add(BaiViet);
            List.Add(SanPham);
            List.Add(AdminChuaTraLoi);
        }
        public static List<Entity> ListType()
        {
            List = new List<Entity>();
            List.Add(BaiViet);
            List.Add(SanPham);
            List.Add(AdminChuaTraLoi);
            return List;
        }
        public static string Name(string key)
        {
            try
            {
                var entity = List.SingleOrDefault(item => item.id.Equals(key));
                return entity.name;
            }
            catch { return string.Empty; }
        }
        public static void bindForGenderToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = ListType();
            ddl.DataTextField = "name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn kiểu -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
    }
    public class NhomThongTin
    {
        public static List<Entity> List
        {
            get;
            private set;
        }
        public static Entity LOGO = new Entity("LOGO", "Logo");
        public static Entity KETNOI = new Entity("KETNOI", "Kết nối");
        public static Entity SLIDE = new Entity("SLIDE", "Slide");
        public static Entity FOOTER = new Entity("FOOTER", "Footer");
        public static Entity HOTLINE = new Entity("HOTLINE", "Hotline");
        public static Entity SUPPORT = new Entity("SUPPORT", "Trợ giúp trực tuyến");
        static NhomThongTin()
        {
            List = new List<Entity>();
            List.Add(LOGO);
            List.Add(KETNOI);
            List.Add(SLIDE);
            List.Add(FOOTER);
            List.Add(HOTLINE);
            List.Add(SUPPORT);
        }
        public static List<Entity> ListThongTin()
        {
            List = new List<Entity>();
            List.Add(LOGO);
            List.Add(KETNOI);
            List.Add(SLIDE);
            List.Add(FOOTER);
            List.Add(HOTLINE);
            List.Add(SUPPORT);
            return List;
        }
        public static string Name(string key)
        {
            try
            {
                var entity = List.SingleOrDefault(item => item.id.Equals(key));
                return entity.name;
            }
            catch { return string.Empty; }
        }
        public static void bindUnitToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = ListThongTin();
            ddl.DataTextField = "name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn nhóm thông tin -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
    }
    public class ThongTin
    {
        public static List<Entity> List
        {
            get;
            private set;
        }
        public static Entity LOGO_CHINH = new Entity("LOGO_CHINH", " Logo chính");
        public static Entity FACE = new Entity("FACE", " FACE");
        public static Entity GPLUS = new Entity("GPLUS", " GPLUS");
        public static Entity YOUTUBE = new Entity("YOUTUBE", " YOUTUBE");
        public static Entity ZING = new Entity("ZING", " ZING");
        public static Entity SLIDE1 = new Entity("SLIDE1", " SLIDE1");
        public static Entity SLIDE2 = new Entity("SLIDE2", " SLIDE2");
        public static Entity SLIDE3 = new Entity("SLIDE3", " SLIDE3");
        public static Entity SLIDE4 = new Entity("SLIDE4", " SLIDE4");
        public static Entity SLIDE5 = new Entity("SLIDE5", " SLIDE5");
        public static Entity BAN_QUYEN = new Entity("BAN_QUYEN", " Bản quyền");
        public static Entity DIA_CHI = new Entity("DIA_CHI", " Địa chỉ");
        public static Entity EMAIL = new Entity("EMAIL", " EMAIL");
        public static Entity WEBSITE = new Entity("WEBSITE", " WEBSITE");
        public static Entity CSKH = new Entity("CSKH", " CSKH");
        public static Entity KINH_DOANH = new Entity("KINH_DOANH", " Kinh doanh online");
        public static Entity YAHOO1 = new Entity("YAHOO1", " YAHOO1");
        public static Entity YAHOO2 = new Entity("YAHOO2", " YAHOO2");
        public static Entity YAHOO3 = new Entity("YAHOO3", " YAHOO3");

        static ThongTin()
        {
            List = new List<Entity>();
            List.Add(LOGO_CHINH);
            List.Add(FACE);
            List.Add(GPLUS);
            List.Add(YOUTUBE);
            List.Add(ZING);
            List.Add(SLIDE1);
            List.Add(SLIDE2);
            List.Add(SLIDE3);
            List.Add(SLIDE4);
            List.Add(SLIDE5);
            List.Add(BAN_QUYEN);
            List.Add(DIA_CHI);
            List.Add(EMAIL);
            List.Add(WEBSITE);
            List.Add(CSKH);
            List.Add(KINH_DOANH);
            List.Add(YAHOO1);
            List.Add(YAHOO2);
            List.Add(YAHOO3);

        }
        public static List<Entity> ListThongTin()
        {
            List = new List<Entity>();
            List.Add(LOGO_CHINH);
            List.Add(FACE);
            List.Add(GPLUS);
            List.Add(YOUTUBE);
            List.Add(ZING);
            List.Add(SLIDE1);
            List.Add(SLIDE2);
            List.Add(SLIDE3);
            List.Add(SLIDE4);
            List.Add(SLIDE5);
            List.Add(BAN_QUYEN);
            List.Add(DIA_CHI);
            List.Add(EMAIL);
            List.Add(WEBSITE);
            List.Add(CSKH);
            List.Add(KINH_DOANH);
            List.Add(YAHOO1);
            List.Add(YAHOO2);
            List.Add(YAHOO3);
            return List;
        }
        public static string Name(string key)
        {
            try
            {
                var entity = List.SingleOrDefault(item => item.id.Equals(key));
                return entity.name;
            }
            catch { return string.Empty; }
        }
        public static void bindUnitToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = ListThongTin();
            ddl.DataTextField = "name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn nhóm thông tin -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
    }
}
