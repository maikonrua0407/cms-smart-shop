using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PositionEx
/// </summary>
/// 
namespace SmartShop.DAL
{
    public class PositionEx
    {
        public static string bannerHome { get { return "bannerHome"; } }
        public static string slideHome { get { return "slideHome"; } }
        public static string todaysaleHome { get { return "todaysaleHome"; } }
        public static string mainLogo { get { return "mainLogo"; } }
        public static string subLogo { get { return "subLogo"; } }
        public static string categoryHome { get { return "categoryHome"; } }
        public static string AdvChild { get { return "AdvChild"; } }

        public static Dictionary<string, string> List { get; private set; }

        static PositionEx()
        {
            List = new Dictionary<string, string>();
            List.Add(mainLogo, "Logo lớn");
            List.Add(subLogo, "Logo nhỏ");
            List.Add(bannerHome, "Banner trang chủ");
            List.Add(slideHome, "Slide trang chủ");
            List.Add(todaysaleHome, "Today Sales");
            List.Add(categoryHome, "Danh mục sản phẩm");
            List.Add(AdvChild, "Quảng cáo");
        }

        public static string Name(string posType)
        {
            posType = posType.Trim();
            string str = List.ContainsKey(posType) ? List[posType] : string.Empty;
            return str;
        }
    }
}