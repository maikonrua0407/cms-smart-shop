using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmartShop.Utilities.Helper
{
    public class DeviceUtil
    {
        public static bool DetectMobileBrowser(int width, int height)
        {
            var isMobile = DetectUserAgentBrowser() || DetectSize(width, height);
            return isMobile;
        }
        public static bool DetectSize(int width, int height)
        {
            var isMobile = Math.Min(width, height) <= 480;
            return isMobile;
        }
        public static bool DetectUserAgentBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            var context = HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            var sAgent = context.Request.UserAgent;
            if (!string.IsNullOrEmpty(sAgent))
            {
                sAgent = sAgent.ToLower();
                //Create a list of all mobile types
                var mobiles =
                    new[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone", "iPad2C4", "iPad2C5","JZO54K"
                };
                return !sAgent.Contains("ipad") && mobiles.Any(s => sAgent.Contains(s.ToLower()));
                //Loop through each item in the list created above 
                //and check if the header contains that text
            }

            return false;
        }
    }
}
