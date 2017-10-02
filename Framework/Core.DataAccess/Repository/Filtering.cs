using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.Repository
{
    public static class Filtering
    {
        public static DbSet<T> AddFilter<T>(this DbSet<T> queryable, Dictionary<object, object> queryParameters)
            where T : class
        {

            var tQueryble = queryable.AsQueryable<T>();

            var filterExpressions = new List<Expression>();
            //var eParams = new List<Expression>();
            var i = 0;
            foreach (var parameter in queryParameters)
            {
                i++;
                var eParam = Expression.Parameter(typeof(T), "p" + i);
                //eParams.Add(eParam);

                var left = Expression.Property(eParam, parameter.Key.ToString());
                var right = Expression.Constant(Convert.ChangeType(parameter.Value, Type.GetType(left.Type.FullName)));
                var lambda = Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), eParam);



                filterExpressions.Add(lambda);
            }


            //добиться фильтрации по всем полям....
            var res1 = tQueryble.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    "Where",
                    new Type[] { tQueryble.ElementType },
                    tQueryble.Expression,
                    filterExpressions[0]//, Expression.Or(filterExpressions[0], filterExpressions[1])
                    )
             );
            return queryable;
        }
    }
}
