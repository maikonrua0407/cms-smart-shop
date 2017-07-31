using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Globalization;
using System.Web;

namespace SmartShop.Utilities.Helper
{
    public static class MyExtensionClass
    {
        public static List<T> ToCollection<T>(this DataTable dt)
        {
            var lst = new List<T>();
            var tClass = typeof(T);
            var pClass = new List<PropertyInfo>(tClass.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy));
            var pTClass = new List<PropertyInfo>();
            var dc = dt.Columns.Cast<DataColumn>().ToList();
            foreach (DataRow item in dt.Rows)
            {
                var cn = (T)Activator.CreateInstance(tClass);
                foreach (var pc in from pc in pClass let d = dc.Find(c => c.ColumnName.Equals(pc.Name, StringComparison.CurrentCultureIgnoreCase)) where d != null select pc)
                {
                    pc.SetValue(cn, BoHelper.ConvertData(item[pc.Name], pc.PropertyType), null);
                    if (pTClass.Count < pClass.Count)
                        pTClass.Add(pc);
                }
                lst.Add(cn);
                if (pClass.Count > pTClass.Count)
                    pClass = pTClass;
            }
            return lst;
        }


        public static List<T> ToCollection<T>(this IDataReader rd)
        {
            var lst = new List<T>();
            if (rd.IsClosed || !(rd is SqlDataReader) || !((SqlDataReader)rd).HasRows) return lst;
            var tClass = typeof(T);
            var pClass = new List<string>();
            var pTClass = new List<string>();

            for (var id = 0; id < rd.FieldCount; id++)
            {
                pClass.Add(rd.GetName(id));
            }
            do
            {
                var objBo = (T)Activator.CreateInstance(tClass);
                foreach (var columnName in pClass)
                {
                    var proInfor = tClass.GetProperty(columnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase);
                    if (proInfor == null || !proInfor.CanWrite) continue;
                    proInfor.SetValue(objBo, BoHelper.ConvertData(rd[proInfor.Name], proInfor.PropertyType), null);
                    if (pTClass.Count < pClass.Count)
                        pTClass.Add(columnName);
                }
                lst.Add(objBo);
                if (pClass.Count > pTClass.Count)
                    pClass = pTClass;
            } while (rd.Read());
            return lst;
        }
    }

    public class BoHelper
    {
        #region "Public methods"

        public static object ConvertData(object source, Type typ)
        {
            return ConvertData(source, typ, new CultureInfo("en-US"));
        }

        public static object ConvertData(object source, Type typ, CultureInfo cul)
        {
            if (Equals(source, null) || Equals(source, DBNull.Value))
                return "string".Equals(typ.Name, StringComparison.CurrentCultureIgnoreCase) ? string.Empty : null;
            switch (typ.Name.ToLower())
            {
                case "int?":
                case "int":
                case "int32":
                case "int16":
                case "int32?":
                case "int16?":
                    return int.Parse(source.ToString());
                case "double":
                case "double?":
                    return double.Parse(source.ToString());
                case "single":
                case "single?":
                case "float":
                case "float?":
                    return float.Parse(source.ToString());
                case "decimal":
                case "decimal?":
                    return decimal.Parse(source.ToString());
                case "bool":
                case "bool?":
                case "boolean":
                case "boolean?":
                    return ConvertToBoolean(source);
                case "datetime":
                case "datetime?":
                    try
                    {
                        return DateTime.Parse(source.ToString(), cul);
                    }
                    catch
                    {
                        return DateTime.Parse(source.ToString(), cul.Name.Equals("en-US") ? new CultureInfo("vi-VN") : new CultureInfo("en-US"));
                    }
                case "char":
                case "char?":
                    return char.Parse(source.ToString());
                case "string":
                    return source.ToString();
                case "object":
                    return source;
                case "byte[]":
                    return (byte[])source;
                case "nullable`1":
                    typ = Nullable.GetUnderlyingType(typ);
                    return ConvertData(source, typ);
                default:
                    if (typ.BaseType != null)
                        switch (typ.BaseType.Name.ToLower())
                        {
                            case "enum":
                                return Enum.Parse(typ, source.ToString(), true);
                        }
                    break;
            }
            return source;
        }

        public static object ConvertData(object source, string typName)
        {
            return ConvertData(source, typName, new CultureInfo("en-US"));
        }

        public static object ConvertData(object source, string typName, CultureInfo cul)
        {
            switch (typName.ToLower())
            {
                case "int?":
                case "int":
                case "int32":
                case "int16":
                case "int32?":
                case "int16?":
                    return int.Parse(source.ToString());
                case "double":
                case "double?":
                    return double.Parse(source.ToString());
                case "single":
                case "single?":
                case "float":
                case "float?":
                    return float.Parse(source.ToString());
                case "decimal":
                case "decimal?":
                    return decimal.Parse(source.ToString());
                case "bool":
                case "bool?":
                case "boolean":
                case "boolean?":
                    return ConvertToBoolean(source);
                case "datetime":
                case "datetime?":
                    try
                    {
                        return DateTime.Parse(source.ToString(), cul);
                    }
                    catch
                    {
                        return DateTime.Parse(source.ToString(), cul.Name.Equals("en-US") ? new CultureInfo("vi-VN") : new CultureInfo("en-US"));
                    }
                case "char":
                case "char?":
                    return char.Parse(source.ToString());
                case "string":
                    return source.ToString();
                case "object":
                    return source;
                case "byte[]":
                    return (byte[])source;
            }
            return source;
        }

        public static string ConvertToString(object source)
        {
            if (Equals(source, null))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(source.ToString()))
            {
                return string.Empty;
            }

            if (source is DateTime)
            {
                return ((DateTime)source).ToString("dd/MM/yyyy");
            }

            //if (source is System.DateTime)
            //{
            //    return ((System.DateTime)source).ToString("dd/MM/yyyy HH:mm:ss");
            //}

            return source.ToString();
        }

        public static string ConvertToString(object source, bool isMoney)
        {
            return isMoney ? ((double)source).ToString(",.") : ConvertToString(source);
        }

        public static bool ConvertToBoolean(object source)
        {
            if (Equals(source, null) || Equals(source, DBNull.Value) || string.IsNullOrEmpty(source.ToString()))
            {
                return false;
            }

            var b = false;
            if (!bool.TryParse(source.ToString(), out b))
                b = "Y".Equals(source.ToString().ToUpper()) || (int)source == 1;
            return b;
        }

        public static char ConvertToChar(bool source)
        {
            return source ? '1' : '0';
        }

        public static void FillBoDataRow(DataRow source, object objBo)
        {
            if (Equals(source, null)) return;
            foreach (var proInfor in from DataColumn colum in source.Table.Columns select objBo.GetType().GetProperty(colum.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase) into proInfor where proInfor != null where proInfor.CanWrite select proInfor)
            {
                proInfor.SetValue(objBo, ConvertData(source[proInfor.Name], proInfor.PropertyType), null);
            }
        }

        public static void FillBoData(DataRow source, object objBo)
        {
            if (Equals(source, null)) return;
            foreach (var proInfor in from DataColumn colum in source.Table.Columns select objBo.GetType().GetProperty(colum.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase) into proInfor where proInfor != null where proInfor.CanWrite select proInfor)
            {
                proInfor.SetValue(objBo, ConvertData(source[proInfor.Name], proInfor.PropertyType), null);
            }
        }

        public static T FillBoData<T>(DataRow source)
        {
            var objBo = (T)Activator.CreateInstance(typeof(T));

            FillBoData(source, objBo);

            return objBo;
        }

        public static List<T> FillBoListData<T>(DataTable source)
        {
            return source != null ? source.ToCollection<T>() : null;
        }

        public static object TranslateObject(Type typ, object value)
        {
            return typ.IsSealed || value == null ? value : TranslateObject(typ, Activator.CreateInstance(typ), value);
        }

        public static object TranslateObject(Type typ, object objBo, object value)
        {
            if (value == null) return null;
            if (objBo == null)
                return TranslateObject(typ, value);

            var proSourceInfors = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase);
            foreach (var proSourceInfor in proSourceInfors)
            {
                if (proSourceInfor == null || !proSourceInfor.CanRead) continue;
                var proInfor = objBo.GetType().GetProperty(proSourceInfor.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase);
                if (proInfor == null) continue;
                if (proInfor.CanWrite)
                {
                    if (proInfor.PropertyType.IsSealed)
                    {
                        proInfor.SetValue(objBo, ConvertData(proSourceInfor.GetValue(value, null), proInfor.PropertyType), null);

                    }
                    else if (proInfor.PropertyType.IsGenericType || proInfor.PropertyType.IsArray)
                    {
                        //proInfor.SetValue(objBo, TranslateListObject(proInfor.PropertyType, proSourceInfor.GetValue(value, null)), null);
                    }
                    else
                    {
                        var val = proSourceInfor.GetValue(value, null);
                        if (val != null)
                            proInfor.SetValue(objBo, TranslateObject(proInfor.PropertyType, val), null);
                    }
                }
            }
            return objBo;
        }

        public static T TranslateObject<T>(object value)
        {
            return (T)TranslateObject(typeof(T), value);
        }

        public static List<T> TranslateListObject<T>(List<object> list)
        {
            return TranslateListObject(typeof(List<T>), list) as List<T>;
        }

        public static List<T> TranslateListObject<T>(IEnumerable list)
        {
            return TranslateListObject(typeof(List<T>), list) as List<T>;
        }

        public static object TranslateListObject(Type typ, object source)
        {
            object list = null;
            if (typ.IsGenericType)
            {
                list = Activator.CreateInstance(typ);
            }
            else if (typ.IsArray)
            {
                list = new ArrayList();
            }

            return TranslateListObject(typ, list, source);
        }

        public static object TranslateListObject(Type tType, object destination, object source)
        {
            if (source == null) return null;
            if (destination == null)
                return TranslateListObject(tType, source);

            var fType = source.GetType();

            if (fType.BaseType != null && (tType.IsGenericType || fType.BaseType.IsGenericType))
            {
                var itemType = tType.GetGenericArguments()[0];
                var list = destination as IList;
                if (list != null)
                {
                    if (fType.IsGenericType || fType.BaseType.IsGenericType)
                    {
                        if (fType.GetInterface("IList", true) != null)
                        {
                            var s1 = source as IList;
                            if (s1 != null)
                                foreach (var item in s1)
                                {
                                    list.Add(TranslateObject(itemType, item));
                                }
                        }
                        else if (fType.GetInterface("IEnumerable", true) != null)
                        {
                            var s1 = source as IEnumerable;
                            if (s1 != null)
                                foreach (var item in s1)
                                {
                                    list.Add(TranslateObject(itemType, item));
                                }
                        }
                    }
                    else if (fType.IsArray)
                    {
                        var s1 = source as Array;
                        if (s1 != null)
                            foreach (var item in s1)
                            {
                                list.Add(TranslateObject(itemType, item));
                            }
                    }
                }
                return list;
            }
            else if (fType.IsArray)
            {
                var itemType = tType.GetElementType();
                var list = destination as ArrayList;

                if (list != null)
                {
                    if (tType.BaseType != null && (fType.IsGenericType || tType.BaseType.IsGenericType))
                    {
                        if (fType.GetInterface("IList", true) != null)
                        {
                            var s1 = source as IList;
                            if (s1 != null)
                                foreach (var item in s1)
                                {
                                    list.Add(TranslateObject(itemType, item));
                                }
                        }
                        else if (fType.GetInterface("IEnumerable", true) != null)
                        {
                            var s1 = source as IEnumerable;
                            if (s1 != null)
                                foreach (var item in s1)
                                {
                                    list.Add(TranslateObject(itemType, item));
                                }
                        }
                    }
                    else if (fType.IsArray)
                    {
                        var s1 = source as IList;
                        if (s1 != null)
                            foreach (var item in s1)
                            {
                                list.Add(TranslateObject(itemType, item));
                            }
                    }
                }
                return list == null ? null : list.ToArray(itemType);
            }

            return null;
        }

        public static void CopyData(object source, object destination)
        {
            TranslateObject(destination.GetType(), destination, source);
        }

        public static void FillBoData(IDataReader source, object objBo)
        {
            if (Equals(source, null)) return;
            if (source.IsClosed || !((SqlDataReader)source).HasRows) return;
            //source.Read();
            for (var id = 0; id < source.FieldCount; id++)
            {
                var columnName = source.GetName(id);
                var proInfor = objBo.GetType().GetProperty(columnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase);
                if (proInfor == null) continue;
                if (proInfor.CanWrite)
                {
                    proInfor.SetValue(objBo, ConvertData(source[proInfor.Name], proInfor.PropertyType), null);
                }
            }
        }

        public static T FillBoData<T>(IDataReader source)
        {
            var objBo = (T)Activator.CreateInstance(typeof(T));

            FillBoData(source, objBo);

            return objBo;
        }

        public static List<T> FillBoListData<T>(IDataReader source)
        {
            return source != null ? source.ToCollection<T>() : null;
        }

        public static void FillBo2Params(object objBo, SqlParameter[] Params)
        {
            if (objBo == null || Params == null || Params.Length == 0) return;
            var tClass = objBo.GetType();
            var pClass = new List<PropertyInfo>(tClass.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy));
            var dc = Params.ToList();
            foreach (var pc in dc)
            {
                var pName = pc.ParameterName.Replace("@", string.Empty);
                if (!pClass.Exists(c => c.Name.Equals(pName, StringComparison.CurrentCultureIgnoreCase))) continue;
                var pro = pClass.Single(c => c.Name.Equals(pName, StringComparison.CurrentCultureIgnoreCase));
                var val = pro.GetValue(objBo, null);
                if (val == null || Equals(val, string.Empty) || Equals(val, DateTime.MinValue))
                    pc.Value = DBNull.Value;
                else if (pc.SqlDbType == SqlDbType.Bit && val is bool)
                    pc.Value = ((bool)val) ? 1 : 0;
                else
                    pc.Value = val;
            }
        }

        public static void FillParams2Bo(SqlParameter[] Params, object objBo)
        {
            if (objBo == null || Params == null || Params.Length == 0) return;
            var tClass = objBo.GetType();
            var pClass = new List<PropertyInfo>(tClass.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy));
            var dc = Params.ToList();
            foreach (var pc in dc.Where(a => a.Direction == ParameterDirection.Output || a.Direction == ParameterDirection.InputOutput))
            {
                var pName = pc.ParameterName.Replace("@", string.Empty);
                if (!pClass.Exists(c => c.Name.Equals(pName, StringComparison.CurrentCultureIgnoreCase))) continue;
                var pro = pClass.Single(c => c.Name.Equals(pName, StringComparison.CurrentCultureIgnoreCase));
                pro.SetValue(objBo, ConvertData(pc.Value, pro.PropertyType), null);
            }
        }

        public static object GetPropertyValue(object data, string propertyName)
        {
            if (data == null) return string.Empty;
            var tClass = data.GetType();
            var pClass = new List<PropertyInfo>(tClass.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy));
            if (!pClass.Exists(c => c.Name.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase))) return string.Empty;
            var pro = pClass.Single(c => c.Name.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase));
            return pro.GetValue(data, null);
        }

        #endregion
    }
}