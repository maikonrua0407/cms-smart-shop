using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.DAL
{
    public class WebInfoDAL
    {
        public static bool Update(WebInfo obj)
        {
            var dc = new SmartShopEntities();
            try
            {
                WebInfo wi = dc.WebInfoes.FirstOrDefault();
                int id = wi.InforID;
                wi.NameLiveChat1 = obj.NameLiveChat1;
                wi.YahooLiveChat1 = obj.YahooLiveChat1;
                wi.PhoneLiveChat1 = obj.PhoneLiveChat1;
                wi.NameLiveChat2 = obj.NameLiveChat2;
                wi.YahooLiveChat2 = obj.YahooLiveChat2;
                wi.PhoneLiveChat2 = obj.PhoneLiveChat2;
                wi.HotLine1 = obj.HotLine1;
                wi.HotLine2 = obj.HotLine2;
                wi.NameSupport1 = obj.NameSupport1;
                wi.YahooSupport1 = obj.YahooSupport1;
                wi.PhoneSupport1 = obj.PhoneSupport1;
                wi.NameSupport2 = obj.NameSupport2;
                wi.YahooSupport2 = obj.YahooSupport2;
                wi.PhoneSupport2 = obj.PhoneSupport2;
                wi.SupportSale_Comp = obj.SupportSale_Comp;
                wi.SupportSale_Address = obj.SupportSale_Address;
                wi.SupportSale_Email = obj.SupportSale_Email;
                wi.SupportSale_Web = obj.SupportSale_Web;
                wi.SupportSale_Phone1 = obj.SupportSale_Phone1;
                wi.SupportSale_Phone2 = obj.SupportSale_Phone2;
                wi.Bank_AcountName = obj.Bank_AcountName;
                wi.Bank1_Name = obj.Bank1_Name;
                wi.Bank1_BranchName = obj.Bank1_BranchName;
                wi.Bank1_Acount = obj.Bank1_Acount;
                wi.Bank1_Logo = obj.Bank1_Logo;
                wi.Bank2_Name = obj.Bank2_Name;
                wi.Bank2_BranchName = obj.Bank2_BranchName;
                wi.Bank2_Acount = obj.Bank2_Acount;
                wi.Bank2_Logo = obj.Bank2_Logo;
                wi.Bank3_Name = obj.Bank3_Name;
                wi.Bank3_BranchName = obj.Bank3_BranchName;
                wi.Bank3_Acount = obj.Bank3_Acount;
                wi.Bank3_Logo = obj.Bank3_Logo;
                wi.TwitterLink = obj.TwitterLink;
                wi.FacebookLink = obj.FacebookLink;
                wi.GooglePlusLink = obj.GooglePlusLink;
                wi.YouTubeLink = obj.YouTubeLink;
                dc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static WebInfo GetInfo()
        {
            var dc = new SmartShopEntities();
            return dc.WebInfoes.FirstOrDefault();
        }
    }
}