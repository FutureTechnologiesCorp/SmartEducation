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
            var i = 0;

            var eParam = Expression.Parameter(typeof(T), "p");
            foreach (var parameter in queryParameters)
            {
                i++;
                //свойство
                var left = Expression.PropertyOrField(eParam, parameter.Key);
                //значение свойства
                
                var right = Expression.Constant(Convert.ChangeType(parameter.Value, left.Type));
                
                //TODO: переделать биндинг в выражение............ т.к. не работают nullable типы
                //полуичм выражение
                var expression = Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), eParam);

                if (i == 1)
                {
                    concatExpression = expression;
                }
                else
                {
                    concatExpression = concatExpression.AndAlso(expression);
                }
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
