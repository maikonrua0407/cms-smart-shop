using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SmartShop.Admin
{
    public class DataCombobox
    {
        public class Page
        {
            public string id;
        }

        private static bool GetIsSelect(string idOri, string idCompare)
        {
            return idOri == idCompare ? true : false;
        }

        public static List<SelectListItem> LoadFontSize(string DefaultSelect = "")
        {
            var option = new List<SelectListItem>();
            for (int i = 0; i < 20; i++)
            {
                option.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = !string.IsNullOrEmpty(DefaultSelect) && DefaultSelect.Equals(i.ToString()) ? true : false
                });
            }
            return option;
        }

        public static List<SelectListItem> LoadComboYear()
        {
            var option = new List<SelectListItem>();
            int iYear = DateTime.Now.Year;
            for (int i = 0; i < 20; i++)
            {
                option.Add(new SelectListItem { Text = (iYear - i).ToString(), Value = (iYear - i).ToString() });
            }
            return option;
        }

        public static List<SelectListItem> LoadComboMonth()
        {
            var option = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                option.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = DateTime.Now.Month == i ? true : false });
            }
            return option;
        }

        /// <summary>
        /// Load datasource Giờ
        /// </summary>
        /// <param name="showAll">hiển thị giá trị mặc định "Giờ"</param>
        /// <returns></returns>
        public static List<SelectListItem> LoadComboGio(bool showAll)
        {
            var option = new List<SelectListItem>();
            if (showAll) option.Add(new SelectListItem { Text = "Giờ", Value = "0" });
            option.Add(new SelectListItem { Text = "6:00 AM", Value = "6" });
            option.Add(new SelectListItem { Text = "7:00 AM", Value = "7" });
            option.Add(new SelectListItem { Text = "8:00 AM", Value = "8" });
            option.Add(new SelectListItem { Text = "9:00 AM", Value = "9" });
            option.Add(new SelectListItem { Text = "10:00 AM", Value = "10" });
            option.Add(new SelectListItem { Text = "11:00 AM", Value = "11" });
            option.Add(new SelectListItem { Text = "12:00 AM", Value = "12" });
            option.Add(new SelectListItem { Text = "1:00 PM", Value = "13" });
            option.Add(new SelectListItem { Text = "2:00 PM", Value = "14" });
            option.Add(new SelectListItem { Text = "3:00 PM", Value = "15" });
            option.Add(new SelectListItem { Text = "4:00 PM", Value = "16" });
            option.Add(new SelectListItem { Text = "5:00 PM", Value = "17" });
            option.Add(new SelectListItem { Text = "6:00 PM", Value = "18" });
            option.Add(new SelectListItem { Text = "7:00 PM", Value = "19" });
            option.Add(new SelectListItem { Text = "8:00 PM", Value = "20" });
            option.Add(new SelectListItem { Text = "9:00 PM", Value = "21" });
            return option;
        }

        public static List<SelectListItem> LoadComboMonthLoadToMoment()
        {
            var option = new List<SelectListItem>();
            for (int i = 1; i <= DateTime.Now.Month; i++)
            {
                option.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = DateTime.Now.Month == i ? true : false });
            }
            return option;
        }

        public static List<SelectListItem> LoadPageSizeCustom()
        {
            var option = new List<SelectListItem>();
            option.Add(new SelectListItem { Text = "10", Value = "10" });
            option.Add(new SelectListItem { Text = "20", Value = "20" });
            option.Add(new SelectListItem { Text = "30", Value = "30" });
            option.Add(new SelectListItem { Text = "40", Value = "40" });
            option.Add(new SelectListItem { Text = "50", Value = "50" });
            option.Add(new SelectListItem { Text = "100", Value = "100" });
            return option;
        }

        public static List<Page> initPageShow()
        {
            var lst = new List<Page>();
            lst.Add(new Page { id = "10" });
            lst.Add(new Page { id = "20" });
            lst.Add(new Page { id = "30" });
            lst.Add(new Page { id = "50" });
            lst.Add(new Page { id = "100" });
            return lst;
        }

        public static string LoadPageShow(string current, string act)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<select name=\"cboPage" + act + "\" id=\"cboPage" + act + "\" style=\"width:60%\" class=\"cssCombobox\" onchange=\"loadPage('1','" + act + "');\">");
            foreach (var d in initPageShow())
            {
                if (current == d.id)
                {
                    sb.AppendLine("<option selected  value=\"" + d.id + "\">" + d.id + "</option>");
                }
                else
                {
                    sb.AppendLine("<option value=\"" + d.id + "\">" + d.id + "</option>");
                }
            }
            sb.AppendLine("</select> ");
            return sb.ToString();
        }
    }
}