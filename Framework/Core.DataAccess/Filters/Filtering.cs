using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static Core.DataAccess.Filters.FilteringCommonObjects;

namespace Core.DataAccess.Repository
{
    /*
        1. TODO: Необходимо расширить фиьтрацию по условиям сравнения:
        
            = (Equals) - сделано

            > (Greater Than)
            < (Less Than)
            >= (Greater Than or Equal To)
            <= (Less Than or Equal To)
            <> (Not Equal To)
            !< (Not Less Than)
            != (Not Equal To)
            !> (Not Greater Than)

         2. так же нужно пересмотреть подход для передачи данных в фильтр по одинаковым полям, но с разными условиями.
    */

    public static class Filtering
    {
        public static IQueryable<T> ApplyFilterSettings<T>(this IQueryable<T> queryable, FilterSettings<T> settings)
            where T : class
        {
            return queryable;
        }

        public static IQueryable<T> ApplyFilterByQueryParameters<T>(this IQueryable<T> queryable, Dictionary<string, object> queryParameters)
            where T : class
        {
            Expression<Func<T, bool>> concatExpression = null;


            var tableAlias = Expression.Parameter(typeof(T), typeof(T).Name + "_alias");
            foreach (var parameter in queryParameters.Where(r => !new[] {
                                                                        SortingSettingsObject ,
                                                                        PagingSettingsObject }
                                                                .Contains(r.Key)))
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

                concatExpression = concatExpression.AndAlso(expression);
            }

            if (concatExpression != null)
                return queryable.Provider.CreateQuery<T>(
                    Expression.Call(
                        typeof(Queryable),
                        "Where",
                        new Type[] { queryable.ElementType },
                        queryable.Expression,
                        concatExpression));

            return queryable;
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> queryable, Dictionary<string, object> queryParameters, ParameterExpression tableAlias = null)
            where T : class
        {
            if (queryParameters.ContainsKey(SortingSettingsObject))
            {
                if (queryParameters[SortingSettingsObject] is List<SortingSetting<T>>)
                {
                    var sortingDataList = queryParameters[SortingSettingsObject] as List<SortingSetting<T>>;

                    if (!sortingDataList.Any()) return queryable;

                    sortingDataList.Reverse();
                    Expression concatExpression = null;
                    tableAlias = tableAlias ?? Expression.Parameter(typeof(T), typeof(T).Name + "_alias");

                    foreach (var sortingData in sortingDataList)
                    {
                        //сортируемая колонка
                        var column = Expression.PropertyOrField(tableAlias, sortingData.PropertyName);
                        //найдем свойство для получения его типа 
                        var property = typeof(T).GetProperty(sortingData.PropertyName);
                        //выражение для сортировки
                        var orderExpression = Expression.Quote(Expression.Lambda(column, tableAlias));

                        concatExpression = Expression.Call(
                            typeof(Queryable),
                            sortingData.SortingType == SortingTypes.Desc ? "OrderByDescending" : "OrderBy",
                            new Type[] { typeof(T), property.PropertyType },
                            concatExpression ?? queryable.Expression,
                            orderExpression
                            );
                    }

                    if (concatExpression != null)
                        return queryable.Provider.CreateQuery<T>(concatExpression);
                }
            }

            return queryable;
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> queryable, Dictionary<string, object> queryParameters)
            where T : class
        {
            if (queryParameters.ContainsKey(PagingSettingsObject))
            {
                if (queryParameters[PagingSettingsObject] is PageSetting)
                {
                    var pageSetting = queryParameters[PagingSettingsObject] as PageSetting;

                    var page = pageSetting.PageNumber;
                    var rowCount = pageSetting.RowCountPerPage;

                    var skip = page == 1 ? 0 : (page - 1) * rowCount;

                    var r = queryable.Skip(skip).Take(rowCount);
                    return r;
                }
            }

            return queryable;
        }

        private static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> curentExpression, Expression<Func<T, bool>> newExpression)
        {
            if (curentExpression == null)
            {
                return Expression.Lambda<Func<T, bool>>(
                    Expression.Invoke(
                        newExpression, newExpression.Parameters), newExpression.Parameters);
            }

            //1. если параметры разные, нужно применить лямбда-выражение к списку выражений аргумента
            //2. либо сразу создадим новое выражение из двух условий
            var param = curentExpression.Parameters[0];
            if (ReferenceEquals(param, newExpression.Parameters[0]))
            {
                return Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(
                        curentExpression.Body, newExpression.Body), param);
            }

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    curentExpression.Body,
                    Expression.Invoke(newExpression, param)), param);
        }
    }
}
