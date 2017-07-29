using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Utilities
{
    /// <summary>
    /// TruongLq - 05/05/2014
    /// Thư viện các hàm thao tác với kiểu DateTime
    /// </summary>
    public static class LDateTime
    {
        /// <summary>
        /// Kiểm tra chuỗi kiểu DateTime theo format
        /// </summary>
        /// <param name="value">Chuỗi cần kiểm tra</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi cần kiểm tra</param>
        /// <returns>Trả lại true nếu là chuỗi chứa ngày tháng. Trả lại false nếu chuỗi không chứa ngày tháng hoặc không đúng format</returns>
        public static bool IsDate(this string value, string format = "dd/MM/yyyy")
        {
            // Không có dữ liệu
            if (value.IsNullOrEmptyOrSpace()) return false;

            bool result = false;
            DateTime dt;
            // Không có format
            if (format.IsNullOrEmptyOrSpace()) result = DateTime.TryParse(value, out dt);
            //Có format
            // InvariantCulture để format không phụ thuộc môi trường
            else result = DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            return dt.IsDate();
        }

        /// <summary>
        /// Kiểm tra ngày tháng theo chuẩn MSSQL
        /// </summary>
        /// <param name="value">Ngày tháng cần kiểm tra</param>
        /// <returns>Trả lại true nếu theo chuẩn MSSQL. Trả lại false nếu không theo chuẩn MSSQL</returns>
        private static bool IsDate(this DateTime value)
        {
            return (value - GetMinDate()).Days >= 0;
        }

        /// <summary>
        /// Chuyển đổi kiểu DateTime sang kiểu int
        /// </summary>
        /// <param name="value">Giá trị ngày tháng cần chuyển đổi</param>
        /// <returns>Trả lại giá trị số</returns>
        public static int DateToInt(this DateTime value)
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");

            // Tương tự hàm của MSSQL (làm tròn nửa ngày)
            // Tính từ 01/01/1990
            return int.Parse(Math.Round((value - DateTime.MinValue.AddYears(1899)).TotalDays, MidpointRounding.AwayFromZero).ToString());
        }

        /// <summary>
        /// Chuyển đổi kiểu int sang kiểu DateTime
        /// </summary>
        /// <param name="value">Giá trị số cần chuyển đổi</param>
        /// <returns>Trả lại giá trị ngày tháng</returns>
        public static DateTime IntToDate(this int value)
        {
            // Tương tự hàm của MSSQL
            // 01/01/1753
            if (value < -53690) throw new ArgumentException("Giá trị cần chuyển đổi nhỏ hơn giá trị cho phép là -53690.", "value = " + value.ToString() + "");
            // 31/12/9999
            if (value > 2958463) throw new ArgumentException("Giá trị cần chuyển đổi lớn hơn giá trị cho phép là 2958463.", "value = " + value.ToString() + "");
            // Tính từ 01/01/1990
            return DateTime.MinValue.AddYears(1899).PlusDays(value);
        }

        /// <summary>
        /// Chuyển đổi kiểu string sang kiểu DateTime theo format
        /// </summary>
        /// <param name="value">Chuỗi cần chuyển đổi</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi cần chuyển đổi</param>
        /// <returns>Trả lại ngày tháng đã chuyển đổi</returns>
        public static DateTime StringToDate(this string value, string format = "dd/MM/yyyy")
        {
            // Không có dữ liệu
            if (value.IsNullOrEmptyOrSpace())
                throw new ArgumentException("Không có giá trị ngày tháng.", "value = '" + value + "'");

            DateTime result;
            if (format.IsNullOrEmptyOrSpace())
            {
                // Không có format
                if (!DateTime.TryParse(value, out result))
                    throw new ArgumentException("Ngày tháng không hợp lệ.", "value = '" + value + "', format = '" + format + "'");
            }
            else
            {
                // Có format
                // Xử lý để độ dài value và format bằng nhau
                // InvariantCulture để format không phụ thuộc môi trường
                if (!DateTime.TryParseExact(value.Length > format.Length ? value.Substring(0, format.Length) : value, format.Length > value.Length ? format.Substring(0, value.Length) : format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                    throw new ArgumentException("Ngày tháng không hợp lệ hoặc không phù hợp với format.", "value = '" + value + "', format = '" + format + "'");
            }

            // Kiểm tra kết quả có thỏa mãn chuẩn MSSQL
            if (!IsDate(result))
                throw new ArgumentException("Ngày tháng nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value + "', format = '" + format + "'");

            return result;
        }

        /// <summary>
        /// Chuyển đổi kiểu DateTime sang kiểu string theo format
        /// </summary>
        /// <param name="value">Ngày tháng cần chuyển đổi</param>
        /// <param name="format">Format (dạng ngày tháng) của kết quả</param>
        /// <returns>Trả lại chuỗi chứa ngày tháng theo format</returns>
        public static string DateToString(this DateTime value, string format = "dd/MM/yyyy")
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");

            if (format.IsNullOrEmptyOrSpace())
            {
                // Không có format
                return value.ToString();
            }
            else
            {
                // Có format
                // InvariantCulture để format không phụ thuộc môi trường
                return value.ToString(format, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Đọc ngày tháng năm theo ngôn ngữ truyền vào
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đọc</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi cần đọc</param>
        /// <param name="language">Chuỗi xác định ngôn ngữ (cần điều chỉnh khi có biến toàn cục)</param>
        /// <returns>Trả lại chuỗi dạng ngày tháng năm theo ngôn ngữ truyền vào</returns>
        public static string ReadDateTime(this string value, string format, Constant.Languages language = Constant.Languages.Vietnamese)
        {
            return value.StringToDate(format).ReadDateTime(language);
        }

        /// <summary>
        /// Đọc ngày tháng năm theo ngôn ngữ truyền vào
        /// </summary>
        /// <param name="value">Ngày tháng cần đọc</param>
        /// <param name="language">Chuỗi xác định ngôn ngữ (cần điều chỉnh khi có biến toàn cục)</param>
        /// <returns>Trả lại chuỗi dạng ngày tháng năm theo ngôn ngữ truyền vào</returns>
        public static string ReadDateTime(this DateTime value, Constant.Languages language = Constant.Languages.Vietnamese)
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");

            switch (language)
            {
                case Constant.Languages.English:
                    return String.Format(new CultureInfo("en-US"), "{0:dddd}, {0:MMMM} {1}, {0:yyyy}", value, value.Day.ToOrdinal());
                default: // Ngôn ngữ mặc định là tiếng Việt
                    return String.Format("ngày {0:dd} tháng {0:MM} năm {0:yyyy}", value);
            }
        }
        private static string ToOrdinal(this int number)
        {
            if (number < 0) return number.ToString();
            int rem = number % 100;
            if (rem >= 11 && rem <= 13) return number + "th";
            switch (number % 10)
            {
                case 1:
                    return number + "st";
                case 2:
                    return number + "nd";
                case 3:
                    return number + "rd";
                default:
                    return number + "th";
            }
        }

        /// <summary>
        /// Hiệu của 2 ngày
        /// </summary>
        /// <param name="value">Ngày tháng thứ nhất</param>
        /// <param name="minusDate">Ngày tháng thứ hai</param>
        /// <returns>Trả lại số ngày chênh lệch</returns>
        public static int MinusDate(this DateTime value, DateTime minusDate)
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng thứ nhất nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");
            if (!IsDate(minusDate))
                throw new ArgumentException("Ngày tháng thứ hai nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "minusDate = '" + minusDate.ToString("dd/MM/yyyy") + "'");

            return (value - minusDate).Days;
        }

        /// <summary>
        /// Hiệu của 2 ngày
        /// </summary>
        /// <param name="value">Ngày tháng thứ nhất</param>
        /// <param name="minusDate">Ngày tháng thứ hai</param>
        /// <returns>Trả lại số giây chênh lệch</returns>
        public static int MinusSeconds(this DateTime value, DateTime minusDate)
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng thứ nhất nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");
            if (!IsDate(minusDate))
                throw new ArgumentException("Ngày tháng thứ hai nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "minusDate = '" + minusDate.ToString("dd/MM/yyyy") + "'");

            return (value - minusDate).Seconds;
        }

        /// <summary>
        /// Hiệu của 2 ngày
        /// </summary>
        /// <param name="value">Ngày tháng thứ nhất</param>
        /// <param name="minusDate">Ngày tháng thứ hai</param>
        /// <param name="format">Format (dạng ngày tháng) của các chuỗi chứa ngày tháng cần trừ</param>
        /// <returns>Trả lại số ngày chênh lệch</returns>
        public static int MinusDate(this string value, string minusDate, string format = "dd/MM/yyyy")
        {
            return MinusDate(value.StringToDate(format), minusDate.StringToDate(format));
        }

        /// <summary>
        /// Cộng ngày
        /// </summary>
        /// <param name="value">Ngày tháng cần cộng</param>
        /// <param name="days">Số ngày cần cộng</param>
        /// <returns>Trả lại ngày tháng đã cộng</returns>
        public static DateTime PlusDays(this DateTime value, int days)
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");

            DateTime result;
            try { result = value.AddDays(days); }
            catch { throw new ArgumentException("Kết quả không hợp lệ.", "value = '" + value.ToString("dd/MM/yyyy") + "', days = " + days.ToString()); }

            // Kiểm tra kết quả có thỏa mãn chuẩn MSSQL
            if (!IsDate(result))
                throw new ArgumentException("Kết quả nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "result = '" + result.ToString("dd/MM/yyyy") + "'");

            return result;
        }

        /// <summary>
        /// Cộng ngày
        /// </summary>
        /// <param name="value">Ngày tháng cần cộng</param>
        /// <param name="quantity">Số lượng cần cộng</param>
        /// <param name="unit">Đơn vị thời gian</param>
        /// <returns>Trả lại ngày tháng đã cộng</returns>
        public static DateTime PlusDaysComposite(this DateTime value, int quantity, string unit)
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");

            DateTime result;
            try
            {
                if (unit.Equals(Constant.TAN_SUAT.NGAY.layGiaTri()))
                    result = value.AddDays(quantity);
                else if (unit.Equals(Constant.TAN_SUAT.TUAN.layGiaTri()))
                {
                    quantity = quantity * 7;
                    result = value.AddDays(quantity);
                }
                else if (unit.Equals(Constant.TAN_SUAT.THANG.layGiaTri()))
                    result = value.AddMonths(quantity);
                else if (unit.Equals(Constant.TAN_SUAT.QUY.layGiaTri()))
                {
                    quantity = quantity * 3;
                    result = value.AddMonths(quantity);
                }
                else if (unit.Equals(Constant.TAN_SUAT.NAM.layGiaTri()))
                    result = value.AddYears(quantity);
                else
                    result = value;
            }
            catch { throw new ArgumentException("Kết quả không hợp lệ.", "value = '" + value.ToString("dd/MM/yyyy") + "', " + unit + " = " + quantity.ToString()); }

            // Kiểm tra kết quả có thỏa mãn chuẩn MSSQL
            if (!IsDate(result))
                throw new ArgumentException("Kết quả nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "result = '" + result.ToString("dd/MM/yyyy") + "'");

            return result;
        }

        /// <summary>
        /// Cộng ngày
        /// </summary>
        /// <param name="value">Ngày tháng cần cộng</param>
        /// <param name="days">Số ngày cần cộng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi kết quả</param>
        /// <returns>Trả lại chuỗi chứa ngày tháng đã cộng theo format</returns>
        public static string PlusDays(this DateTime value, int days, string format)
        {
            return PlusDays(value, days).DateToString(format);
        }

        /// <summary>
        /// Cộng ngày
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần cộng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần cộng</param>
        /// <param name="days">Số ngày cần cộng</param>
        /// <returns>Trả lại ngày tháng đã cộng</returns>
        public static DateTime PlusDays(this string value, string format, int days)
        {
            return PlusDays(value.StringToDate(format), days);
        }

        /// <summary>
        /// Cộng ngày
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần cộng</param>
        /// <param name="days">Số ngày cần cộng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần cộng và chuỗi kết quả</param>
        /// <returns>Trả lại chuỗi chứa ngày tháng đã cộng theo format</returns>
        public static string PlusDays(this string value, int days, string format)
        {
            return PlusDays(value.StringToDate(format), days).DateToString(format);
        }

        /// <summary>
        /// Trừ ngày
        /// </summary>
        /// <param name="value">Ngày tháng cần trừ</param>
        /// <param name="days">Số ngày cần trừ</param>
        /// <returns>Trả lại ngày tháng đã trừ</returns>
        public static DateTime MinusDays(this DateTime value, int days)
        {
            return PlusDays(value, -days);
        }

        /// <summary>
        /// Trừ ngày
        /// </summary>
        /// <param name="value">Ngày tháng cần trừ</param>
        /// <param name="days">Số ngày cần trừ</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi kết quả</param>
        /// <returns>Trả lại chuỗi chứa ngày tháng đã trừ theo format</returns>
        public static string MinusDays(this DateTime value, int days, string format)
        {
            return value.MinusDays(days).DateToString(format);
        }

        /// <summary>
        /// Trừ ngày
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần trừ</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần trừ</param>
        /// <param name="days">Số ngày cần trừ</param>
        /// <returns>Trả lại ngày tháng đã trừ</returns>
        public static DateTime MinusDays(this string value, string format, int days)
        {
            return value.StringToDate(format).MinusDays(days);
        }

        /// <summary>
        /// Trừ ngày
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần trừ</param>
        /// <param name="days">Số ngày cần trừ</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần trừ và chuỗi kết quả</param>
        /// <returns>Trả lại chuỗi chứa ngày tháng đã trừ theo format</returns>
        public static string MinusDays(this string value, int days, string format)
        {
            return value.StringToDate(format).MinusDays(days).DateToString(format);
        }

        /// <summary>
        /// Lấy ngày tháng hiện tại của hệ điều hành
        /// </summary>
        /// <returns>Trả lại ngày tháng hiện tại của hệ điều hành</returns>
        public static DateTime GetCurrentDate()
        {
            DateTime result = DateTime.Today;
            // Kiểm tra kết quả có thỏa mãn chuẩn MSSQL
            if (!IsDate(result))
                throw new ArgumentException("Kết quả nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "result = '" + result.ToString("dd/MM/yyyy") + "'");

            return result;
        }

        /// <summary>
        /// Lấy ngày tháng hiện tại của hệ điều hành
        /// </summary>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi kết quả</param>
        /// <returns>Trả lại chuỗi chứa ngày tháng hiện tại của hệ điều hành theo format</returns>
        public static string GetCurrentDate(string format = "dd/MM/yyyy")
        {
            return GetCurrentDate().DateToString(format);
        }

        /// <summary>
        /// Lấy ngày tháng nhỏ nhất
        /// </summary>
        /// <returns>Trả lại ngày tháng nhỏ nhất</returns>
        public static DateTime GetMinDate()
        {
            // Tương tự MSSQL 01/01/1753
            return DateTime.MinValue.AddYears(1752);
        }

        /// <summary>
        /// Lấy ngày tháng nhỏ nhất
        /// </summary>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi kết quả</param>
        /// <returns>Trả lại chuỗi chứa ngày tháng nhỏ nhất theo format</returns>
        public static string GetMinDate(this string format)
        {
            return GetMinDate().DateToString(format);
        }

        /// <summary>
        /// Lấy ngày tháng lớn nhất
        /// </summary>
        /// <returns>Trả lại ngày tháng lớn nhất</returns>
        public static DateTime GetMaxDate()
        {
            return DateTime.MaxValue;
        }

        /// <summary>
        /// Lấy ngày tháng lớn nhất
        /// </summary>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi kết quả</param>
        /// <returns>Trả lại chuỗi chứa ngày tháng lớn nhất theo format</returns>
        public static string GetMaxDate(this string format)
        {
            return GetMaxDate().DateToString(format);
        }

        /// <summary>
        /// Lấy ngày
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần lấy ngày</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng</param>
        /// <returns>Trả lại ngày của chuỗi chứa ngày tháng cần lấy</returns>
        public static int GetDay(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).GetDay();
        }

        /// <summary>
        /// Lấy ngày
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày</param>
        /// <returns>Trả lại ngày của ngày tháng cần lấy</returns>
        public static int GetDay(this DateTime value)
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");

            return value.Day;
        }

        /// <summary>
        /// Lấy số ngày giữa khoảng cách 2 ngày
        /// </summary>
        /// <param name="value">Ngày tháng thứ nhất</param>
        /// <param name="otherDate">Ngày tháng thứ hai</param>
        /// <returns>Trả lại số ngày giữa khoảng cách 2 ngày</returns>
        public static int CountDayBetweenDates(this DateTime value, DateTime otherDate)
        {
            return Math.Abs(MinusDate(value, otherDate));
        }

        /// <summary>
        /// Lấy số ngày giữa khoảng cách 2 ngày
        /// </summary>
        /// <param name="value">Ngày tháng thứ nhất</param>
        /// <param name="otherDate">Ngày tháng thứ hai</param>
        /// <param name="format">Format (dạng ngày tháng) của các chuỗi chứa ngày tháng cần trừ</param>
        /// <returns>Trả lại số ngày giữa khoảng cách 2 ngày</returns>
        public static int CountDayBetweenDates(this string value, string otherDate, string format = "dd/MM/yyyy")
        {
            return CountDayBetweenDates(value.StringToDate(format), otherDate.StringToDate(format));
        }

        /// <summary>
        /// Lấy số ngày trong tháng hiện tại của hệ điều hành
        /// </summary>
        /// <returns>Trả lại số ngày trong tháng hiện tại của hệ điều hành</returns>
        public static int CountDayOfCurrentMonth()
        {
            return GetCurrentDate().CountDayOfMonth();
        }

        /// <summary>
        /// Lấy số ngày trong tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy số ngày trong tháng</param>
        /// <returns>Trả lại số ngày trong tháng</returns>
        public static int CountDayOfMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.GetYear(), value.GetMonth());
        }

        /// <summary>
        /// Lấy số ngày trong tháng
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần lấy số ngày trong tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần lấy số ngày trong tháng</param>
        /// <returns>Trả lại số ngày trong tháng</returns>
        public static int CountDayOfMonth(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountDayOfMonth();
        }

        /// <summary>
        /// Số lượng thứ trong tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ trong tháng</param>
        /// <param name="weekDay">Thứ cần đếm</param>
        /// <returns>Trả lại số lượng thứ trong tháng</returns>
        public static int CountDOWOfMonth(this DateTime value, DayOfWeek weekDay)
        {
            // Lấy ngày đầu tháng
            DateTime startDate = value.GetFirstDateOfMonth();
            // Tìm ngày đầu tiên trùng với thứ
            while (startDate.DayOfWeek != weekDay) startDate = startDate.PlusDays(1);
            // Cộng số lượng thứ nếu vẫn trong tháng
            int result = 0;
            while (startDate.Month == value.Month)
            {
                result += 1;
                startDate = startDate.PlusDays(7);
            }

            return result;
        }

        /// <summary>
        /// Số lượng thứ trong tháng
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ trong tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ trong tháng</param>
        /// <param name="weekDay">Thứ cần đếm</param>
        /// <returns>Trả lại số lượng thứ trong tháng</returns>
        public static int CountDOWOfMonth(this string value, DayOfWeek weekDay, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountDOWOfMonth(weekDay);
        }

        /// <summary>
        /// Lấy số ngày chủ nhật trong tháng
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng chủ nhật trong tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng chủ nhật trong tháng</param>
        /// <returns>Trả lại số ngày chủ nhật trong tháng</returns>
        public static int CountSundayOfMonth(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountSundayOfMonth();
        }

        /// <summary>
        /// Lấy số ngày chủ nhật trong tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng chủ nhật trong tháng</param>
        /// <returns>Trả lại số ngày chủ nhật trong tháng</returns>
        public static int CountSundayOfMonth(this DateTime value)
        {
            return value.CountDOWOfMonth(DayOfWeek.Sunday);
        }

        /// <summary>
        /// Lấy số ngày thứ 7 trong tháng
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 7 trong tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 7 trong tháng</param>
        /// <returns>Trả lại số ngày thứ 7 trong tháng</returns>
        public static int CountSaturdayOfMonth(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountSaturdayOfMonth();
        }

        /// <summary>
        /// Lấy số ngày thứ 7 trong tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 7 trong tháng</param>
        /// <returns>Trả lại số ngày thứ 7 trong tháng</returns>
        public static int CountSaturdayOfMonth(this DateTime value)
        {
            return value.CountDOWOfMonth(DayOfWeek.Saturday);
        }

        /// <summary>
        /// Lấy số ngày thứ 6 trong tháng
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 6 trong tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 6 trong tháng</param>
        /// <returns>Trả lại số ngày thứ 6 trong tháng</returns>
        public static int CountFridayOfMonth(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountFridayOfMonth();
        }

        /// <summary>
        /// Lấy số ngày thứ 6 trong tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 6 trong tháng</param>
        /// <returns>Trả lại số ngày thứ 6 trong tháng</returns>
        public static int CountFridayOfMonth(this DateTime value)
        {
            return value.CountDOWOfMonth(DayOfWeek.Friday);
        }

        /// <summary>
        /// Lấy số ngày thứ 5 trong tháng
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 5 trong tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 5 trong tháng</param>
        /// <returns>Trả lại số ngày thứ 5 trong tháng</returns>
        public static int CountThursdayOfMonth(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountThursdayOfMonth();
        }

        /// <summary>
        /// Lấy số ngày thứ 5 trong tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 5 trong tháng</param>
        /// <returns>Trả lại số ngày thứ 5 trong tháng</returns>
        public static int CountThursdayOfMonth(this DateTime value)
        {
            return value.CountDOWOfMonth(DayOfWeek.Thursday);
        }

        /// <summary>
        /// Lấy số ngày thứ 4 trong tháng
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 4 trong tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 4 trong tháng</param>
        /// <returns>Trả lại số ngày thứ 4 trong tháng</returns>
        public static int CountWednesdayOfMonth(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountWednesdayOfMonth();
        }

        /// <summary>
        /// Lấy số ngày thứ 4 trong tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 4 trong tháng</param>
        /// <returns>Trả lại số ngày thứ 4 trong tháng</returns>
        public static int CountWednesdayOfMonth(this DateTime value)
        {
            return value.CountDOWOfMonth(DayOfWeek.Wednesday);
        }

        /// <summary>
        /// Lấy số ngày thứ 3 trong tháng
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 3 trong tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 3 trong tháng</param>
        /// <returns>Trả lại số ngày thứ 3 trong tháng</returns>
        public static int CountTuesdayOfMonth(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountTuesdayOfMonth();
        }

        /// <summary>
        /// Lấy số ngày thứ 3 trong tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 3 trong tháng</param>
        /// <returns>Trả lại số ngày thứ 3 trong tháng</returns>
        public static int CountTuesdayOfMonth(this DateTime value)
        {
            return value.CountDOWOfMonth(DayOfWeek.Tuesday);
        }

        /// <summary>
        /// Lấy số ngày thứ 2 trong tháng
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 2 trong tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 2 trong tháng</param>
        /// <returns>Trả lại số ngày thứ 2 trong tháng</returns>
        public static int CountMondayOfMonth(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountMondayOfMonth();
        }

        /// <summary>
        /// Lấy số ngày thứ 2 trong tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 2 trong tháng</param>
        /// <returns>Trả lại số ngày thứ 2 trong tháng</returns>
        public static int CountMondayOfMonth(this DateTime value)
        {
            return value.CountDOWOfMonth(DayOfWeek.Monday);
        }

        /// <summary>
        /// Lấy số ngày trong quý hiện tại của hệ điều hành
        /// </summary>
        /// <returns>Trả lại số ngày trong quý hiện tại của hệ điều hành</returns>
        public static int CountDayOfCurrentQuarter()
        {
            return GetCurrentDate().CountDayOfQuarter();
        }

        /// <summary>
        /// Lấy số ngày trong quý
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy số ngày trong quý</param>
        /// <returns>Trả lại số ngày trong quý</returns>
        public static int CountDayOfQuarter(this DateTime value)
        {
            return MinusDate(value.GetLastDateOfQuarter(), value.GetFirstDateOfQuarter()) + 1;
        }

        /// <summary>
        /// Lấy số ngày trong quý
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần lấy số ngày trong quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần lấy số ngày trong quý</param>
        /// <returns>Trả lại số ngày trong quý</returns>
        public static int CountDayOfQuarter(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountDayOfQuarter();
        }

        /// <summary>
        /// Số lượng thứ trong quý
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ trong quý</param>
        /// <param name="weekDay">Thứ cần đếm</param>
        /// <returns>Trả lại số lượng thứ trong quý</returns>
        public static int CountDOWOfQuarter(this DateTime value, DayOfWeek weekDay)
        {
            // Lấy ngày đầu quý
            DateTime startDate = value.GetFirstDateOfQuarter();
            // Tìm ngày đầu tiên trùng với thứ
            while (startDate.DayOfWeek != weekDay) startDate = startDate.PlusDays(1);
            // Cộng số lượng thứ nếu vẫn trong quý
            int result = 0;
            while (startDate.GetQuarter() == value.GetQuarter())
            {
                result += 1;
                startDate = startDate.PlusDays(7);
            }

            return result;
        }

        /// <summary>
        /// Số lượng thứ trong quý
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ trong quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ trong quý</param>
        /// <param name="weekDay">Thứ cần đếm</param>
        /// <returns>Trả lại số lượng thứ trong quý</returns>
        public static int CountDOWOfQuarter(this string value, DayOfWeek weekDay, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountDOWOfQuarter(weekDay);
        }

        /// <summary>
        /// Lấy số ngày chủ nhật trong quý
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng chủ nhật trong quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng chủ nhật trong quý</param>
        /// <returns>Trả lại số ngày chủ nhật trong quý</returns>
        public static int CountSundayOfQuarter(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountSundayOfQuarter();
        }

        /// <summary>
        /// Lấy số ngày chủ nhật trong quý
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng chủ nhật trong quý</param>
        /// <returns>Trả lại số ngày chủ nhật trong quý</returns>
        public static int CountSundayOfQuarter(this DateTime value)
        {
            return value.CountDOWOfQuarter(DayOfWeek.Sunday);
        }

        /// <summary>
        /// Lấy số ngày thứ 7 trong quý
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 7 trong quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 7 trong quý</param>
        /// <returns>Trả lại số ngày thứ 7 trong quý</returns>
        public static int CountSaturdayOfQuarter(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountSaturdayOfQuarter();
        }

        /// <summary>
        /// Lấy số ngày thứ 7 trong quý
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 7 trong quý</param>
        /// <returns>Trả lại số ngày thứ 7 trong quý</returns>
        public static int CountSaturdayOfQuarter(this DateTime value)
        {
            return value.CountDOWOfQuarter(DayOfWeek.Saturday);
        }

        /// <summary>
        /// Lấy số ngày thứ 6 trong quý
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 6 trong quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 6 trong quý</param>
        /// <returns>Trả lại số ngày thứ 6 trong quý</returns>
        public static int CountFridayOfQuarter(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountFridayOfQuarter();
        }

        /// <summary>
        /// Lấy số ngày thứ 6 trong quý
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 6 trong quý</param>
        /// <returns>Trả lại số ngày thứ 6 trong quý</returns>
        public static int CountFridayOfQuarter(this DateTime value)
        {
            return value.CountDOWOfQuarter(DayOfWeek.Friday);
        }

        /// <summary>
        /// Lấy số ngày thứ 5 trong quý
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 5 trong quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 5 trong quý</param>
        /// <returns>Trả lại số ngày thứ 5 trong quý</returns>
        public static int CountThursdayOfQuarter(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountThursdayOfQuarter();
        }

        /// <summary>
        /// Lấy số ngày thứ 5 trong quý
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 5 trong quý</param>
        /// <returns>Trả lại số ngày thứ 5 trong quý</returns>
        public static int CountThursdayOfQuarter(this DateTime value)
        {
            return value.CountDOWOfQuarter(DayOfWeek.Thursday);
        }

        /// <summary>
        /// Lấy số ngày thứ 4 trong quý
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 4 trong quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 4 trong quý</param>
        /// <returns>Trả lại số ngày thứ 4 trong quý</returns>
        public static int CountWednesdayOfQuarter(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountWednesdayOfQuarter();
        }

        /// <summary>
        /// Lấy số ngày thứ 4 trong quý
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 4 trong quý</param>
        /// <returns>Trả lại số ngày thứ 4 trong quý</returns>
        public static int CountWednesdayOfQuarter(this DateTime value)
        {
            return value.CountDOWOfQuarter(DayOfWeek.Wednesday);
        }

        /// <summary>
        /// Lấy số ngày thứ 3 trong quý
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 3 trong quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 3 trong quý</param>
        /// <returns>Trả lại số ngày thứ 3 trong quý</returns>
        public static int CountTuesdayOfQuarter(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountTuesdayOfQuarter();
        }

        /// <summary>
        /// Lấy số ngày thứ 3 trong quý
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 3 trong quý</param>
        /// <returns>Trả lại số ngày thứ 3 trong quý</returns>
        public static int CountTuesdayOfQuarter(this DateTime value)
        {
            return value.CountDOWOfQuarter(DayOfWeek.Tuesday);
        }

        /// <summary>
        /// Lấy số ngày thứ 2 trong quý
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 2 trong quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 2 trong quý</param>
        /// <returns>Trả lại số ngày thứ 2 trong quý</returns>
        public static int CountMondayOfQuarter(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountMondayOfQuarter();
        }

        /// <summary>
        /// Lấy số ngày thứ 2 trong quý
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 2 trong quý</param>
        /// <returns>Trả lại số ngày thứ 2 trong quý</returns>
        public static int CountMondayOfQuarter(this DateTime value)
        {
            return value.CountDOWOfQuarter(DayOfWeek.Monday);
        }

        /// <summary>
        /// Lấy số ngày trong năm hiện tại của hệ điều hành
        /// </summary>
        /// <returns>Trả lại số ngày trong năm hiện tại của hệ điều hành</returns>
        public static int CountDayOfCurrentYear()
        {
            return GetCurrentDate().CountDayOfYear();
        }

        /// <summary>
        /// Lấy số ngày trong năm
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy số ngày trong năm</param>
        /// <returns>Trả lại số ngày trong năm</returns>
        public static int CountDayOfYear(this DateTime value)
        {
            return MinusDate(value.GetLastDateOfYear(), value.GetFirstDateOfYear()) + 1;
        }

        /// <summary>
        /// Lấy số ngày trong năm
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần lấy số ngày trong năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần lấy số ngày trong năm</param>
        /// <returns>Trả lại số ngày trong năm</returns>
        public static int CountDayOfYear(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountDayOfYear();
        }

        /// <summary>
        /// Số lượng thứ trong năm
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ trong năm</param>
        /// <param name="weekDay">Thứ cần đếm</param>
        /// <returns>Trả lại số lượng thứ trong năm</returns>
        public static int CountDOWOfYear(this DateTime value, DayOfWeek weekDay)
        {
            // Lấy ngày đầu năm
            DateTime startDate = value.GetFirstDateOfYear();
            // Tìm ngày đầu tiên trùng với thứ
            while (startDate.DayOfWeek != weekDay) startDate = startDate.PlusDays(1);
            // Cộng số lượng thứ nếu vẫn trong năm
            int result = 0;
            while (startDate.Year == value.Year)
            {
                result += 1;
                startDate = startDate.PlusDays(7);
            }

            return result;
        }

        /// <summary>
        /// Số lượng thứ trong năm
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ trong năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ trong năm</param>
        /// <param name="weekDay">Thứ cần đếm</param>
        /// <returns>Trả lại số lượng thứ trong năm</returns>
        public static int CountDOWOfYear(this string value, DayOfWeek weekDay, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountDOWOfYear(weekDay);
        }

        /// <summary>
        /// Lấy số ngày chủ nhật trong năm
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng chủ nhật trong năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng chủ nhật trong năm</param>
        /// <returns>Trả lại số ngày chủ nhật trong năm</returns>
        public static int CountSundayOfYear(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountSundayOfYear();
        }

        /// <summary>
        /// Lấy số ngày chủ nhật trong năm
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng chủ nhật trong năm</param>
        /// <returns>Trả lại số ngày chủ nhật trong năm</returns>
        public static int CountSundayOfYear(this DateTime value)
        {
            return value.CountDOWOfYear(DayOfWeek.Sunday);
        }

        /// <summary>
        /// Lấy số ngày thứ 7 trong năm
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 7 trong năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 7 trong năm</param>
        /// <returns>Trả lại số ngày thứ 7 trong năm</returns>
        public static int CountSaturdayOfYear(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountSaturdayOfYear();
        }

        /// <summary>
        /// Lấy số ngày thứ 7 trong năm
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 7 trong năm</param>
        /// <returns>Trả lại số ngày thứ 7 trong năm</returns>
        public static int CountSaturdayOfYear(this DateTime value)
        {
            return value.CountDOWOfYear(DayOfWeek.Saturday);
        }

        /// <summary>
        /// Lấy số ngày thứ 6 trong năm
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 6 trong năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 6 trong năm</param>
        /// <returns>Trả lại số ngày thứ 6 trong năm</returns>
        public static int CountFridayOfYear(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountFridayOfYear();
        }

        /// <summary>
        /// Lấy số ngày thứ 6 trong năm
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 6 trong năm</param>
        /// <returns>Trả lại số ngày thứ 6 trong năm</returns>
        public static int CountFridayOfYear(this DateTime value)
        {
            return value.CountDOWOfYear(DayOfWeek.Friday);
        }

        /// <summary>
        /// Lấy số ngày thứ 5 trong năm
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 5 trong năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 5 trong năm</param>
        /// <returns>Trả lại số ngày thứ 5 trong năm</returns>
        public static int CountThursdayOfYear(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountThursdayOfYear();
        }

        /// <summary>
        /// Lấy số ngày thứ 5 trong năm
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 5 trong năm</param>
        /// <returns>Trả lại số ngày thứ 5 trong năm</returns>
        public static int CountThursdayOfYear(this DateTime value)
        {
            return value.CountDOWOfYear(DayOfWeek.Thursday);
        }

        /// <summary>
        /// Lấy số ngày thứ 4 trong năm
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 4 trong năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 4 trong năm</param>
        /// <returns>Trả lại số ngày thứ 4 trong năm</returns>
        public static int CountWednesdayOfYear(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountWednesdayOfYear();
        }

        /// <summary>
        /// Lấy số ngày thứ 4 trong năm
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 4 trong năm</param>
        /// <returns>Trả lại số ngày thứ 4 trong năm</returns>
        public static int CountWednesdayOfYear(this DateTime value)
        {
            return value.CountDOWOfYear(DayOfWeek.Wednesday);
        }

        /// <summary>
        /// Lấy số ngày thứ 3 trong năm
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 3 trong năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 3 trong năm</param>
        /// <returns>Trả lại số ngày thứ 3 trong năm</returns>
        public static int CountTuesdayOfYear(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountTuesdayOfYear();
        }

        /// <summary>
        /// Lấy số ngày thứ 3 trong năm
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 3 trong năm</param>
        /// <returns>Trả lại số ngày thứ 3 trong năm</returns>
        public static int CountTuesdayOfYear(this DateTime value)
        {
            return value.CountDOWOfYear(DayOfWeek.Tuesday);
        }

        /// <summary>
        /// Lấy số ngày thứ 2 trong năm
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần đếm số lượng thứ 2 trong năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần đếm số lượng thứ 2 trong năm</param>
        /// <returns>Trả lại số ngày thứ 2 trong năm</returns>
        public static int CountMondayOfYear(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountMondayOfYear();
        }

        /// <summary>
        /// Lấy số ngày thứ 2 trong năm
        /// </summary>
        /// <param name="value">Ngày tháng cần đếm số lượng thứ 2 trong năm</param>
        /// <returns>Trả lại số ngày thứ 2 trong năm</returns>
        public static int CountMondayOfYear(this DateTime value)
        {
            return value.CountDOWOfYear(DayOfWeek.Monday);
        }

        /// <summary>
        /// Lấy tuần hiện tại của hệ điều hành
        /// </summary>
        /// <returns>Trả lại tuần hiện tại theo hệ điều hành</returns>
        public static int GetCurrentWeek()
        {
            return GetCurrentDate().GetWeek();
        }

        /// <summary>
        /// Lấy tuần
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy tuần</param>
        /// <returns>Trả lại tuần của ngày tháng cần lấy</returns>
        public static int GetWeek(this DateTime value)
        {
            // Lấy ngày đầu năm
            DateTime startDate = value.GetFirstDateOfYear();
            // Tìm ngày thứ 2 đầu tiên
            while (startDate.DayOfWeek != DayOfWeek.Monday) startDate = startDate.PlusDays(1);

            decimal result = MinusDate(value, startDate);
            if (result >= 0) result = Math.Floor(result / 7) + 1;
            else result = 0; // value là trước thứ 2 đầu tiên -> tuần thứ nhất
            // Ngày đầu năm là thứ 2 (tính là tuần đầu tiên)
            if (startDate.Equals(value.GetFirstDateOfYear())) return (int)result;
            // Ngày đầu năm không phải là thứ 2 (tính là tuần thứ hai)
            return (int)result + 1;
        }

        /// <summary>
        /// Lấy tuần
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần lấy tuần</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng</param>
        /// <returns>Trả lại tuần của chuỗi chứa ngày tháng cần lấy</returns>
        public static int GetWeek(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).GetWeek();
        }

        /// <summary>
        /// Lấy số tuần giữa khoảng cách 2 ngày
        /// </summary>
        /// <param name="value">Ngày tháng thứ nhất</param>
        /// <param name="otherDate">Ngày tháng thứ hai</param>
        /// <returns>Trả lại số tuần giữa khoảng cách 2 ngày</returns>
        public static int CountWeekBetweenDates(this DateTime value, DateTime otherDate)
        {
            decimal result = (value - otherDate).Duration().Days;
            return (int)Math.Floor(result / 7);
        }

        /// <summary>
        /// Lấy số tuần giữa khoảng cách 2 ngày
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng thứ nhất</param>
        /// <param name="otherDate">Chuỗi chứa ngày tháng thứ hai</param>
        /// /// <param name="format">Format (dạng ngày tháng) của các chuỗi chứa ngày tháng</param>
        /// <returns>Trả lại số tuần giữa khoảng cách 2 ngày</returns>
        public static int CountWeekBetweenDates(this string value, string otherDate, string format = "dd/MM/yyyy")
        {
            return CountWeekBetweenDates(value.StringToDate(format), otherDate.StringToDate(format));
        }

        /// <summary>
        /// Lấy số tuần trong tháng hiện tại của hệ điều hành
        /// </summary>
        /// <returns>Trả lại số tuần trong tháng hiện tại của hệ điều hành</returns>
        public static int CountWeekInCurrentMonth()
        {
            return GetCurrentDate().CountWeekInMonth();
        }

        /// <summary>
        /// Lấy số tuần trong tháng
        /// </summary>
        /// <param name="value">Ngày tháng của tháng cần lấy số tuần</param>
        /// <returns>Trả lại số tuần trong tháng</returns>
        public static int CountWeekInMonth(this DateTime value)
        {
            // Lấy ngày đầu, cuối tháng
            DateTime startDate = value.GetFirstDateOfMonth();
            DateTime endDate = value.GetLastDateOfMonth();
            // Tìm ngày thứ 2 đầu tiên
            while (startDate.DayOfWeek != DayOfWeek.Monday) startDate = startDate.PlusDays(1);

            decimal result = MinusDate(endDate, startDate);
            result = Math.Floor(result / 7) + 1;
            // Ngày đầu tháng là thứ 2 (tính là tuần đầu tiên)
            if (startDate.Equals(value.GetFirstDateOfMonth())) return (int)result;
            // Ngày đầu tháng không phải là thứ 2 (tính là tuần thứ hai)
            return (int)result + 1;
        }

        /// <summary>
        /// Lấy số tuần trong tháng
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng của tháng cần lấy số tuần</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng của tháng cần lấy số tuần</param>
        /// <returns>Trả lại số tuần trong tháng</returns>
        public static int CountWeekInMonth(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountWeekInMonth();
        }

        /// <summary>
        /// Lấy số tuần trong năm hiện tại của hệ điều hành
        /// </summary>
        /// <returns>Trả lại số tuần trong năm hiện tại của hệ điều hành</returns>
        public static int CountWeekInCurrentYear()
        {
            return GetCurrentDate().CountWeekInYear();
        }

        /// <summary>
        /// Lấy số tuần trong năm
        /// </summary>
        /// <param name="value">Ngày tháng của năm cần lấy số tuần</param>
        /// <returns>Trả lại số tuần trong năm</returns>
        public static int CountWeekInYear(this DateTime value)
        {
            return GetCurrentDate().GetLastDateOfYear().GetWeek();
        }

        /// <summary>
        /// Lấy số tuần trong năm
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng của năm cần lấy số tuần</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng của năm cần lấy số tuần</param>
        /// <returns>Trả lại số tuần trong năm</returns>
        public static int CountWeekInYear(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).CountWeekInYear();
        }

        /// <summary>
        /// Lấy ngày cuối tuần
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày cuối tuần</param>
        /// <returns>Trả lại ngày đầu tuần</returns>
        public static DateTime GetLastDateOfWeek(this DateTime value)
        {
            return GetFirstDateOfWeek(value).PlusDays(6);
        }

        /// <summary>
        /// Lấy ngày cuối tuần
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần lấy ngày cuối tuần</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần lấy ngày cuối tuần</param>
        /// <returns>Trả lại chuỗi chứa ngày cuối tuần theo format</returns>
        public static string GetLastDateOfWeek(this DateTime value, string format)
        {
            return GetLastDateOfWeek(value).DateToString(format);
        }

        /// <summary>
        /// Lấy ngày đầu tuần
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày đầu tuần</param>
        /// <returns>Trả lại ngày đầu tuần</returns>
        public static DateTime GetFirstDateOfWeek(this DateTime value)
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");

            DateTime result = value;
            // Tìm ngày thứ 2 đầu tiên
            while (result.DayOfWeek != DayOfWeek.Monday) result = result.PlusDays(-1);
            return result;
        }

        /// <summary>
        /// Lấy ngày đầu tuần
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần lấy ngày đầu tuần</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần lấy ngày đầu tuần</param>
        /// <returns>Trả lại chuỗi chứa ngày đầu tuần theo format</returns>
        public static string GetFirstDateOfWeek(this DateTime value, string format)
        {
            return GetFirstDateOfWeek(value).DateToString(format);
        }

        /// <summary>
        /// Lấy thứ trong tuần
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy thứ trong tuần</param>
        /// <returns>Trả lại thứ trong tuần</returns>
        public static DayOfWeek GetDayOfWeek(this DateTime value)
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");

            return value.DayOfWeek;
        }

        /// <summary>
        /// Lấy thứ trong tuần
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần lấy thứ trong tuần</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng cần lấy thứ trong tuần</param>
        /// <returns>Trả lại thứ trong tuần</returns>
        public static DayOfWeek GetDayOfWeek(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).GetDayOfWeek();
        }

        /// <summary>
        /// Lấy tháng hiện tại theo hệ điều hành
        /// </summary>
        /// <returns>Trả lại tháng hiện tại theo hệ điều hành</returns>
        public static int GetCurrentMonth()
        {
            return GetCurrentDate().Month;
        }

        /// <summary>
        /// Lấy tháng
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần lấy tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng</param>
        /// <returns>Trả lại tháng của chuỗi chứa ngày tháng cần lấy</returns>
        public static int GetMonth(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).GetMonth();
        }

        /// <summary>
        /// Lấy tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy tháng</param>
        /// <returns>Trả lại tháng của ngày tháng cần lấy</returns>
        public static int GetMonth(this DateTime value)
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");

            return value.Month;
        }

        /// <summary>
        /// Lấy ngày cuối tháng
        /// </summary>
        /// <param name="year">Năm cần lấy ngày cuối tháng</param>
        /// <param name="month">Tháng cần lấy ngày cuối tháng</param>
        /// <returns>Trả lại ngày cuối tháng</returns>
        public static DateTime GetLastDateOfMonth(int year, int month)
        {
            return GetFirstDateOfMonth(year, month).AddMonths(1).PlusDays(-1);
        }

        /// <summary>
        /// Lấy ngày cuối tháng
        /// </summary>
        /// <param name="year">Năm cần lấy ngày cuối tháng</param>
        /// <param name="month">Tháng cần lấy ngày cuối tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi ngày cuối tháng</param>
        /// <returns>Trả lại chuỗi chứa ngày cuối tháng theo format</returns>
        public static string GetLastDateOfMonth(int year, int month, string format)
        {
            return GetLastDateOfMonth(year, month).DateToString(format);
        }

        /// <summary>
        /// Lấy ngày cuối tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày cuối tháng</param>
        /// <returns>Trả lại ngày cuối tháng</returns>
        public static DateTime GetLastDateOfMonth(this DateTime value)
        {
            return GetLastDateOfMonth(value.Year, value.Month);
        }

        /// <summary>
        /// Lấy ngày cuối tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày cuối tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi ngày cuối tháng</param>
        /// <returns>Trả lại chuỗi chứa ngày cuối tháng theo format</returns>
        public static string GetLastDateOfMonth(this DateTime value, string format)
        {
            return GetLastDateOfMonth(value).DateToString(format);
        }

        /// <summary>
        /// Lấy ngày đầu tháng
        /// </summary>
        /// <param name="year">Năm cần lấy ngày đầu tháng</param>
        /// <param name="month">Tháng cần lấy ngày đầu tháng</param>
        /// <returns>Trả lại ngày đầu tháng</returns>
        public static DateTime GetFirstDateOfMonth(int year, int month)
        {
            if (year < 1753 || year > 9999) throw new ArgumentException("Giá trị năm phải nằm trong khoảng từ 1753 đến 9999.", "year = " + year.ToString() + "");
            if (month < 1 || month > 12) throw new ArgumentException("Giá trị tháng phải nằm trong khoảng từ 1 đến 12.", "month = " + month.ToString() + "");

            return new DateTime(year, month, 1);
        }

        /// <summary>
        /// Lấy ngày đầu tháng
        /// </summary>
        /// <param name="year">Năm cần lấy ngày đầu tháng</param>
        /// <param name="month">Tháng cần lấy ngày đầu tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi ngày đầu tháng</param>
        /// <returns>Trả lại chuỗi chứa ngày đầu tháng theo format</returns>
        public static string GetFirstDateOfMonth(int year, int month, string format)
        {
            return GetFirstDateOfMonth(year, month).DateToString(format);
        }

        /// <summary>
        /// Lấy ngày đầu tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày đầu tháng</param>
        /// <returns>Trả lại ngày đầu tháng</returns>
        public static DateTime GetFirstDateOfMonth(this DateTime value)
        {
            return GetFirstDateOfMonth(value.Year, value.Month);
        }

        /// <summary>
        /// Lấy ngày đầu tháng
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày đầu tháng</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi ngày đầu tháng</param>
        /// <returns>Trả lại chuỗi chứa ngày đầu tháng theo format</returns>
        public static string GetFirstDateOfMonth(this DateTime value, string format)
        {
            return GetFirstDateOfMonth(value).DateToString(format);
        }

        /// <summary>
        /// Lấy số tháng giữa khoảng cách 2 ngày
        /// </summary>
        /// <param name="value">Ngày tháng thứ nhất</param>
        /// <param name="otherDate">Ngày tháng thứ hai</param>
        /// <returns>Trả lại số tháng giữa khoảng cách 2 ngày</returns>
        public static int CountMonthBetweenDates(this DateTime value, DateTime otherDate)
        {
            DateTime d1;
            DateTime d2;
            int days = MinusDate(value, otherDate);
            // Cùng ngày
            if (days == 0) return 0;
            if (days < 0)
            {   // value < otherDate
                d1 = value;
                d2 = otherDate;
            }
            else
            {   // otherDate < value
                d1 = otherDate;
                d2 = value;
            }

            // Tính số tháng chênh lệch
            int result = (d2.Year - d1.Year) * 12 + (d2.Month - d1.Month);
            // Chưa đủ tháng
            if (d2.Day < d1.Day) return result - 1;
            // Đủ tháng
            return result;
        }

        /// <summary>
        /// Lấy số tháng giữa khoảng cách 2 ngày
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng thứ nhất</param>
        /// <param name="otherDate">Chuỗi chứa ngày tháng thứ hai</param>
        /// <param name="format">Format (dạng ngày tháng) của các chuỗi chứa ngày tháng</param>
        /// <returns>Trả lại số tháng giữa khoảng cách 2 ngày</returns>
        public static int CountMonthBetweenDates(this string value, string otherDate, string format = "dd/MM/yyyy")
        {
            return CountMonthBetweenDates(value.StringToDate(format), otherDate.StringToDate(format));
        }

        /// <summary>
        /// Lấy quý hiện tại theo hệ điều hành
        /// </summary>
        /// <returns>Trả lại quý hiện tại theo hệ điều hành</returns>
        public static int GetCurrentQuarter()
        {
            return GetCurrentDate().GetQuarter();
        }

        /// <summary>
        /// Lấy quý
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần lấy quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng</param>
        /// <returns>Trả lại quý của chuỗi chứa ngày tháng cần lấy</returns>
        public static int GetQuarter(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).GetQuarter();
        }

        /// <summary>
        /// Lấy quý
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy quý</param>
        /// <returns>Trả lại quý của ngày tháng cần lấy</returns>
        public static int GetQuarter(this DateTime value)
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");
            decimal d = (value.Month - 1);
            d = d / 3;
            return (int)Math.Floor(d) + 1;
        }

        /// <summary>
        /// Lấy ngày cuối quý
        /// </summary>
        /// <param name="year">Năm cần lấy ngày cuối quý</param>
        /// <param name="quarter">Quý cần lấy ngày cuối quý</param>
        /// <returns>Trả lại ngày cuối quý</returns>
        public static DateTime GetLastDateOfQuarter(int year, int quarter)
        {
            return GetFirstDateOfQuarter(year, quarter).PlusDays(-1).AddMonths(3);
        }

        /// <summary>
        /// Lấy ngày cuối quý
        /// </summary>
        /// <param name="year">Năm cần lấy ngày cuối quý</param>
        /// <param name="quarter">Quý cần lấy ngày cuối quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi ngày cuối quý</param>
        /// <returns>Trả lại chuỗi chứa ngày cuối quý theo format</returns>
        public static string GetLastDateOfQuarter(int year, int quarter, string format)
        {
            return GetLastDateOfQuarter(year, quarter).DateToString(format);
        }

        /// <summary>
        /// Lấy ngày cuối quý
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày cuối quý</param>
        /// <returns>Trả lại ngày cuối quý</returns>
        public static DateTime GetLastDateOfQuarter(this DateTime value)
        {
            return GetLastDateOfQuarter(value.Year, value.GetQuarter());
        }

        /// <summary>
        /// Lấy ngày cuối quý
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày cuối quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi ngày cuối quý</param>
        /// <returns>Trả lại chuỗi chứa ngày cuối quý theo format</returns>
        public static string GetLastDateOfQuarter(this DateTime value, string format)
        {
            return GetLastDateOfQuarter(value).DateToString(format);
        }

        /// <summary>
        /// Lấy ngày đầu quý
        /// </summary>
        /// <param name="year">Năm cần lấy ngày đầu quý</param>
        /// <param name="month">Quý cần lấy ngày đầu quý</param>
        /// <returns>Trả lại ngày đầu quý</returns>
        public static DateTime GetFirstDateOfQuarter(int year, int quarter)
        {
            if (year < 1753 || year > 9999) throw new ArgumentException("Giá trị năm phải nằm trong khoảng từ 1753 đến 9999.", "year = " + year.ToString() + "");
            if (quarter < 1 || quarter > 4) throw new ArgumentException("Giá trị quý phải nằm trong khoảng từ 1 đến 4.", "quarter = " + quarter.ToString() + "");

            return new DateTime(year, (quarter * 3) - 2, 1);
        }

        /// <summary>
        /// Lấy ngày đầu quý
        /// </summary>
        /// <param name="year">Năm cần lấy ngày đầu quý</param>
        /// <param name="month">Quý cần lấy ngày đầu quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi ngày đầu quý</param>
        /// <returns>Trả lại chuỗi chứa ngày đầu quý theo format</returns>
        public static string GetFirstDateOfQuarter(int year, int quarter, string format)
        {
            return GetFirstDateOfQuarter(year, quarter).DateToString(format);
        }

        /// <summary>
        /// Lấy ngày đầu quý
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày đầu quý</param>
        /// <returns>Trả lại ngày đầu quý</returns>
        public static DateTime GetFirstDateOfQuarter(this DateTime value)
        {
            return GetFirstDateOfQuarter(value.Year, value.GetQuarter());
        }

        /// <summary>
        /// Lấy ngày đầu quý
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày đầu quý</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi ngày đầu quý</param>
        /// <returns>Trả lại chuỗi chứa ngày đầu quý theo format</returns>
        public static string GetFirstDateOfQuarter(this DateTime value, string format)
        {
            return GetFirstDateOfQuarter(value).DateToString(format);
        }

        /// <summary>
        /// Lấy số quý giữa khoảng cách 2 ngày
        /// </summary>
        /// <param name="value">Ngày tháng thứ nhất</param>
        /// <param name="otherDate">Ngày tháng thứ hai</param>
        /// <returns>Trả lại số quý giữa khoảng cách 2 ngày</returns>
        public static int CountQuarterBetweenDates(this DateTime value, DateTime otherDate)
        {
            decimal result = CountMonthBetweenDates(value, otherDate);
            result = Math.Floor(result / 3);
            return (int)result;
        }

        /// <summary>
        /// Lấy số quý giữa khoảng cách 2 ngày
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng thứ nhất</param>
        /// <param name="otherDate">Chuỗi chứa ngày tháng thứ hai</param>
        /// <param name="format">Format (dạng ngày tháng) của các chuỗi chứa ngày tháng</param>
        /// <returns>Trả lại số quý giữa khoảng cách 2 ngày</returns>
        public static int CountQuarterBetweenDates(this string value, string otherDate, string format = "dd/MM/yyyy")
        {
            return CountQuarterBetweenDates(value.StringToDate(format), otherDate.StringToDate(format));
        }

        /// <summary>
        /// Lấy năm hiện tại theo hệ điều hành
        /// </summary>
        /// <returns>Trả lại năm hiện tại theo hệ điều hành</returns>
        public static int GetCurrentYear()
        {
            return GetCurrentDate().Year;
        }

        /// <summary>
        /// Lấy năm
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng cần lấy năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi chứa ngày tháng</param>
        /// <returns>Trả lại năm của chuỗi chứa ngày tháng cần lấy</returns>
        public static int GetYear(this string value, string format = "dd/MM/yyyy")
        {
            return value.StringToDate(format).GetYear();
        }

        /// <summary>
        /// Lấy năm
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy năm</param>
        /// <returns>Trả lại năm của ngày tháng cần lấy</returns>
        public static int GetYear(this DateTime value)
        {
            // Kiểm tra ngày tháng có thỏa mãn chuẩn MSSQL
            if (!IsDate(value))
                throw new ArgumentException("Ngày tháng nhỏ hơn giá trị cho phép là '" + DateTime.MinValue.AddYears(1752).ToString("dd/MM/yyyy") + "'.", "value = '" + value.ToString("dd/MM/yyyy") + "'");

            return value.Year;
        }

        /// <summary>
        /// Lấy ngày cuối năm
        /// </summary>
        /// <param name="year">Năm cần lấy ngày cuối năm</param>
        /// <returns>Trả lại ngày cuối năm</returns>
        public static DateTime GetLastDateOfYear(int year)
        {
            return GetFirstDateOfYear(year).PlusDays(-1).AddMonths(12);
        }

        /// <summary>
        /// Lấy ngày cuối năm
        /// </summary>
        /// <param name="year">Năm cần lấy ngày cuối năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi ngày cuối năm</param>
        /// <returns>Trả lại chuỗi chứa ngày cuối năm theo format</returns>
        public static string GetLastDateOfYear(int year, string format)
        {
            return GetLastDateOfYear(year).DateToString(format);
        }

        /// <summary>
        /// Lấy ngày cuối năm
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày cuối năm</param>
        /// <returns>Trả lại ngày cuối năm</returns>
        public static DateTime GetLastDateOfYear(this DateTime value)
        {
            return GetLastDateOfYear(value.Year);
        }

        /// <summary>
        /// Lấy ngày cuối năm
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày cuối năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi ngày cuối năm</param>
        /// <returns>Trả lại chuỗi chứa ngày cuối năm theo format</returns>
        public static string GetLastDateOfYear(this DateTime value, string format)
        {
            return GetLastDateOfYear(value).DateToString(format);
        }

        /// <summary>
        /// Lấy ngày đầu năm
        /// </summary>
        /// <param name="year">Năm cần lấy ngày đầu năm</param>
        /// <returns>Trả lại ngày đầu năm</returns>
        public static DateTime GetFirstDateOfYear(int year)
        {
            if (year < 1753 || year > 9999) throw new ArgumentException("Giá trị năm phải nằm trong khoảng từ 1753 đến 9999.", "year = " + year.ToString() + "");

            return new DateTime(year, 1, 1);
        }

        /// <summary>
        /// Lấy ngày đầu năm
        /// </summary>
        /// <param name="year">Năm cần lấy ngày đầu năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi ngày đầu năm</param>
        /// <returns>Trả lại chuỗi chứa ngày đầu năm theo format</returns>
        public static string GetFirstDateOfYear(int year, string format)
        {
            return GetFirstDateOfYear(year).DateToString(format);
        }

        /// <summary>
        /// Lấy ngày đầu năm
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày đầu năm</param>
        /// <returns>Trả lại ngày đầu năm</returns>
        public static DateTime GetFirstDateOfYear(this DateTime value)
        {
            return GetFirstDateOfYear(value.Year);
        }

        /// <summary>
        /// Lấy ngày đầu năm
        /// </summary>
        /// <param name="value">Ngày tháng cần lấy ngày đầu năm</param>
        /// <param name="format">Format (dạng ngày tháng) của chuỗi ngày đầu năm</param>
        /// <returns>Trả lại chuỗi chứa ngày đầu năm theo format</returns>
        public static string GetFirstDateOfYear(this DateTime value, string format)
        {
            return GetFirstDateOfYear(value).DateToString(format);
        }

        /// <summary>
        /// Lấy số năm giữa khoảng cách 2 ngày
        /// </summary>
        /// <param name="value">Ngày tháng thứ nhất</param>
        /// <param name="otherDate">Ngày tháng thứ hai</param>
        /// <returns>Trả lại số năm giữa khoảng cách 2 ngày</returns>
        public static int CountYearBetweenDates(this DateTime value, DateTime otherDate)
        {
            decimal result = CountMonthBetweenDates(value, otherDate);
            result = Math.Floor(result / 12);
            return (int)result;
        }

        /// <summary>
        /// Lấy số năm giữa khoảng cách 2 ngày
        /// </summary>
        /// <param name="value">Chuỗi chứa ngày tháng thứ nhất</param>
        /// <param name="otherDate">Chuỗi chứa ngày tháng thứ hai</param>
        /// <param name="format">Format (dạng ngày tháng) của các chuỗi chứa ngày tháng</param>
        /// <returns>Trả lại số năm giữa khoảng cách 2 ngày</returns>
        public static int CountYearBetweenDates(this string value, string otherDate, string format = "dd/MM/yyyy")
        {
            return CountYearBetweenDates(value.StringToDate(format), otherDate.StringToDate(format));
        }

        /// <summary>
        /// Chuyển đổi DateTime sang ngày tháng dạng Julian Date
        /// </summary>
        /// <param name="value">Ngày tháng cần chuyển đổi</param>
        /// <returns>Trả lại ngày tháng dạng Julian Date</returns>
        private static long Date2Julian(this DateTime value)
        {
            long a = LNumber.FloorDouble2Long((14 - value.GetMonth()) / 12);
            long y = value.GetYear() + 4800 - a;
            long m = value.GetMonth() + 12 * a - 3;
            long result = value.GetDay() + LNumber.FloorDouble2Long((153 * m + 2) / 5) + 365 * y + LNumber.FloorDouble2Long(y / 4) - LNumber.FloorDouble2Long(y / 100) + LNumber.FloorDouble2Long(y / 400) - 32045;
            if (result < 2299161)
            {
                result = value.GetDay() + LNumber.FloorDouble2Long((153 * m + 2) / 5) + 365 * y + LNumber.FloorDouble2Long(y / 4) - 32083;
            }
            return result;
        }

        /// <summary>
        /// Chuyển đổi ngày tháng dạng Julian Date sang DateTime
        /// </summary>
        /// <param name="value">Ngày tháng dạng Julian Date cần chuyển đổi</param>
        /// <returns>Trả lại ngày tháng dạng DateTime</returns>
        private static DateTime Julian2Date(this long value)
        {
            long a, b, c, d, e, m;
            if (value > 2299160)
            { // After 5/10/1582, Gregorian calendar
                a = value + 32044;
                b = LNumber.FloorDouble2Long((4 * a + 3) / 146097);
                c = a - LNumber.FloorDouble2Long((b * 146097) / 4);
            }
            else
            {
                b = 0;
                c = value + 32082;
            }
            d = LNumber.FloorDouble2Long((4 * c + 3) / 1461);
            e = c - LNumber.FloorDouble2Long((1461 * d) / 4);
            m = LNumber.FloorDouble2Long((5 * e + 2) / 153);
            long day = e - LNumber.FloorDouble2Long((153 * m + 2) / 5) + 1;
            long month = m + 3 - 12 * LNumber.FloorDouble2Long(m / 10);
            long year = b * 100 + d - 4800 + LNumber.FloorDouble2Long(m / 10);
            return new DateTime((int)year, (int)month, (int)day);
        }

        /// <summary>
        /// Tính Julian Date của ngày Sóc
        /// </summary>
        /// <param name="value">Thứ tự ngày Sóc</param>
        /// <param name="timeZone">Vùng thời gian</param>
        /// <returns>Trả lại ngày tháng dạng Julian Date của ngày Sóc</returns>
        private static long GetNewMoonDay(this long value, long timeZone = 7)
        {
            double T = value / 1236.85; // Time in Julian centuries from 1900 January 0.5
            double T2 = T * T;
            double T3 = T2 * T;
            double dr = Math.PI / 180;
            double Jd1 = 2415020.75933 + 29.53058868 * value + 0.0001178 * T2 - 0.000000155 * T3;
            Jd1 = Jd1 + 0.00033 * Math.Sin((166.56 + 132.87 * T - 0.009173 * T2) * dr); // Mean new moon
            double M = 359.2242 + 29.10535608 * value - 0.0000333 * T2 - 0.00000347 * T3; // Sun's mean anomaly
            double Mpr = 306.0253 + 385.81691806 * value + 0.0107306 * T2 + 0.00001236 * T3; // Moon's mean anomaly
            double F = 21.2964 + 390.67050646 * value - 0.0016528 * T2 - 0.00000239 * T3; // Moon's argument of latitude
            double C1 = (0.1734 - 0.000393 * T) * Math.Sin(M * dr) + 0.0021 * Math.Sin(2 * dr * M);
            C1 = C1 - 0.4068 * Math.Sin(Mpr * dr) + 0.0161 * Math.Sin(dr * 2 * Mpr);
            C1 = C1 - 0.0004 * Math.Sin(dr * 3 * Mpr);
            C1 = C1 + 0.0104 * Math.Sin(dr * 2 * F) - 0.0051 * Math.Sin(dr * (M + Mpr));
            C1 = C1 - 0.0074 * Math.Sin(dr * (M - Mpr)) + 0.0004 * Math.Sin(dr * (2 * F + M));
            C1 = C1 - 0.0004 * Math.Sin(dr * (2 * F - M)) - 0.0006 * Math.Sin(dr * (2 * F + Mpr));
            C1 = C1 + 0.0010 * Math.Sin(dr * (2 * F - Mpr)) + 0.0005 * Math.Sin(dr * (2 * Mpr + M));
            double deltat = 0;
            if (T < -11)
            {
                deltat = 0.001 + 0.000839 * T + 0.0002261 * T2 - 0.00000845 * T3 - 0.000000081 * T * T3;
            }
            else
            {
                deltat = -0.000278 + 0.000265 * T + 0.000262 * T2;
            };
            double JdNew = Jd1 + C1 - deltat;
            return LNumber.FloorDouble2Long(JdNew + 0.5 + (double)((double)timeZone / 24));
        }

        /// <summary>
        /// Tính kinh độ mặt trời
        /// </summary>
        /// <param name="julianDate">Ngày tháng dạng Julian Date</param>
        /// <param name="timeZone">Vùng thời gian</param>
        /// <returns>Trả lại kinh độ mặt trời của ngày Julian Date</returns>
        private static long GetSunLongitude(this long julianDate, int timeZone = 7)
        {
            double T = (julianDate - 2451545.5 - timeZone / 24) / 36525; // Time in Julian centuries from 2000-01-01 12:00:00 GMT
            double T2 = T * T;
            double dr = Math.PI / 180; // degree to radian
            double M = 357.52910 + 35999.05030 * T - 0.0001559 * T2 - 0.00000048 * T * T2; // mean anomaly, degree
            double L0 = 280.46645 + 36000.76983 * T + 0.0003032 * T2; // mean longitude, degree
            double DL = (1.914600 - 0.004817 * T - 0.000014 * T2) * Math.Sin(dr * M);
            DL = DL + (0.019993 - 0.000101 * T) * Math.Sin(dr * 2 * M) + 0.000290 * Math.Sin(dr * 3 * M);
            double L = L0 + DL; // true longitude, degree
            // obtain apparent longitude by correcting for nutation and aberration
            double omega = 125.04 - 1934.136 * T;
            L = L - 0.00569 - 0.00478 * Math.Sin(omega * dr);
            L = L * dr;
            L = L - Math.PI * 2 * (LNumber.FloorDouble2Long(L / (Math.PI * 2))); // Normalize to (0, 2*PI)
            return LNumber.FloorDouble2Long(L / Math.PI * 6);
        }

        /// <summary>
        /// Tính ngày bắt đầu tháng 11 âm lịch theo năm
        /// </summary>
        /// <param name="year">Năm cần tính</param>
        /// <param name="timeZone">Vùng thời gian</param>
        /// <returns>Trả lại ngày tháng dạng Julian Date của ngày bắt đầu tháng 11 âm lịch</returns>
        private static long GetLunarMonth11(this int year, int timeZone = 7)
        {
            long off = (new DateTime(year, 12, 31)).Date2Julian() - 2415021;
            long k = LNumber.FloorDouble2Long(off / 29.530588853);
            long result = k.GetNewMoonDay(timeZone);
            long sunLong = result.GetSunLongitude(timeZone); // sun longitude at local midnight
            if (sunLong >= 9)
            {
                result = (k - 1).GetNewMoonDay(timeZone);
            }
            return result;
        }

        /// <summary>
        /// Xác định tháng nhuận
        /// </summary>
        /// <param name="value">Ngày tháng dạng Julian Date của ngày bắt đầu tháng 11 âm lịch</param>
        /// <param name="timeZone">Vùng thời gian</param>
        /// <returns>Trả lại tháng nhuận sau tháng 11</returns>
        private static int GetLeapMonthOffset(this long value, int timeZone = 7)
        {
            long k = LNumber.FloorDouble2Long((value - 2415021.076998695) / 29.530588853 + 0.5);
            long last = 0;
            int i = 1; // We start with the month following lunar month 11
            long arc = (k + i).GetNewMoonDay(timeZone).GetSunLongitude(timeZone);
            do
            {
                last = arc;
                i = i + 1;
                arc = (k + i).GetNewMoonDay(timeZone).GetSunLongitude(timeZone);
            } while (arc != last && i < 14);
            return i - 1;
        }

        /// <summary>
        /// Chuyển đổi ngày dương sang ngày âm
        /// </summary>
        /// <param name="value">Ngày tháng cần chuyển đổi</param>
        /// <param name="timeZone">Vùng thời gian</param>
        /// <returns>Trả lại mảng chứa thông tin ngày âm</returns>
        private static int[] ConvertSolar2Lunar(this DateTime value, int timeZone = 7)
        {
            long dayNumber = value.Date2Julian();
            long k = LNumber.FloorDouble2Long((dayNumber - 2415021.076998695) / 29.530588853);
            long monthStart = (k + 1).GetNewMoonDay(timeZone);
            if (monthStart > dayNumber)
            {
                monthStart = k.GetNewMoonDay(timeZone);
            }
            long a11 = value.GetYear().GetLunarMonth11(timeZone);
            long b11 = a11;
            int lunarYear;
            if (a11 >= monthStart)
            {
                lunarYear = value.GetYear();
                a11 = (value.GetYear() - 1).GetLunarMonth11(timeZone);
            }
            else
            {
                lunarYear = value.GetYear() + 1;
                b11 = (value.GetYear() + 1).GetLunarMonth11(timeZone);
            }
            long lunarDay = dayNumber - monthStart + 1;
            long diff = LNumber.FloorDouble2Long((monthStart - a11) / 29);
            int lunarLeap = 0;
            long lunarMonth = diff + 11;
            if (b11 - a11 > 365)
            {
                int leapMonthDiff = a11.GetLeapMonthOffset(timeZone);
                if (diff >= leapMonthDiff)
                {
                    lunarMonth = diff + 10;
                    if (diff == leapMonthDiff)
                    {
                        lunarLeap = 1;
                    }
                }
            }
            if (lunarMonth > 12)
            {
                lunarMonth = lunarMonth - 12;
            }
            if (lunarMonth >= 11 && diff < 4)
            {
                lunarYear -= 1;
            }
            return new int[] { (int)lunarYear, (int)lunarMonth, (int)lunarDay, lunarLeap };
        }

        /// <summary>
        /// Chuyển đổi ngày âm sang ngày dương
        /// </summary>
        /// <param name="lunarDay">Ngày âm lịch</param>
        /// <param name="lunarMonth">Tháng âm lịch</param>
        /// <param name="lunarYear">Năm âm lịch</param>
        /// <param name="lunarLeap">Tháng nhuận</param>
        /// <param name="timeZone">Vùng thời gian</param>
        /// <returns>Trả lại ngày dương lịch</returns>
        private static DateTime ConvertLunar2Solar(int lunarYear, int lunarMonth, int lunarDay, int lunarLeap, int timeZone = 7)
        {
            long a11, b11, leapOff, leapMonth;
            if (lunarMonth < 11)
            {
                a11 = (lunarYear - 1).GetLunarMonth11(timeZone);
                b11 = lunarYear.GetLunarMonth11(timeZone);
            }
            else
            {
                a11 = lunarYear.GetLunarMonth11(timeZone);
                b11 = (lunarYear + 1).GetLunarMonth11(timeZone);
            }
            long k = LNumber.FloorDouble2Long(0.5 + (a11 - 2415021.076998695) / 29.530588853);
            int off = lunarMonth - 11;
            if (off < 0)
            {
                off += 12;
            }
            if (b11 - a11 > 365)
            {
                leapOff = a11.GetLeapMonthOffset(timeZone);
                leapMonth = leapOff - 2;
                if (leapMonth < 0)
                {
                    leapMonth += 12;
                }
                if (lunarLeap != 0 && lunarMonth != leapMonth)
                {
                    return DateTime.MinValue;
                }
                else if (lunarLeap != 0 || off >= leapOff)
                {
                    off += 1;
                }
            }
            long monthStart = (k + off).GetNewMoonDay(timeZone);
            return (monthStart + lunarDay - 1).Julian2Date();
        }

        /// <summary>
        /// Chuyển đổi ngày dương lịch sang âm lịch
        /// </summary>
        /// <param name="value">Ngày tháng cần chuyển đổi</param>
        /// <returns>Trả lại mảng chứa thông tin ngày âm lịch</returns>
        public static int[] Solar2Lunar(this DateTime value)
        {
            return value.ConvertSolar2Lunar();
        }

        /// <summary>
        /// Chuyển đổi ngày dương lịch sang âm lịch
        /// </summary>
        /// <param name="value">Ngày tháng cần chuyển đổi</param>
        /// <returns>Trả lại ngày tháng dương lịch</returns>
        public static DateTime Lunar2Solar(this DateTime value)
        {
            DateTime result;
            // Trừ tháng Chạp thì các tháng khác tính theo tháng chính, không tính theo tháng nhuận
            if (value.GetMonth() == 12)
            {   // Tính theo tháng nhuận trước
                result = ConvertLunar2Solar(value.GetYear(), value.GetMonth(), value.GetDay(), 1);
                // Nếu không có tháng nhuận
                if (result == DateTime.MinValue) result = ConvertLunar2Solar(value.GetYear(), value.GetMonth(), value.GetDay(), 0);
            }
            else result = ConvertLunar2Solar(value.GetYear(), value.GetMonth(), value.GetDay(), 0);
            // Xử lý tháng thiếu
            int[] lunarDate = result.Solar2Lunar();
            if (value.DateToString("yyyyMMdd") != (new DateTime(lunarDate[0], lunarDate[1], lunarDate[2]).DateToString("yyyyMMdd")))
                result = result.MinusDays(1);
            return result;
        }

        /// <summary>
        /// Chuyển chuỗi thứ 3 ký tự về enum DayOfWeek
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DayOfWeek GetDayOfWeek(this string value)
        {
            DayOfWeek result;
            switch (value)
            {
                case "MON": result = DayOfWeek.Monday; break;
                case "TUE": result = DayOfWeek.Tuesday; break;
                case "WED": result = DayOfWeek.Wednesday; break;
                case "THU": result = DayOfWeek.Thursday; break;
                case "FRI": result = DayOfWeek.Friday; break;
                case "SAT": result = DayOfWeek.Saturday; break;
                default: result = DayOfWeek.Sunday; break;
            }
            return result;
        }

        /// <summary>
        /// Tính ngày trong tháng theo cặp định dạng thứ.tuần
        /// </summary>
        /// <param name="value"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string GetDateOfWeekPair(this string value, string month)
        {
            // Lấy ngày đầu tháng
            DateTime startDate = month.StringToDate(Constant.defaultDateTimeFormat).GetFirstDateOfMonth();
            // Tìm ngày thứ 2 đầu tiên
            while (startDate.DayOfWeek != DayOfWeek.Monday) startDate = startDate.PlusDays(1);
            // Xác định tuần đầu
            int week = startDate.Equals(month.StringToDate(Constant.defaultDateTimeFormat).GetFirstDateOfMonth()) ? 1 : 2;
            // Tuần cần lấy thứ chỉ hợp lệ với 2,3,4 vì tuần 1,5,6 có thể không có thứ cần tìm
            string result;
            // Tìm ngày của thứ cần tìm trong tuần đầu tiên
            while (startDate.DayOfWeek != value.SplitByDelimiter(".")[0].GetDayOfWeek()) startDate = startDate.PlusDays(1);
            // Tìm đến tuần cần tính
            while (week < value.SplitByDelimiter(".")[1].StringToInt32())
            {
                startDate = startDate.PlusDays(7);
                week++;
            }
            result = startDate.DateToString(Constant.defaultDateTimeFormat);
            return result;
        }
    }
}
