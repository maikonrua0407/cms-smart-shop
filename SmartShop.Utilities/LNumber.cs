using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Utilities
{

    /// <summary>
    /// TruongLq - 05/05/2014
    /// Thư viện các hàm thao tác với kiểu số
    /// </summary>
    public static class LNumber
    {
        /// <summary>
        /// Làm tròn thông thường, lên hay xuống
        /// </summary>
        public enum RoundingType { None, Up, Down }

        /// <summary>
        /// Chuyển đổi kiểu string sang kiểu Int32
        /// </summary>
        /// <param name="value">Chuỗi chứa số cần chuyển đổi</param>
        /// <returns>Trả lại số kiểu Int32</returns>
        public static Int32 StringToInt32(this string value)
        {
            Int32 result;
            if (Int32.TryParse(value, out result)) return result;
            throw new ArgumentException("Số không hợp lệ.", "value = '" + value + "'");
        }

        /// <summary>
        /// Chuyển đổi kiểu string sang kiểu Int64
        /// </summary>
        /// <param name="value">Chuỗi chứa số cần chuyển đổi</param>
        /// <returns>Trả lại số kiểu Int64</returns>
        public static Int64 StringToInt64(this string value)
        {
            Int64 result;
            if (Int64.TryParse(value, out result)) return result;
            throw new ArgumentException("Số không hợp lệ.", "value = '" + value + "'");
        }

        /// <summary>
        /// Chuyển đổi kiểu string sang kiểu decimal
        /// </summary>
        /// <param name="value">Chuỗi chứa số cần chuyển đổi</param>
        /// <returns>Trả lại số kiểu decimal</returns>
        public static decimal StringToDecimal(this string value)
        {
            CultureInfo info = CultureInfo.GetCultureInfo("en-US");
            decimal result;
            if (decimal.TryParse(value, NumberStyles.Any, info, out result)) return result;
            throw new ArgumentException("Số không hợp lệ.", "value = '" + value + "'");
        }

        /// <summary>
        /// Chuyển đổi kiểu string sang kiểu double
        /// </summary>
        /// <param name="value">Chuỗi chứa số cần chuyển đổi</param>
        /// <returns>Trả lại số kiểu double</returns>
        public static double StringToDouble(this string value)
        {
            double result;
            if (double.TryParse(value, out result)) return result;
            throw new ArgumentException("Số không hợp lệ.", "value = '" + value + "'");
        }

        /// <summary>
        /// Chuyển đổi kiểu string sang kiểu float
        /// </summary>
        /// <param name="value">Chuỗi chứa số cần chuyển đổi</param>
        /// <returns>Trả lại số kiểu float</returns>
        public static float StringToFloat(this string value)
        {
            float result;
            if (float.TryParse(value, out result)) return result;
            throw new ArgumentException("Số không hợp lệ.", "value = '" + value + "'");
        }

        /// <summary>
        /// Đọc số thành chữ
        /// </summary>
        /// <param name="value">Số cần đọc</param>
        /// <param name="language">Ngôn ngữ cần đọc</param>
        /// <param name="currency">Loại tiền nếu là số tiền</param>
        /// <returns>Trả lại chuỗi đã đọc</returns>
        public static string ReadNumber(this decimal value, Constant.Languages language = Constant.Languages.Vietnamese, string currency = "VND")
        {
            string result = "";
            string major;
            string minor;
            string temp;
            string[] ones;
            string[] teens;
            string[] tens;
            string[] thousands;
            // Tách các phần nguyên và thập phân
            string[] splitter = value.FormatNumber().SplitByDelimiter(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            decimal intPart = decimal.Parse(splitter[0]);
            decimal decPart = splitter.Length == 1 ? 0 : decimal.Parse(splitter[1]);

            switch (language)
            {
                case Constant.Languages.English:
                    ones = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                    teens = new string[] { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                    tens = new string[] { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                    thousands = new string[] { "", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", "duodecillion", "tredecillion", "quattuordecillion", "quindecillion", "sexdecillion", "septdecillion", "octodecillion", "novemdecillion", "vigintillion" };

                    if (value < 0) { result = "negative "; intPart = Math.Abs(intPart); }

                    // Xử lý phần nguyên
                    string digits;
                    bool showThousands = false;
                    bool allZeros = true;

                    // Use StringBuilder to build result
                    StringBuilder builder = new StringBuilder();
                    // Convert integer portion of value to string
                    digits = intPart.FormatNumber();
                    // Traverse characters in reverse order
                    for (int i = digits.Length - 1; i >= 0; i--)
                    {
                        int ndigit = (int)(digits[i] - '0');
                        int column = (digits.Length - (i + 1));

                        // Determine if ones, tens, or hundreds column
                        switch (column % 3)
                        {
                            case 0:        // Ones position
                                showThousands = true;
                                if (i == 0)
                                {   // First digit in number (last in loop)
                                    temp = String.Format("{0} ", ones[ndigit]);
                                }
                                else if (digits[i - 1] == '1')
                                {   // This digit is part of "teen" value
                                    //temp = String.Format("and {0} ", teens[ndigit]); // en-UK
                                    temp = String.Format("{0} ", teens[ndigit]); // en-US
                                    // Skip tens position
                                    i--;
                                }
                                else if (ndigit != 0)
                                {   // Any non-zero digit
                                    //temp = String.Format("{0}{1} ", digits[i - 1] == '0' ? "and " : "", ones[ndigit]); // en-UK
                                    temp = String.Format("{0} ", ones[ndigit]); // en-US
                                }
                                else
                                {   // This digit is zero. If digit in tens and hundreds
                                    // column are also zero, don't show "thousands"
                                    temp = String.Empty;
                                    // Test for non-zero digit in this grouping
                                    if (digits[i - 1] != '0' || (i > 1 && digits[i - 2] != '0')) showThousands = true;
                                    else showThousands = false;
                                }

                                // Show "thousands" if non-zero in grouping
                                if (showThousands)
                                {
                                    if (column > 0)
                                    {
                                        //temp = String.Format("{0}{1}{2}", temp, thousands[column / 3], allZeros ? " " : builder.ToString().Substring(0, 3) == "and" ? " " : ", "); //en-UK
                                        temp = String.Format("{0}{1}{2}", temp, thousands[column / 3], allZeros ? " " : ", "); // en-US
                                    }
                                    // Indicate non-zero digit encountered
                                    allZeros = false;
                                }
                                builder.Insert(0, temp);
                                break;

                            case 1:        // Tens column
                                if (ndigit > 0)
                                {
                                    //temp = String.Format("and {0}{1}", tens[ndigit], (digits[i + 1] != '0') ? "-" : " "); // en-UK
                                    temp = String.Format("{0}{1}", tens[ndigit], (digits[i + 1] != '0') ? "-" : " "); // en-US
                                    builder.Insert(0, temp);
                                }
                                break;

                            case 2:        // Hundreds column
                                if (ndigit > 0)
                                {
                                    temp = String.Format("{0} hundred ", ones[ndigit]);
                                    builder.Insert(0, temp);
                                }
                                break;
                        }
                    }
                    result = builder.ToString();

                    // Xử lý dấu thập phân
                    switch (currency.NullOrSpaceToEmpty())
                    {
                        case "VND": major = " dong"; minor = " xu"; break;
                        case "USD": major = " US dollars"; minor = " cents"; break;
                        default: major = ""; minor = ""; break;
                    }
                    result += major;

                    // Xử lý phần thập phân
                    if (decPart > 0)
                    {
                        // Tiền tệ chỉ lấy 2 chữ số thập phân
                        if (currency.NullOrSpaceToEmpty() != string.Empty)
                        {
                            decPart = ("0" + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator + decPart.FormatNumber()).StringToDecimal().FormatNumber("0.00").Substring(2, 2).StringToDecimal();
                            result += " and ";
                        }
                        else
                            result += " point "; // Đọc số thường
                        result += decPart.ReadNumber(language, "").ToLower() + minor;
                    }
                    break;

                default: //Vietnamese
                    ones = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
                    thousands = new string[] { "", "nghìn", "triệu", "tỷ" };

                    if (value < 0) { result = "âm "; intPart = Math.Abs(intPart); }

                    // Xử lý phần nguyên (lấy code của eFund)
                    temp = intPart.FormatNumber();
                    string unit; // Thể hiện hàng chục, hàng trăm
                    int position; // Vị trí trong nhóm 3 số
                    int digit; // Chữ số cần đọc
                    // Duyệt (từ trái sang phải) qua từng chữ số
                    for (int i = 0; i < temp.Length; i++)
                    {
                        unit = "";
                        position = (temp.Length - i - 1) % 3;
                        digit = temp.Substring(i, 1).StringToInt32();
                        // Xử lý các trường hợp đặc biệt của số cần đọc
                        switch (digit)
                        {
                            case 1:
                                if ((position == 0) && (i > 0)) // Hàng đơn vị = 1
                                    if ((temp.Substring(i - 1, 1) != "1") && (temp.Substring(i - 1, 1) != "0")) ones[1] = "mốt"; // 21, 31...
                                    else ones[1] = "một"; // 1, 11
                                else
                                    if (position == 1) ones[1] = "mười"; // Hàng chục = 1
                                    else ones[1] = "một";
                                break;

                            case 5:
                                if ((position == 0) && (i != 0)) // Hàng đơn vị = 5
                                    if (temp.Substring(i - 1, 1) != "0") ones[5] = "lăm"; // 15, 25..
                                    else ones[5] = "năm";
                                else ones[5] = "năm";
                                break;

                            case 0:
                                ones[0] = "";
                                if (position == 0 && temp.Length.Equals(1)) ones[0] = "không"; // 0
                                else if (position == 1) // Hàng chục
                                { if (temp.Substring(i + 1, 1) != "0") ones[0] = "linh"; } // 001, 909 ...
                                else if (position == 2) // Hàng trăm
                                { if ((temp.Substring(i + 1, 1) != "0") || (temp.Substring(i + 2, 1) != "0")) ones[0] = "không"; } // 001, 010, 011 ...
                                break;
                        }
                        // Xử lý theo từng vị trí hàng đơn vị, hàng chục, hàng trăm
                        switch (position)
                        {
                            case 2: // Hàng trăm
                                if (i == 0) unit = "trăm";
                                else if ((temp.Substring(i, 1) != "0") || (temp.Substring(i + 1, 1) != "0") || (temp.Substring(i + 2, 1) != "0")) unit = "trăm";
                                break;

                            case 1: // Hàng chục
                                if ((temp.Substring(i, 1) != "1") && (temp.Substring(i, 1) != "0")) unit = "mươi";
                                break;

                            default: // Hàng đơn vị
                                int thousand = (temp.Length - i) / 3; // Vị trí của nghìn, triệu, tỷ
                                if (thousand > 3) thousand = ((thousand - 1) % 3) + 1;
                                if (i > 2) // Từ nhóm 3 số thứ 2
                                {
                                    if (temp.Substring(i - 2, 1) != "0" || temp.Substring(i - 1, 1) != "0" || temp.Substring(i, 1) != "0" || i == temp.Length)
                                        unit = thousands[thousand]; // Chỉ thêm nghìn, triệu, tỷ nếu cả nhóm khác 0
                                }
                                else unit = thousands[thousand];
                                break;
                        }
                        result = result.Trim() + (ones[digit] == "" ? " " : " " + ones[digit] + " ") + unit;
                    }
                    result = result.Trim();

                    // Xử lý dấu thập phân
                    switch (currency.NullOrSpaceToEmpty())
                    {
                        case "VND": major = " đồng"; minor = " xu"; break;
                        case "USD": major = " đô la Mỹ"; minor = " xen"; break;
                        default: major = ""; minor = ""; break;
                    }
                    result += major;

                    // Xử lý phần thập phân
                    if (decPart > 0)
                    {
                        // Tiền tệ chỉ lấy 2 chữ số thập phân
                        if (currency.NullOrSpaceToEmpty() != string.Empty)
                        {
                            decPart = ("0" + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator + decPart.FormatNumber()).StringToDecimal().FormatNumber("0.00").Substring(2, 2).StringToDecimal();
                            result += " và ";
                        }
                        else
                            result += " phảy "; // Đọc số thường
                        result += decPart.ReadNumber(language, "").ToLower() + minor;
                    }
                    break;
            }
            result += ".";
            return result.Capitalize();
        }

        /// <summary>
        /// Chuyển đổi kiểu decimal sang kiểu string theo format
        /// </summary>
        /// <param name="value">Số cần chuyển đổi</param>
        /// <param name="format">Format (dạng số) của chuỗi kết quả</param>
        /// <returns>Trả lại chuỗi chứa số đã chuyển đổi theo format</returns>
        public static string FormatNumber(this decimal value, string format = "")
        {
            return value.ToString(format);
        }

        /// <summary>
        /// Làm tròn số
        /// </summary>
        /// <param name="value">Số cần làm tròn</param>
        /// <param name="digits">Số chữ số thập phân sau khi làm tròn</param>
        /// <param name="rounding">Làm tròn lên hay xuống</param>
        /// <returns>Trả lại số đã làm tròn</returns>
        public static decimal Rounding(this decimal value, int digits, RoundingType rounding = RoundingType.None)
        {
            double result = (double)value;
            int number = digits;

            // Làm tròn trước số thập phân => chia để làm tròn như sau số thập phân
            if (digits < 0)
            {
                result = result / Math.Pow(10, -digits);
                number = 0;
            }

            // Làm tròn sau số thập phân
            // Chỉ xử lý nếu không phải làm tròn (cần thiết để Rounding.Up không bị làm tròn lên khi không cần làm tròn)
            if (result != result.ToString("0.".PadRight(2 + number, '#')).StringToDouble())
            {
                // Số lẻ cộng thêm để làm tròn lên hoặc trừ đi để làm tròn xuống
                double updown = 0.5 / Math.Pow(10, number);
                // Đảo dấu nếu là số âm
                if (result < 0) updown = 0 - updown;

                switch (rounding)
                {
                    case RoundingType.Up: result += updown; break;
                    case RoundingType.Down: result -= updown; break;
                }
            }
            // Làm tròn
            result = Math.Round(result, number, MidpointRounding.AwayFromZero);

            // Khôi phục giá trị trước số thập phân
            if (digits < 0) result = result * Math.Pow(10, -digits);

            return (decimal)result;
        }

        /// <summary>
        /// Chuyển đổi kiểu double sang kiểu long chỉ lấy phần nguyên
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Trả lại phần nguyên</returns>
        internal static long FloorDouble2Long(this double value)
        {
            return (long)Math.Floor(value);
        }

        /// <summary>
        /// Chuyển đổi kiểu object thành decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(object value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Chuyển đổi kiểu object thành double
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble(object value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Chuyển đổi kiểu object thành string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(object value)
        {
            try
            {
                return Convert.ToString(value);
            }
            catch
            {
                return "";
            }
        }

    }
}
