using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Data
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }
        public static IQueryable<TSource> WhereSelfIf<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> condition, Expression<Func<TSource, bool>> predicate)
        {

            if (source.Where(condition).Any())
                return source.Where(condition).Where(predicate);
            else
                return source;
        }

        public static IQueryable<TSource> Includes<TSource>(this IQueryable<TSource> source, params Expression<Func<TSource, object>>[] includes) where TSource : class
        {
            return includes.Aggregate(source, (current, includeProperty) => current.Include(includeProperty));
        }

        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
                pageNumber = 1;
            if (pageSize <= 0)
                return source;
            return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string propertyName, string sortDir)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return source;
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException("Sort property not valid.");
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var methodName = "OrderBy";
            if (sortDir == SortDirection.Descending)
                methodName = "OrderByDescending";
            var resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static async Task<QueryResult<T>> ToQueryResult<T>(this IQueryable<T> dbQuery, int pageNumber = 1, int pageSize = 10, bool isCountOnly = false) where T : class
        {
            if (dbQuery == null)
                throw new ArgumentNullException("dbQuery");
            int count = await dbQuery.CountAsync();
            dbQuery = dbQuery.Page(pageNumber, pageSize);
            var data = isCountOnly ? null : await dbQuery.ToListAsync();
            return new QueryResult<T>(data, count, pageNumber, pageSize);
        }
        public static async Task<QueryResult<T>> ToQueryResult<T>(this IEnumerable<T> dbQuery, int pageNumber = 1, int pageSize = 10, bool isCountOnly = false) where T : class
        {
            if (dbQuery == null)
                throw new ArgumentNullException("dbQuery");
            int count = dbQuery.Count();
            dbQuery = dbQuery.Skip(((pageNumber < 0 ? 1 : pageNumber) - 1) * pageSize).Take(pageSize).ToList();
            var data = isCountOnly ? null : dbQuery.ToList();
            return new QueryResult<T>(data, count, pageNumber, pageSize);
        }
    }
}
