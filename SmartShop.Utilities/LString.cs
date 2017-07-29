using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartShop.Utilities
{

    /// <summary>
    /// TruongLq - 05/05/2014
    /// Thư viện các hàm thao tác với kiểu String
    /// </summary>
    public static class LString
    {
        /// <summary>
        /// Kiểm tra chuỗi null hoặc rỗng hoặc khoảng trắng
        /// </summary>
        /// <param name="value">Chuỗi cần kiểm tra</param>
        /// <returns>Trả lại true nếu chuỗi null hoặc rỗng hoặc khoảng trắng. Trả lại false nếu chuỗi có giá trị</returns>
        public static bool IsNullOrEmptyOrSpace(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Kiểm tra chuỗi null
        /// </summary>
        /// <param name="value">Chuỗi cần kiểm tra</param>
        /// <returns>Trả lại true nếu chuỗi null. Trả lại false nếu chuỗi không null</returns>
        public static bool IsNull(this string value)
        {
            if (Object.ReferenceEquals(value, null)) return true;

            return false;
        }

        /// <summary>
        /// Kiểm tra một object là kiểu string
        /// </summary>
        /// <typeparam name="T">Kiểu của biến (tự nhận theo biến truyền vào)</typeparam>
        /// <param name="value">Biến cần kiểm tra</param>
        /// <returns>Trả lại true nếu biến cần kiểm tra là kiểu string. Trả lại false nếu không phải là kiểu string</returns>
        public static bool IsString<T>(this T value)
        {
            return Object.ReferenceEquals(typeof(T), typeof(string));
        }

        /// <summary>
        /// Chuyển đổi thành chuỗi trống nếu chuỗi là null hoặc khoảng trắng
        /// </summary>
        /// <param name="value">Chuỗi cần chuyển đổi</param>
        /// <returns>Trả lại chuỗi trống nếu null hoặc khoảng trắng. Trả lại chuỗi gốc nếu có giá trị</returns>
        public static string NullOrSpaceToEmpty(this string value)
        {
            if (value.IsNullOrEmptyOrSpace()) return string.Empty;

            return value;
        }

        /// <summary>
        /// Chuyển đổi thành chuỗi null nếu chuỗi là trống hoặc khoảng trắng
        /// </summary>
        /// <param name="value">Chuỗi cần chuyển đổi</param>
        /// <returns>Trả lại null nếu trống hoặc khoảng trắng. Trả lại chuỗi gốc nếu có giá trị</returns>
        public static string EmptyOrSpaceToNull(this string value)
        {
            if (value.IsNullOrEmptyOrSpace()) return null;

            return value;
        }

        /// <summary>
        /// Ghép nhiều chuỗi trong mảng thành một chuỗi có dấu phân cách
        /// </summary>
        /// <param name="delimiter">Chuỗi phân cách</param>
        /// <param name="values">Mảng các chuỗi cần ghép</param>
        /// <returns>Trả lại chuỗi đã ghép có phân cách. Trả lại null nếu mảng null</returns>
        public static string ArrayToStringByDelimiter(string delimiter, params string[] values)
        {
            return values.ArrayToStringByDelimiter(delimiter);
        }

        /// <summary>
        /// Ghép nhiều chuỗi trong mảng thành một chuỗi có dấu phân cách
        /// </summary>
        /// <param name="values">Mảng các chuỗi cần ghép</param>
        /// <param name="delimiter">Chuỗi phân cách</param>
        /// <returns>Trả lại chuỗi đã ghép có phân cách. Trả lại null nếu mảng null</returns>
        public static string ArrayToStringByDelimiter(this string[] values, string delimiter)
        {
            if (Object.ReferenceEquals(values, null)) return null;

            return String.Join(delimiter, values);
        }

        /// <summary>
        /// Tách chuỗi thành mảng chuỗi bởi dấu phân cách
        /// </summary>
        /// <param name="value">Chuỗi cần phân tách</param>
        /// <param name="delimiter">Các chuỗi phân cách</param>
        /// <returns>Trả lại mảng chuỗi đã phân tách. Trả lại null nếu chuỗi null</returns>
        public static string[] SplitByDelimiter(this string value, params string[] delimiter)
        {
            if (value.IsNull()) return null;

            return value.Split(delimiter, StringSplitOptions.None);
        }

        /// <summary>
        /// Viết hoa ký tự đầu mỗi từ trong chuỗi
        /// </summary>
        /// <param name="value">Chuỗi cần xử lý</param>
        /// <returns>Trả lại chuỗi đã viết hoa ký tự đầu mỗi từ</returns>
        public static string UpperFirstOfWord(this string value)
        {
            string input = value.KeepOneSpace();
            if (value.IsNullOrEmptyOrSpace()) return input;

            string[] words = input.SplitByDelimiter(" ");
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1, words[i].Length - 1);
            }
            return words.ArrayToStringByDelimiter(" ");
        }

        /// <summary>
        /// Viết hoa ký tự đầu của chuỗi
        /// </summary>
        /// <param name="value">Chuỗi cần xử lý</param>
        /// <returns>Trả lại chuỗi đã viết hoa ký tự đầu</returns>
        public static string Capitalize(this string value)
        {
            string input = value.KeepOneSpace();
            if (value.IsNullOrEmptyOrSpace()) return input;

            return input.Substring(0, 1).ToUpper() + input.Substring(1, input.Length - 1);
        }

        /// <summary>
        /// Giữ lại 1 space giữa các từ trong chuỗi
        /// </summary>
        /// <param name="value">Chuỗi cần xử lý</param>
        /// <returns>Trả lại chuỗi chỉ giữ lại 1 space giữa các từ</returns>
        public static string KeepOneSpace(this string value)
        {
            if (value.IsNull()) return null;

            try { return Regex.Replace(value.Trim(), @"[ ]{2,}", @" "); }
            catch { return value; }
        }

        /// <summary>
        /// Đếm số lượng từ trong chuỗi phân biệt bởi dấu phân cách
        /// </summary>
        /// <param name="value">Chuỗi cần đếm số lượng từ</param>
        /// <param name="delimiter">Chuỗi phân cách</param>
        /// <returns>Trả lại số lượng từ của chuỗi</returns>
        public static int CountWordByDelimiter(this string value, string delimiter)
        {
            if (value.IsNullOrEmptyOrSpace()) return 0;

            string[] words = value.SplitByDelimiter(delimiter);
            return words.Length;
        }

        static private bool invalid = false;
        /// <summary>
        /// Kiểm tra định dạng địa chỉ e-mail
        /// </summary>
        /// <param name="value">Chuỗi chứa địa chỉ e-mail cần kiểm tra</param>
        /// <returns>Trả lại true nếu đúng định dạng. Trả lại false nếu sai định dạng</returns>
        public static bool IsEmailAddress(this string value)
        {
            invalid = false;
            if (value.IsNullOrEmptyOrSpace()) return false;

            // Use IdnMapping class to convert Unicode domain names.
            value = Regex.Replace(value, @"(@)(.+)$", DomainMapper);
            if (invalid) return false;

            // Return true if value is in valid e-mail format.
            return Regex.IsMatch(value,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }
        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        /// <summary>
        /// Kiểm tra chuỗi có phải là một số
        /// </summary>
        /// <param name="value">Chuỗi cần kiểm tra</param>
        /// <returns>Trả lại true nếu chuỗi là một số. Trả lại false nếu chuỗi không phải là một số</returns>
        public static bool IsNumeric(this string value)
        {
            // Không sử dụng Regex do sẽ phức tạp trong việc xử lý các dấu thập phân,
            // phân cách phần nghìn... khi người dùng có thể tự định nghĩa các dấu này
            if (value.IsNullOrEmptyOrSpace()) return false;

            decimal d = 0;
            return decimal.TryParse(value, out d);
        }

        /// <summary>
        /// Kiểm tra chuỗi chỉ chứa các chữ cái và chữ số
        /// </summary>
        /// <param name="value">Chuỗi cần kiểm tra</param>
        /// <returns>Trả lại true nếu chuỗi chỉ chứa các chữ cái và chữ số. Trả lại false nếu chuỗi null, trống hoặc chứa các ký tự khác</returns>
        public static bool IsAlphaNumberic(this string value)
        {
            if (value.IsNull()) return false;

            return Regex.Match(value, @"^\w+$").Success;
        }

        /// <summary>
        /// Kiểm tra chuỗi chỉ chứa các chữ cái
        /// </summary>
        /// <param name="value">Chuỗi cần kiểm tra</param>
        /// <returns>Trả lại true nếu chuỗi chỉ chứa các chữ cái. Trả lại false nếu chuỗi null, trống hoặc chứa các ký tự khác</returns>
        public static bool IsAlpha(this string value)
        {
            if (value.IsNull()) return false;

            return Regex.Match(value, @"^\p{L}+$").Success;
        }

        /// <summary>
        /// Tạo chuỗi viết tắt theo ký tự đầu mỗi từ trong chuỗi
        /// </summary>
        /// <param name="value">Chuỗi cần xử lý</param>
        /// <param name="value">Độ dài tối thiểu</param>
        /// <returns>Trả lại viết tắt theo ký tự đầu mỗi từ</returns>
        public static string GetShortDesc(this string value, int length = 0)
        {
            string result = value.KeepOneSpace();
            if (value.IsNullOrEmptyOrSpace()) return result;

            string[] words = result.VNUnsigned().SplitByDelimiter(" ", "-");
            result = "";
            for (int i = 0; i < words.Length; i++)
            {
                result += words[i].Substring(0, 1).ToUpper();
            }
            if (result.Length < length) result += words[words.Length - 1].Substring(words[words.Length - 1].Length - 1, 1).ToUpper();
            return result;
        }

        /// <summary>
        /// Chuyển tiếng Việt thành không dấu
        /// </summary>
        /// <param name="value">Chuỗi cần xử lý</param>
        /// <returns>Chuỗi tiếng Việt không dấu</returns>
        public static string VNUnsigned(this string value)
        {
            Regex result = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string v_str_FormD = value.Normalize(NormalizationForm.FormD);
            return result.Replace(v_str_FormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static byte[] StringToByteArray(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// Tách tên từ họ tên đầy đủ
        /// </summary>
        /// <param name="str">Chuỗi họ tên cần xử lý</param>
        /// <returns>Trả về tên</returns>
        public static string SplitLastName(string str)
        {
            str = str.Trim();
            string[] array = str.Split(' ');
            return array[array.Length - 1].Trim();
        }

        /// <summary>
        /// Tách họ và đệm từ họ tên đầy đủ
        /// </summary>
        /// <param name="str">Chuỗi họ tên cần xử lý</param>
        /// <returns>Trả về họ và tên đệm</returns>
        public static string SplitFirstName(string str)
        {
            str = str.Trim();
            string lastName = SplitLastName(str);
            return str.Replace(lastName, "").Trim();
        }

        /// <summary>
        /// Chuyển thành tên tắt. VD: Lại Văn Sâm -> SamLV or Sam_LV
        /// </summary>
        /// <param name="str">Chuỗi họ tên cần chuyển</param>
        /// <param name="join">Chuỗi cần nối giữa tên và họ</param>
        /// <returns></returns>
        public static string ToShortName(string str, string join = "")
        {
            str = VNUnsigned(str);

            string lastName = SplitLastName(str).ToUpper();


            string firstName = SplitFirstName(str).ToUpper();
            string[] array = firstName.Split(' ');
            string temp = "";
            for (int i = 0; i < array.Length; i++)
            {
                temp = temp + array[i][0];
            }
            temp = temp.ToUpper();


            if (temp.Trim().Length > 0)
                return lastName + join + temp;
            else
                return lastName;
        }

        public static string FormatNumber(decimal number)
        {
            return String.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:n}", number).Replace(",00", "");
        }

        public static string FormatNumber(double number)
        {
            return String.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:n}", number).Replace(",00", "");
        }

        public static string FormatNumber(int number)
        {
            return String.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:n}", number).Replace(",00", "");
        }

        public static string[] GetStringsIncrease(string strBatDau, int iLenght)
        {
            string[] strKetQua = new string[iLenght];
            strKetQua[0] = strBatDau;
            for (int i = 1; i <= iLenght - 1; i++)
            {
                strKetQua[i] = GetNextSring(strKetQua[i - 1]);
            }
            return strKetQua;
        }

        public static string GetNextSring(string value)
        {
            string res = string.Empty;
            char[] Charactors = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] Numbers = "0123456789".ToCharArray();
            char[] cValue = value.ToCharArray();

            int idxThem = -1;
            for (int i = cValue.Length - 1; i >= 0; i--)
            {
                if (char.IsNumber(cValue[i]))
                {
                    if (cValue[i].CompareTo(Numbers[Numbers.Length - 1]) < 0)
                    {
                        idxThem = i;
                        break;
                    }
                }
                else
                {
                    if (cValue[i].CompareTo(Charactors[Charactors.Length - 1]) < 0)
                    {
                        idxThem = i;
                        break;
                    }
                }
            }

            if (idxThem == -1)
            {
                if (char.IsNumber(cValue[0]))
                    cValue = (Numbers[1].ToString() + value).ToCharArray();
                else
                    cValue = (Charactors[0].ToString() + value).ToCharArray();
                idxThem++;
            }
            else
            {
                if (char.IsNumber(cValue[idxThem]))
                {
                    string temp = (Int32.Parse(cValue[idxThem].ToString()) + 1).ToString();
                    cValue[idxThem] = temp.ToCharArray()[0];
                }
                else
                {
                    for (int i = 0; i < Charactors.Length; i++)
                        if (Charactors[i].CompareTo(cValue[idxThem]) == 0)
                        {
                            cValue[idxThem] = Charactors[i + 1];
                            break;
                        }

                }
            }


            for (int i = cValue.Length - 1; i > idxThem; i--)
            {
                if (char.IsNumber(cValue[i]))
                    cValue[i] = Numbers[0];
                else
                    cValue[i] = Charactors[0];
            }

            for (int i = 0; i < cValue.Length; i++)
                res += cValue[i].ToString();

            return res;
        }
    }
}
