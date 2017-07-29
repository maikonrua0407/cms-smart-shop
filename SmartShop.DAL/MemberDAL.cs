using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;

namespace SmartShop.DAL
{
    public class MemberDAL
    {
        public static string UserName
        {
            get;
            set;
        }
        public static bool isAdmin(string username)
        {
            try
            {
                var result = false;
                var dc = new SmartShopEntities();
                var obj = dc.Members.SingleOrDefault(c => c.AcountLogin == username);
                if (obj.MemberGroupID == 1)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }            
        }
        public static bool CheckLoginUserPass(string username, string password)
        {
            try
            {
                var result = false;
                var dc = new SmartShopEntities();
                var obj = dc.Members.SingleOrDefault(c => c.AcountLogin == username && c.AcountPass == password);
                if (obj!=null && obj.Acitve==true)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }            
        }
        public static bool CheckLoginAdminPass(string username, string password)
        {
            try
            {
                var result = false;
                var dc = new SmartShopEntities();
                var obj = dc.Members.SingleOrDefault(c => c.AcountLogin == username && c.AcountPass == password);
                if (obj != null && (obj.MemberGroupID == 1 || obj.MemberGroupID == 2 || obj.MemberGroupID == 4) && obj.Acitve == true)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool CheckLoginUser(string username)
        {
            try
            {
                var result = false;
                var dc = new SmartShopEntities();
                var obj = dc.Members.SingleOrDefault(c => c.AcountLogin == username);
                if (obj != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool CheckLoginAdmin(string username)
        {
            try
            {
                var result = false;
                var dc = new SmartShopEntities();
                var obj = dc.Members.SingleOrDefault(c => c.AcountLogin == username);
                if (obj != null && (obj.MemberGroupID == 1 || obj.MemberGroupID == 2 || obj.MemberGroupID == 4))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Member GetByID(int id)
        {
            var dc = new SmartShopEntities();
            return dc.Members.SingleOrDefault(c => c.MemberID == id);
        }
        public static Member GetByUserName(string usernam)
        {
            var dc = new SmartShopEntities();
            return dc.Members.SingleOrDefault(c => c.AcountLogin == usernam);
        }
        public static IQueryable<Member> GetAllItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.Members.AsQueryable();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static IQueryable<Member> GetExist_Ma(string Ma_Exist)
        {
            try
            {
                var dc = new SmartShopEntities();
                var list = dc.Members.Where(n => n.AcountLogin != Ma_Exist);
                return list.AsQueryable();
            }
            catch
            {
                return null;
            }
        }
        public static IQueryable<Member> GetExist_Email(string Email_Exist)
        {
            try
            {
                var dc = new SmartShopEntities();
                var list = dc.Members.Where(n => n.Email != Email_Exist);
                return list.AsQueryable();
            }
            catch
            {
                return null;
            }
        }
        public static bool checkExits_Ma_Update(string Ma_Exist, string Ma_From)
        {
            try
            {
                var listExist = GetExist_Ma(Ma_Exist);
                var list = listExist.Where(n => n.AcountLogin == Ma_From);
                return list.Count() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool CheckExist_Ma_Insert(string Main)
        {
            try
            {
                var dc = new SmartShopEntities();
                return dc.Members.Where(So => So.AcountLogin == Main).Count() > 0;
            }
            catch
            {
                return false;
            }
        }
        public static bool Insert(Member obj)
        {
            try
            {
                var dc = new SmartShopEntities();
                Member m = new Member();
                m = obj;
                dc.Members.Add(m);
                dc.SaveChanges();
                if (m.MemberGroupID == 2)
                    m.ManageBy = m.MemberID;
                dc.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Update(Member objMember)
        {
            try
            {
                var dc = new SmartShopEntities();
                var obj = dc.Members.SingleOrDefault(c => c.MemberID == objMember.MemberID);
                if (obj != null)
                {
                    obj.MemberGroupID = objMember.MemberGroupID;
                    obj.AcountLogin = objMember.AcountLogin;
                    //string strPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(objMember.AcountPass, "SHA1");
                    obj.AcountPass = objMember.AcountPass;
                    obj.TrueName = objMember.TrueName;
                    obj.Address = objMember.Address;
                    obj.Email = objMember.Email;
                    obj.DateOfBirth = objMember.DateOfBirth;
                    obj.ManageBy = objMember.ManageBy;
                    obj.Phone = objMember.Phone;
                    obj.AcountBank = objMember.AcountBank;
                    obj.BranchOfBank = objMember.BranchOfBank;
                    obj.Note = objMember.Note;
                    obj.Acitve = objMember.Acitve;
                    dc.SaveChanges();
                    if (obj.MemberGroupID == 2)
                        obj.ManageBy = obj.MemberID;
                    dc.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<Member> GetP_Member_UserName_MemberGroupID_SearchPaging(string userName, int memberGroup, string keyword, int pageSize, int currentPage, ref int? totalRecord)
        {
            try
            {
                var dc = new SmartShopEntities();
                var query = dc.P_Member_UserName_MemberGroupID_SearchPaging(userName, memberGroup, keyword, pageSize, currentPage, new System.Data.Objects.ObjectParameter("totalRecord",totalRecord));
                var lst = query.ToList();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool Delete_ID(int id)
        {
            try
            {
                var dc = new SmartShopEntities();
                var obj = dc.Members.SingleOrDefault(c => c.MemberID == id);
                obj.ManageBy = 0;
                dc.SaveChanges();               
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static IQueryable<Member> GetAllMemberItem()
        {
            try
            {
                var dc = new SmartShopEntities();
                var lst = dc.Members.Where(c=>c.MemberGroupID==2 || c.MemberGroupID==1).OrderBy(c=>c.MemberGroupID).OrderBy( c=>c.AcountLogin).AsQueryable();
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static void bindMemberToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            ddl.Items.Clear();
            ddl.DataSource = GetAllMemberItem().Where(e=>e.AcountLogin.Equals("sa")==false);
            ddl.DataTextField = "AcountLogin";
            ddl.DataValueField = "MemberID";
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
