using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Utilities
{
    public enum AggregateFunction
    {
        Count = 1,
        Sum = 2,
        First = 3,
        Last = 4,
        Average = 5,
        Max = 6,
        Min = 7,
        Exists = 8
    }


    public class Pivot
    {
        private DataTable _SourceTable = new DataTable();
        public Pivot(DataTable SourceTable)
        {
            _SourceTable = SourceTable;
        }

        /// <summary>
        /// Pivots the DataTable based on provided RowField, DataField, Aggregate Function and ColumnFields.//
        /// </summary>
        /// <param name="RowField">The column name of the Source Table which you want to spread into rows</param>
        /// <param name="DataField">The column name of the Source Table which you want to spread into Data Part</param>
        /// <param name="Aggregate">The Aggregate function which you want to apply in case matching data found more than once</param>
        /// <param name="ColumnFields">The List of column names which you want to spread as columns</param>
        /// <returns>A DataTable containing the Pivoted Data</returns>
        public DataTable PivotData(string RowField, string DataField, AggregateFunction Aggregate, params string[] ColumnFields)
        {
            DataTable dt = new DataTable();
            string Separator = ".";
            var RowList = (from x in _SourceTable.AsEnumerable() select new { Name = x.Field<object>(RowField) }).Distinct();
            // Gets the list of columns .(dot) separated.
            var ColList = (from x in _SourceTable.AsEnumerable()
                           select new
                           {
                               Name = ColumnFields.Select(n => x.Field<object>(n))
                                   .Aggregate((a, b) => a += Separator + b.ToString())
                           })
                               .Distinct()
                               .OrderBy(m => m.Name);

            dt.Columns.Add(RowField);
            foreach (var col in ColList)
            {
                dt.Columns.Add(col.Name.ToString());  // Cretes the result columns.//
            }

            foreach (var RowName in RowList)
            {
                DataRow row = dt.NewRow();
                row[RowField] = RowName.Name.ToString();
                foreach (var col in ColList)
                {
                    string strFilter = RowField + " = '" + RowName.Name + "'";
                    string[] strColValues = col.Name.ToString().Split(Separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < ColumnFields.Length; i++)
                        strFilter += " and " + ColumnFields[i] + " = '" + strColValues[i] + "'";
                    row[col.Name.ToString()] = GetData(strFilter, DataField, Aggregate);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        /// <summary>
        /// Retrives the data for matching RowField value and ColumnFields values with Aggregate function applied on them.
        /// </summary>
        /// <param name="Filter">DataTable Filter condition as a string</param>
        /// <param name="DataField">The column name which needs to spread out in Data Part of the Pivoted table</param>
        /// <param name="Aggregate">Enumeration to determine which function to apply to aggregate the data</param>
        /// <returns></returns>
        private object GetData(string Filter, string DataField, AggregateFunction Aggregate)
        {
            try
            {
                DataRow[] FilteredRows = _SourceTable.Select(Filter);
                object[] objList = FilteredRows.Select(x => x.Field<object>(DataField)).ToArray();

                switch (Aggregate)
                {
                    case AggregateFunction.Average:
                        return GetAverage(objList);
                    case AggregateFunction.Count:
                        return objList.Count();
                    case AggregateFunction.Exists:
                        return (objList.Count() == 0) ? "False" : "True";
                    case AggregateFunction.First:
                        return GetFirst(objList);
                    case AggregateFunction.Last:
                        return GetLast(objList);
                    case AggregateFunction.Max:
                        return GetMax(objList);
                    case AggregateFunction.Min:
                        return GetMin(objList);
                    case AggregateFunction.Sum:
                        return GetSum(objList);
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                return "#Error";
            }
        }

        private object GetAverage(object[] objList)
        {
            return objList.Count() == 0 ? null : (object)(Convert.ToDecimal(GetSum(objList)) / objList.Count());
        }
        private object GetSum(object[] objList)
        {
            return objList.Count() == 0 ? null : (object)(objList.Aggregate(new decimal(), (x, y) => x += Convert.ToDecimal(y)));
        }
        private object GetFirst(object[] objList)
        {
            return (objList.Count() == 0) ? null : objList.First();
        }
        private object GetLast(object[] objList)
        {
            return (objList.Count() == 0) ? null : objList.Last();
        }
        private object GetMax(object[] objList)
        {
            return (objList.Count() == 0) ? null : objList.Max();
        }
        private object GetMin(object[] objList)
        {
            return (objList.Count() == 0) ? null : objList.Min();
        }
    }
}
