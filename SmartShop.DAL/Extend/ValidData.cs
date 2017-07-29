using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace SmartShop.DAL
{
    /// <summary>
    /// Summary description for ValidData
    /// </summary>
    public class ValidData
    {
        public static bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }
        public static bool IsDateTime(string pText)
        {
            Regex regex = new Regex(@"^(\d{1,2})(\/|-|.)(\d{1,2})(\/|-|.)(\d{2,4})$");
            return regex.IsMatch(pText);
        }
    }
}