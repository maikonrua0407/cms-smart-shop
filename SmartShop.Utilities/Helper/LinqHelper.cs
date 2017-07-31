using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SmartShop.Utilities.Helper
{
    /// <summary>
    /// Chứa các hàm mở rộng hỗ trợ xử lý cho LinQ
    /// </summary>
    public static class LinqHelper
    {
        #region Public Methods

        /// <summary>
        /// Sắp xếp kết quả theo danh sách column truyền vào.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="orderBy">Tên cột cần sắp xếp. Có thể nhập nhiều trường, phân biệt bằng dấu phẩy. Thêm dấu '-' vào đầu để sắp xếp ngược.</param>
        /// <returns></returns>
        public static IQueryable<T> OrderExtend<T>(this IQueryable<T> source, List<string> orderBy)
        {
            if (orderBy == null || orderBy.Count == 0)
                throw new ArgumentException("'orderBy' can not be null or empty.");

            int colIdx = 0;
            foreach (string item in orderBy)
            {
                if (colIdx <= 0)
                {
                    if (item.StartsWith("-"))
                        source = source.OrderExtend(item.Substring(1), "OrderByDescending");
                    else
                        source = source.OrderExtend(item, "OrderBy");
                }
                else
                {
                    if (item.StartsWith("-"))
                        source = source.OrderExtend(item.Substring(1), "ThenByDescending");
                    else
                        source = source.OrderExtend(item, "ThenBy");
                }

                colIdx++;
            }

            return source;
        }

        /// <summary>
        /// Tùy biến truy vấn select.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="source"></param>
        /// <param name="fields">Tên trường cần lấy dữ liệu, phân biệt chữ hoa chữ thường.</param>
        /// <returns></returns>
        public static IEnumerable<T> SelectExtend<T>(this IQueryable<T> source, List<string> fields)
        {
            if (fields == null || fields.Count <= 0)
                throw new ArgumentException("'fields' can not be null or empty.");

            var xParameter = Expression.Parameter(typeof(T), "o");
            var xNew = Expression.New(typeof(T));

            var bindings = fields.Select(o => o.Trim())
                .Select(o =>
                {
                    var mi = typeof(T).GetProperty(o);
                    var xOriginal = Expression.Property(xParameter, mi);

                    return Expression.Bind(mi, xOriginal);
                }
            );

            var xInit = Expression.MemberInit(xNew, bindings);
            var lambda = Expression.Lambda<Func<T, T>>(xInit, xParameter);

            Func<T, T> selectFunction = lambda.Compile();

            return source.Select(selectFunction);
        }

        public static IQueryable<T> WhereExtend<T>(this IQueryable<T> source, string keywords, params string[] fields)
        {
            var objType = typeof(T);
            var properties = objType.GetProperties();

            // loop each properties
            var builder = PredicateBuilder.False<T>();
            foreach (var field in fields)
            {
                builder = builder.Or(source.WhereExtend(keywords, field));
            }

            return source.Where(builder);
        }

        public static Expression<Func<T, bool>> WhereExtend<T>(this IQueryable<T> source, string keywords, string field)
        {
            var param = Expression.Parameter(typeof(T), "f");

            var predicate = Expression.Lambda<Func<T, bool>>(Expression.Call(Expression.PropertyOrField(param, field), "Contains", null, Expression.Constant(keywords)), param);

            return predicate;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Performs order datasource by invoking method from name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="columnName">Column to order</param>
        /// <param name="methodName">Method to order, ascending or descending</param>
        /// <returns></returns>
        private static IOrderedQueryable<T> OrderExtend<T>(this IQueryable<T> source, string columnName, string methodName)
        {
            List<string> validMethods = new List<string> { "OrderBy", "OrderByDescending", "ThenBy", "ThenByDescending" };

            if (!validMethods.Contains(methodName))
                throw new ArgumentException("Method name is not valid to performs order.", "methodName");

            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            // get property by columnName
            var pi = type.GetProperty(columnName);

            if (pi == null)
                throw new ArgumentException(string.Format("'{0}' is not valid for column name, it's case sensitive!", columnName));

            expr = Expression.Property(expr, pi);
            type = pi.PropertyType;

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            var result = typeof(Queryable).GetMethods().Single((method) => method.Name == methodName
                                                                        && method.IsGenericMethodDefinition
                                                                        && method.GetGenericArguments().Length == 2
                                                                        && method.GetParameters().Length == 2)
                                                        .MakeGenericMethod(typeof(T), type)
                                                        .Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<T>)result;
        }

        #endregion Private Methods
    }

    public static class PredicateBuilder
    {
        #region Public Methods

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return f => false;
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.Or(expr1.Body, expr2.Body), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }

        #endregion Public Methods
    }
}