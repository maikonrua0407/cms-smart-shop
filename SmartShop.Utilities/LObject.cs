using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SmartShop.Utilities
{
    /// <summary>
    /// TruongLq - 05/05/2014
    /// Thư viện các hàm thao tác với kiểu object
    /// </summary>
    public static class LObject
    {
        private static JavaScriptSerializer json;
        private static JavaScriptSerializer JSON { get { return json ?? (json = new JavaScriptSerializer()); } }
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
            foreach (PropertyInfo property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase))
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
            foreach (PropertyInfo property in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase))
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
            if (Object.ReferenceEquals(value, null))
                return true;
            // Kiểm tra kiểu string
            if (typeof(T) == typeof(String))
                return value.Equals(String.Empty);
            try
            {
                // Kiểm tra các object có khởi tạo không cần tham số (vd: new())
                T obj = Activator.CreateInstance<T>();
                if (value.Equals(obj))
                    return true;
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
            var destProperties = destination.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase);

            foreach (var sourceProperty in source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase))
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

        public static void ListToFileXml<T>(this T data, string path)
        {
            //Write List<T> to XML file
            string output = path + typeof(T).Name + DateTime.Today.DateToString("yyyyMMdd") + ".xml";
            XmlSerializer serialiser = new XmlSerializer(typeof(List<T>));
            TextWriter FileStream = new StreamWriter(output);
            serialiser.Serialize(FileStream, data);
            FileStream.Close();
        }

        /// <summary>
        /// Copy data from List of Object to another List of Object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<T> Copy<T>(IEnumerable entity)
        {
            List<T> retObj = new List<T>();

            foreach (var item in entity)
            {
                retObj.Add(Copy<T>(item));
            }

            return retObj;
        }

        /// <summary>
        /// Copy data from object to another object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T Copy<T>(object entity)
        {
            T retObj = Activator.CreateInstance<T>();
            if (entity.IsNullOrEmpty())
                return retObj;
            PropertyInfo[] properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase);

            object propValue = null;
            foreach (var item in properties)
            {
                // get value from object
                propValue = item.GetValue(entity, null);

                // get property by name
                var propertyInfo = retObj.GetType().GetProperty(item.Name);

                if (propertyInfo == null)
                    continue;

                // set value to this property of return object
                propertyInfo.SetValue(retObj, propValue, null);
            }

            return retObj;
        }

        public static T Map<T>(object entity)
        {
            return Helper.BoHelper.TranslateObject<T>(entity);
        }

        public static List<T> Maps<T>(IEnumerable entity)
        {
            return Helper.BoHelper.TranslateListObject<T>(entity);
        }

        public static string JsonSerialize(object obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            //create a memory stream
            var ms = new MemoryStream();
            //serialize the object to memory stream
            serializer.WriteObject(ms, obj);
            //convert the serizlized object to string
            var jsonString = Encoding.UTF8.GetString(ms.ToArray());
            //close the memory stream
            ms.Close();
            return jsonString;
        }

        public static string JsonZip(object obj)
        {
            var jsonString = JsonSerialize(obj);
            return Common.Compress(jsonString);
        }

        public static T JsonDeserialize<T>(string json)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            return (T)serializer.ReadObject(stream);
        }

        public static IEnumerable<t> DistinctBy<t>(this IEnumerable<t> list, Func<t, object> propertySelector)
        {
            return list.GroupBy(propertySelector).Select(x => x.First());
        }

        public static Stream ToStream(this string @this)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@this);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static bool IsList<T>(this T obj)
        {
            if (obj == null)
                return false;

            return obj is IList &&
                   obj.GetType().IsGenericType &&
                   obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static T ParseXML<T>(this string @this) where T : class
        {
            var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }

        public static T ParseJSON<T>(this string @this) where T : class
        {
            return JSON.Deserialize<T>(@this.Trim());
        }

        public static T ConvertNode<T>(XmlNode node) where T : class
        {
            MemoryStream stm = new MemoryStream();

            StreamWriter stw = new StreamWriter(stm);
            stw.Write(node.OuterXml);
            stw.Flush();

            stm.Position = 0;

            XmlSerializer ser = new XmlSerializer(typeof(T));
            T result = (ser.Deserialize(stm) as T);

            return result;
        }
    }
}
