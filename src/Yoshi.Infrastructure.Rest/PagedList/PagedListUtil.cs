using System;
using System.Linq;
using System.Linq.Expressions;

namespace Yoshi.Infrastructure.Rest.PagedList
{
    public static class PagedListUtil
    {
        public static IQueryable<T> BuildOrderExpression<T, TReturn>(IQueryable<T> query, string propertyName, bool ascending)
        {
            var paramterExpression = Expression.Parameter(typeof(T));
            var member = Expression.PropertyOrField(paramterExpression, propertyName);

            var lambdaExpression = Expression.Lambda(member, paramterExpression);

            if (ascending)
                query = query.OrderBy((Expression<Func<T, TReturn>>)lambdaExpression);
            else
                query = query.OrderByDescending((Expression<Func<T, TReturn>>)lambdaExpression);

            return query;
        }
    }
}
