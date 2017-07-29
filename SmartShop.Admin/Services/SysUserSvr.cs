using SmartShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SmartShop.Admin
{
    public static class SysUserSvr
    {
        public static bool isAdmin(string username)
        {
            return MemberDAL.isAdmin(username);
        }

        public static bool CheckLoginUserPass(string username, string password)
        {
            return MemberDAL.CheckLoginUserPass(username, password);
        }

        public static bool CheckLoginAdminPass(string username, string password)
        {
            return MemberDAL.CheckLoginAdminPass(username, password);
        }

        public static bool CheckLoginUser(string username)
        {
            return MemberDAL.CheckLoginUser(username);
        }

        public static bool CheckLoginAdmin(string username)
        {
            return MemberDAL.CheckLoginAdmin(username);
        }

        public static Member GetByID(int id)
        {
            return MemberDAL.GetByID(id);
        }

        public static Member GetByUserName(string usernam)
        {
            return MemberDAL.GetByUserName(usernam);
        }

        public static IQueryable<Member> GetAllItem()
        {
            return MemberDAL.GetAllItem();
        }

        public static IQueryable<Member> GetExist_Ma(string Ma_Exist)
        {
            return MemberDAL.GetExist_Ma(Ma_Exist);
        }

        public static IQueryable<Member> GetExist_Email(string Email_Exist)
        {
            return MemberDAL.GetExist_Email(Email_Exist);
        }

        public static bool checkExits_Ma_Update(string Ma_Exist, string Ma_From)
        {
            return MemberDAL.checkExits_Ma_Update(Ma_Exist, Ma_From);
        }

        public static bool CheckExist_Ma_Insert(string Ma)
        {
            return MemberDAL.CheckExist_Ma_Insert(Ma);
        }

        public static bool Insert(Member obj)
        {
            return MemberDAL.Insert(obj);
        }

        public static bool Update(Member objMember)
        {
            return MemberDAL.Update(objMember);
        }

        public static List<Member> GetP_Member_UserName_MemberGroupID_SearchPaging(string userName, int memberGroup, string keyword, int pageSize, int currentPage, ref int? totalRecord)
        {
            return MemberDAL.GetP_Member_UserName_MemberGroupID_SearchPaging(userName, memberGroup, keyword, pageSize, currentPage, ref totalRecord);
        }

        public static bool Delete_ID(int id)
        {
            return MemberDAL.Delete_ID(id);
        }

        public static IQueryable<Member> GetAllMemberItem()
        {
            return MemberDAL.GetAllMemberItem();
        }

        public static void bindMemberToDdl(ref DropDownList ddl, string valueSelected, bool required)
        {
            MemberDAL.bindMemberToDdl(ref ddl, valueSelected, required);
        }

    }
}