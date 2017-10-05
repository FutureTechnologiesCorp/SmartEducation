using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static Core.DataAccess.Filters.FilteringCommonObjects;

namespace Core.DataAccess.Repository
{
    public static class Filtering
    {
        public static IQueryable<T> ApplyFilterSettings<T>(this IQueryable<T> queryable, FilterSettings<T> settings)
            where T : class
        {
            Expression<Func<T, bool>> expression = null;

            foreach (var conditions in settings.SettingsFiltering.GroupBy(r => r.ComparisonType))
            {
                queryable.CreateExpression(ref expression, conditions);
            }

            if (expression != null)
                queryable = queryable.Provider.CreateQuery<T>(
                        Expression.Call(
                            typeof(Queryable),
                            "Where",
                            new Type[] { queryable.ElementType },
                            queryable.Expression,
                            expression));

            ApplySorting(ref queryable, settings.SettingsSorting);
            ApplyPaging(ref queryable, settings.SettingsPage);

            return queryable;
        }

        public static void CreateExpression<T>(this IQueryable<T> queryable, ref Expression<Func<T, bool>> expression, IGrouping<ComparisonTypes, ConditionFilter<T>> conditions)
            where T : class
        {
            Expression<Func<T, bool>> concatExpression = null;

            var tableAlias = Expression.Parameter(typeof(T), typeof(T).Name + "_alias");
            foreach (var filter in conditions)
            {
                //фильтруемая колонка
                var column = Expression.PropertyOrField(tableAlias, filter.PropertyName);

                //значение свойства
                Expression right = null;
                if (column.Type.IsGenericType && column.Type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    right = Expression.Convert(Expression.Constant(filter.ComparisonObject), column.Type);
                else
                    right = Expression.Constant(Convert.ChangeType(filter.ComparisonObject, column.Type));

                //получим выражение
                var exp = GetExpressionByComparitionType(column, right, conditions.Key);

                var lambda = Expression.Lambda<Func<T, bool>>(exp, tableAlias);
                
                concatExpression = concatExpression == null ? lambda : concatExpression.CombineWithAndAlso(lambda);
            }

            if (expression == null)
            {
                expression = concatExpression;
            }
            else if (concatExpression != null)
            {
                expression = expression.CombineWithAndAlso(concatExpression);
            }
        }

        public static void ApplySorting<T>(ref IQueryable<T> queryable, List<SortRequest<T>> sortRequsetList, ParameterExpression tableAlias = null)
            where T : class
        {
            if (!sortRequsetList.Any()) return;

            sortRequsetList.Reverse();
            Expression concatExpression = null;
            tableAlias = tableAlias ?? Expression.Parameter(typeof(T), typeof(T).Name + "_alias");

            foreach (var sortingData in sortRequsetList)
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
                queryable = queryable.Provider.CreateQuery<T>(concatExpression);
        }

        public static void ApplyPaging<T>(ref IQueryable<T> queryable, PageRequest pageSetting)
            where T : class
        {
            if (pageSetting == null) return;

            var page = pageSetting.PageNumber;
            var rowCount = pageSetting.RowCountPerPage;

            var skip = page == 1 ? 0 : (page - 1) * rowCount;

            queryable = queryable.Skip(skip).Take(rowCount);            
        }
        
        private static BinaryExpression GetExpressionByComparitionType(Expression left, Expression right, ComparisonTypes comparisonType)
        {
            switch (comparisonType)
            {
                case ComparisonTypes.Equals: return Expression.Equal(left, right);
                case ComparisonTypes.GreaterThen: return Expression.GreaterThan(left, right);
                case ComparisonTypes.LessThan: return Expression.LessThan(left, right);
                case ComparisonTypes.GreaterThenOrEqualTo: return Expression.GreaterThanOrEqual(left, right);
                case ComparisonTypes.LessThanOrEqualTo: return Expression.LessThanOrEqual(left, right);
                case ComparisonTypes.NotEqualTo: return Expression.NotEqual(left, right);
                default: throw new NotImplementedException($"Тип сравнения \"{ comparisonType }\" не реализован");
            }
        }
    }

    public static class CombineExpressions
    {
        public static Expression<Func<TInput, bool>> CombineWithAndAlso<TInput>(this Expression<Func<TInput, bool>> currentExpression, Expression<Func<TInput, bool>> newExpression)
        {
            return Expression.Lambda<Func<TInput, bool>>(
                Expression.AndAlso(
                    currentExpression.Body, new ExpressionParameterReplacer(newExpression.Parameters, currentExpression.Parameters).Visit(newExpression.Body)),
                currentExpression.Parameters);
        }

        public static Expression<Func<TInput, bool>> CombineWithOrElse<TInput>(this Expression<Func<TInput, bool>> currentExpression, Expression<Func<TInput, bool>> newExpression)
        {
            return Expression.Lambda<Func<TInput, bool>>(
                Expression.OrElse(
                    currentExpression.Body, new ExpressionParameterReplacer(newExpression.Parameters, currentExpression.Parameters).Visit(newExpression.Body)),
                currentExpression.Parameters);
        }

        private class ExpressionParameterReplacer : ExpressionVisitor
        {
            private IDictionary<ParameterExpression, ParameterExpression> ParameterReplacements { get; set; }

            public ExpressionParameterReplacer(IList<ParameterExpression> fromParameters, IList<ParameterExpression> toParameters)
            {
                ParameterReplacements = new Dictionary<ParameterExpression, ParameterExpression>();

                for (int i = 0; i != fromParameters.Count && i != toParameters.Count; i++)
                {
                    ParameterReplacements.Add(fromParameters[i], toParameters[i]);
                }
            }
            
            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (ParameterReplacements.TryGetValue(node, out ParameterExpression replacement))
                {
                    node = replacement;
                }

                return base.VisitParameter(node);
            }
        }
    }
}
