﻿using Permission.Core.Models;
using System.Linq.Expressions;

namespace Permission.Core.Extensions.DataTable
{
    public static class LinqExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string orderByMember, DtOrderDir ascendingDirection)
        {
            var param = Expression.Parameter(typeof(T), "c");

            var body = orderByMember.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            var queryable = ascendingDirection == DtOrderDir.Asc ?
                (IOrderedQueryable<T>)Queryable.OrderBy(query.AsQueryable(), (dynamic)Expression.Lambda(body, param)) :
                (IOrderedQueryable<T>)Queryable.OrderByDescending(query.AsQueryable(), (dynamic)Expression.Lambda(body, param));

            return queryable;
        }
    }
}
