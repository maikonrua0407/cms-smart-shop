using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Utilities
{
    /// <summary>
    /// TruongLq - 05/05/2014
    /// Thư viện các hàm thao tác với kiểu object
    /// </summary>
    public static class LObject
    {
        /// <summary>
        /// Lấy tên đối tượng
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <param name="value">Đối tượng cần lấy tên</param>
        /// <returns>Trả lại tên của đối tượng</returns>
        public static string GetObjectName<T>(this T value)
        {
            return typeof(T).FullName;
        }

        /// <summary>
        /// Lấy danh sách tên các thuộc tính của đối tượng
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <param name="value">Đối tượng cần lấy tên các thuộc tính</param>
        /// <returns>Mảng kiểu string chứa tên các thuộc tính</returns>
        public static string[] GetListPropertyName<T>(this T value)
        {
            List<string> propertyName = new List<string>();
            foreach (PropertyInfo property in typeof(T).GetProperties())
                propertyName.Add(property.Name);

            return propertyName.ToArray<string>();
        }

        /// <summary>
        /// Lấy danh sách tên các thuộc tính của đối tượng
        /// </summary>
        /// <param name="obj">Đối tượng cần lấy tên các thuộc tính</param>
        /// <returns>Mảng kiểu string chứa tên các thuộc tính</returns>
        public static string[] GetListPropertyName(object obj)
        {
            List<string> propertyName = new List<string>();
            Type objType = obj.GetType();
            foreach (PropertyInfo property in objType.GetProperties())
                propertyName.Add(property.Name);

            return propertyName.ToArray<string>();
        }

        /// <summary>
        /// Lấy danh sách tên các sự kiện của đối tượng
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <param name="value">Đối tượng cần lấy tên các sự kiện</param>
        /// <returns>Mảng kiểu string chứa tên các sự kiện</returns>
        public static string[] GetListEventName<T>(this T value)
        {
            List<string> eventName = new List<string>();
            foreach (EventInfo evt in typeof(T).GetEvents())
                eventName.Add(evt.Name);

            return eventName.ToArray<string>();
        }

        /// <summary>
        /// Lấy danh sách tên các sự kiện của đối tượng
        /// </summary>
        /// <param name="obj">Đối tượng cần lấy tên các sự kiện</param>
        /// <returns>Mảng kiểu string chứa tên các sự kiện</returns>
        public static string[] GetListEventName(object obj)
        {
            List<string> eventName = new List<string>();
            Type objType = obj.GetType();
            foreach (EventInfo evt in objType.GetEvents())
                eventName.Add(evt.Name);

            return eventName.ToArray<string>();
        }

        /// <summary>
        /// Kiểm tra đối tượng null hoặc rỗng
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <param name="value">Đối tượng cần kiểm tra</param>
        /// <returns>Trả lại true nếu đối tượng null hoặc rỗng. Trả lại false nếu ngược lại</returns>
        public static bool IsNullOrEmpty<T>(this T value)
        {
            // Kiểm tra null
            if (Object.ReferenceEquals(value, null)) return true;
            // Kiểm tra kiểu string
            if (typeof(T) == typeof(String)) return value.Equals(String.Empty);
            try
            {   // Kiểm tra các object có khởi tạo không cần tham số (vd: new())
                T obj = Activator.CreateInstance<T>();
                if (value.Equals(obj)) return true;
            }
            catch
            {
                // Các object khởi tạo có tham số coi như <> empty
                return false;
            }
            return false;
        }

        public static void CopyPropertyValues(object source, object destination)
        {
            var destProperties = destination.GetType().GetProperties();

            foreach (var sourceProperty in source.GetType().GetProperties())
            {
                if (sourceProperty.CanWrite)
                {
                    foreach (var destProperty in destProperties)
                    {
                        if (destProperty.Name == sourceProperty.Name &&
                    destProperty.PropertyType == sourceProperty.PropertyType)
                        {
                            destProperty.SetValue(destination, sourceProperty.GetValue(
                                source, new object[] { }), new object[] { });

                            break;
                        }
                    }
                }
            }
        }
    }
}
