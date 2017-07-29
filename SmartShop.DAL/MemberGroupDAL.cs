using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SmartShop.DAL
{
    public class MemberGroupDAL
    {
        public static MemberGroup GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.MemberGroups.SingleOrDefault(c => c.MemberGroupID == id);
        }
        public static IQueryable<MemberGroup> GetAllItem()
        {
            var dc = new SmartShopEntities();
            return dc.MemberGroups.AsQueryable();
        }
        public static void bindMemberGroupToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllItem();
            ddl.DataTextField = "GroupName";
            ddl.DataValueField = "MemberGroupID";
            ddl.DataBind();
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, new ListItem("- Chưa có dữ liệu -", "0"));
            }
            else
            {
                if (!required)
                {
                    ddl.Items.Insert(0, new ListItem("- Chọn loại người dùng -", "0"));
                }
                if (!string.IsNullOrEmpty(valueSelected))
                    ddl.SelectedValue = valueSelected;
            }
        }
    }
}
