using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShop.Utilities
{
    public enum LogType
    {
        /// <summary>
        /// Unknown log item type
        /// </summary>
        AllLog,
        /// <summary>
        /// Unknown log item type
        /// </summary>
        Unknown,
        /// <summary>
        /// Customer error log item type
        /// </summary>
        CustomerError,
        /// <summary>
        /// Mail error log item type
        /// </summary>
        MailError,
        /// <summary>
        /// Order error log item type
        /// </summary>
        OrderError,
        /// <summary>
        /// Administration area log item type
        /// </summary>
        AdministrationArea,
        /// <summary>
        /// Common error log item type
        /// </summary>
        CommonError,
        /// <summary>
        /// Shipping error log item type
        /// </summary>
        ShippingErrror,
        /// <summary>
        /// Tax error log item type
        /// </summary>
        TaxError,
        /// <summary>
        /// Information
        /// </summary>
        Information,
    }

    public class AppException : Exception
    {
        public LogType LogType
        { get; set; }
        public int Severity
        { get; set; }

        public AppException(Exception ex)
            : this(ex.Message, ex)
        {   
        }

        public AppException(string Message, Exception ex)
            : this(Message, ex, LogType.CustomerError) 
        {   
        }

        public AppException(string Message, Exception ex, LogType typ)
            : this(Message, ex, typ, 5)
        {

        }

        public AppException(string Message, Exception ex, LogType typ, int Sev)
            : base(Message, ex)
        {
            LogType = typ;
            Severity = Sev;
            Common.OutputLog(ex);
        }
    }
}
