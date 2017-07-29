using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace SmartShop.DAL
{
    public class Utility
    {
        /// <summary>
        /// Get database's connection string
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            }
        }

        public static DateTime? ConvertToDateTime(string strDateTime)
        {
            try
            {
                var date = Convert.ToDateTime(Regex.Replace(strDateTime, @"\b(?<day>\d{1,2})/(?<month>\d{1,2})/(?<year>\d{2,4})\b", "${month}/${day}/${year}"));
                return date;
            }
            catch
            {
                try
                {
                    return Convert.ToDateTime(strDateTime.Trim());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        #region KIEM TRA KY TU CO DAU
        /// <summary>
        /// Kiểm tra ký tự có dấu. Nếu chuỗi NULL -> true (có dấu)
        /// </summary>
        public static bool LaKyTuCoDau(string strText)
        {
            string codauChars = "@àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
            int pos = 0;
            for (int i = 0; i < strText.Length; i++)
            {
                if (strText[i] != ' ')
                {
                    pos = codauChars.IndexOf(strText[i].ToString());
                    if (pos > 0)
                        return true;
                }
            }
            return false;
        }
        #region APPLICATION PATH
        /// <summary>
        ///     Lấy địa chỉ gốc
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                string strPath = HttpContext.Current.Request.ApplicationPath;
                if (strPath == "/")
                {
                    return string.Empty;
                }
                return strPath;
            }
        }
        #endregion
        #endregion
        #region TREE NODE
        /// <summary>
        ///     Xử lý Tree Control
        /// </summary>
        public class TreeNode
        {
            public object Value { set; get; }
            public string Display { set; get; }
            public object Parent { set; get; }
        }

        public static void BuildTree(List<TreeNode> list, object parent, ref DropDownList ddl, int level)
        {

            foreach (TreeNode node in list)
            {
                try
                {
                    if (node.Parent.Equals(parent))
                    {
                        ListItem newItem = new ListItem(StringChildNext(level) + node.Display, node.Value.ToString());
                        if (level == 0)
                        {
                            newItem.Attributes.Add("style", "font-weight: bold");
                        }
                        ddl.Items.Add(newItem);

                        level++;
                        BuildTree(list, node.Value, ref ddl, level);
                        level--;
                    }
                }
                catch { }
            }
        }
        public static void BuildTree(List<TreeNode> list, object parent, int level, ref List<ListItem> item)
        {
            try
            {
                foreach (TreeNode node in list)
                {
                    if (node.Parent.Equals(parent))
                    {
                        ListItem newItem = new ListItem(StringChildNext(level) + node.Display, node.Value.ToString());
                        if (level == 0)
                        {
                            newItem.Attributes.Add("style", "font-weight: bold");
                        }
                        item.Add(newItem);
                        level++;
                        BuildTree(list, node.Value, level, ref item);
                        level--;
                    }
                }
            }
            catch { }
        }

        public static string StringChildNext(int level)
        {
            string strNext = string.Empty;
            for (int i = 0; i < level; i++)
                strNext += "...";
            return strNext;
        }
        #endregion
    }
}
