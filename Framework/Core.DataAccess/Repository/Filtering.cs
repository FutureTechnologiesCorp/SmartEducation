using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.Repository
{
    public static class Filtering
    {
        public static IQueryable<T> AddFilter<T>(this DbSet<T> queryable, Dictionary<string, object> queryParameters)
            where T : class
        {
            var tQueryble = queryable.AsQueryable();
            Expression<Func<T, bool>> concatExpression = null;

            var tableAlias = Expression.Parameter(typeof(T), typeof(T).Name + "_alias");
            foreach (var parameter in queryParameters)
            {
                //фильтруемая колонка
                var column = Expression.PropertyOrField(tableAlias, parameter.Key);

                //значение свойства
                Expression right = null;
                if (column.Type.IsGenericType && column.Type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    right = Expression.Convert(Expression.Constant(parameter.Value), column.Type);
                else
                    right = Expression.Constant(Convert.ChangeType(parameter.Value, column.Type));

                //получим выражение
                var expression = Expression.Lambda<Func<T, bool>>(Expression.Equal(column, right), tableAlias);

                concatExpression = concatExpression == null
                                ? concatExpression = expression
                                : concatExpression.AndAlso(expression);
            }

            var res = tQueryble.Provider.CreateQuery<T>(
                Expression.Call(typeof(Queryable), "Where", new Type[] { tQueryble.ElementType }, tQueryble.Expression, concatExpression));
            return res;
        }

        private static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            //1. если параметры разные, нужно применить лямбда-выражение к списку выражений аргумента
            //2. либо сразу создадим новое выражение из двух условий
            var param = expr1.Parameters[0];
            if (ReferenceEquals(param, expr2.Parameters[0]))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, expr2.Body), param);
            }

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    expr1.Body,
                    Expression.Invoke(expr2, param)), param);
        }
    }
}
