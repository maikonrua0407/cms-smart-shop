using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Utilities
{
    /// <summary>
    /// TruongLq - 05/05/2014
    /// </summary>
    public static class Constant
    {
        /// <summary>
        /// Danh sách các ngôn ngữ
        /// </summary>
        public enum Languages
        {
            Vietnamese,
            English
        }

        /// <summary>
        /// Định dạng ngày tháng mặc định dùng để xử lý và trao đổi dữ liệu trong ứng dụng
        /// </summary>
        public const string defaultDateTimeFormat = "dd/MM/yyyy";

        /// <summary>
        /// Loại lịch: Lịch nghỉ theo âm lịch, dương lịch hoặc lịch làm việc vào ngày nghỉ
        /// </summary>
        public enum LoaiLich { LICH_AM, LICH_DUONG, LICH_LVIEC };
        /// <summary>
        /// Lấy giá trị kiểu string của enum
        /// </summary>
        /// <param name="loaiLich">Enum cần lấy giá trị kiểu string</param>
        /// <returns>Trả lại chuỗi chứa giá trị kiểu string</returns>
        public static string layGiaTri(this LoaiLich loaiLich)
        {
            switch (loaiLich)
            {
                case LoaiLich.LICH_AM: return "LICH_AM";
                case LoaiLich.LICH_DUONG: return "LICH_DUONG";
                case LoaiLich.LICH_LVIEC: return "LICH_LVIEC";
                default: return "";
            }
        }

        /// <summary>
        /// Tần suất thời gian
        /// </summary>
        public enum TAN_SUAT { NGAY, TUAN, QUY, THANG, NAM, NA };
        /// <summary>
        /// Lấy giá trị kiểu string của enum
        /// </summary>
        /// <param name="loaiDoiTuong">Enum cần lấy giá trị kiểu string</param>
        /// <returns>Trả lại chuỗi chứa giá trị kiểu string</returns>
        public static string layGiaTri(this TAN_SUAT TAN_SUAT)
        {
            switch (TAN_SUAT)
            {
                case TAN_SUAT.NGAY: return "NGAY";
                case TAN_SUAT.TUAN: return "TUAN";
                case TAN_SUAT.QUY: return "QUY";
                case TAN_SUAT.THANG: return "THANG";
                case TAN_SUAT.NAM: return "NAM";
                case TAN_SUAT.NA: return "NA";
                default: return "";
            }
        }


        /// <summary>
        /// Loại phiên bản
        /// </summary>
        public enum PHIEN_BAN { APP, MENU, NA };
        /// <summary>
        /// Lấy giá trị kiểu string của enum
        /// </summary>
        /// <param name="loaiDoiTuong">Enum cần lấy giá trị kiểu string</param>
        /// <returns>Trả lại chuỗi chứa giá trị kiểu string</returns>
        public static string layGiaTri(this PHIEN_BAN phienBan)
        {
            switch (phienBan)
            {
                case PHIEN_BAN.APP: return "APP";
                case PHIEN_BAN.MENU: return "MENU";
                case PHIEN_BAN.NA: return "NA";
                default: return "";
            }
        }
    }
}
