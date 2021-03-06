﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace Core.DataAccess.Filters
{
    /// <summary>
    /// Общией объекты для работы с фильтрацией 
    /// </summary>
    public static class FilteringCommonObjects
    {
        #region Constants

        public const string SortingSettingsObject = "SortingSettings";
        public const string PagingSettingsObject = "PageSettings";

        #endregion

        #region Enums

        /// <summary>
        /// Типы сортировки
        /// </summary>
        public enum SortingTypes
        {
            Asc,
            Desc
        }

        /// <summary>
        /// Типы операторов сравнения
        /// </summary>
        public enum ComparisonTypes
        {
            Equals,
            GreaterThen,
            LessThan,
            GreaterThenOrEqualTo,
            LessThanOrEqualTo,
            NotEqualTo
        }

        #endregion

        #region Public classes

        public class FilterSettings<T>
            where T : class
        {
            public FilterSettings()
            {
                SettingsFiltering = new List<ConditionFilter<T>>();
                SettingsSorting = new List<SortRequest<T>>();
            }

            public List<ConditionFilter<T>> SettingsFiltering { get; }
            public List<SortRequest<T>> SettingsSorting { get; }
            public PageRequest SettingsPage { get; private set; }


            public void FillFilteringObjects(string queryString)
            {
                if (string.IsNullOrEmpty(queryString)) return;

                //TODO: вероятно стоит использовать ковертацию из json объекта var ovject = JsonConvert.DeserializeObject(queryString);

                var parameters = queryString.Split("&");
                foreach (var parameter in parameters)
                {
                    var pName = parameter.Split("=")[0];
                    var pValue = parameter.Split("=")[1];

                    if (HasPropertyInObject<T>(pName))
                    {
                        AddSettingProcessing(null, new List<ConditionFilter<T>> { new ConditionFilter<T>(pName, pValue) }, null, null);
                    }
                    if (HasPropertyInObject<PageRequest>(pName))
                    {
                        AddSettingProcessing(null, null, null, new PageRequest(pName, pValue));
                    }
                }
            }            

            public void FillFilteringObjects(T type, List<ConditionFilter<T>> filteringSettings)
            {
                AddSettingProcessing(type, filteringSettings, null, null);
            }

            public void FillFilteringObjects(T type, List<ConditionFilter<T>> filteringSettings, List<SortRequest<T>> sortingSettings)
            {
                AddSettingProcessing(type, filteringSettings, sortingSettings, null);
            }

            public void FillFilteringObjects(T type, List<ConditionFilter<T>> filteringSettings, List<SortRequest<T>> sortingSettings, PageRequest pageSetting)
            {
                AddSettingProcessing(type, filteringSettings, sortingSettings, pageSetting);
            }

            private void AddSettingProcessing(T type, List<ConditionFilter<T>> filteringSettings, List<SortRequest<T>> sortingSettings, PageRequest pageSetting)
            {
                #region Обработка добавления элемента фильтрации

                Action<List<ConditionFilter<T>>> filterProcessing = (settings) =>
                {
                    if (settings != null && settings.Any())
                    {
                        SettingsFiltering.AddRange(settings);
                    }
                        
                };

                #endregion

                #region Обработка добавление элемента сортировки
                
                Action<List<SortRequest<T>>> sortProcessing = (settings) =>
                {
                    if (settings != null)
                        SettingsSorting.AddRange(settings);
                };

                #endregion

                #region Обработка добавления элемента пйджинга

                Action<PageRequest> pageProcessing = (setting) =>
                {
                    if (setting != null)
                        SettingsPage = setting;
                };

                #endregion

                #region Обработка добавления элемента фильтрации через объект  

                Action<T> typeProcessing = (t) =>
                {
                    if (t != null)
                    {
                        var properties = t.GetType()
                                            .GetProperties(System.Reflection.BindingFlags.Public)
                                            .Where(r => r.MemberType == System.Reflection.MemberTypes.Property);

                        foreach (var prop in properties)
                        {
                            filterProcessing(new List<ConditionFilter<T>> { new ConditionFilter<T>(prop.Name, prop.GetValue(t)) });
                        }
                    }
                };

                #endregion

                typeProcessing(type);
                filterProcessing(filteringSettings);
                sortProcessing(sortingSettings);
                pageProcessing(pageSetting);
            }
        }

        public class ConditionFilter<T>
            where T : class
        {
            public ConditionFilter(string propertyName, object comparisonObject, ComparisonTypes comparisonType = ComparisonTypes.Equals)
            {
                if (!HasPropertyInObject<T>(propertyName))
                    throw new ArgumentException($"Свойство с именем {propertyName} не пренаделжит объекту {typeof(T).FullName}");

                PropertyName = propertyName;
                ComparisonObject = comparisonObject;
                ComparisonType = comparisonType;
            }

            public string PropertyName { get; private set; }
            public object ComparisonObject { get; private set; }
            public ComparisonTypes ComparisonType { get; private set; }
        }

        public class SortRequest<T>
            where T : class
        {
            public SortRequest(string propertyName, SortingTypes sortingOperationType)
            {
                if (!HasPropertyInObject<T>(propertyName))
                    throw new ArgumentException($"Свойство с именем {propertyName} не пренаделжит объекту {typeof(T).FullName}");

                PropertyName = propertyName;
                SortingType = sortingOperationType;
            }
            public string PropertyName { get; private set; }
            public SortingTypes SortingType { get; private set; }
        }

        public class PageRequest
        {
            public PageRequest(string pageNumber, string rowCountPerPage)
            {
                PageNumber = int.Parse(pageNumber);
                RowCountPerPage = int.Parse(rowCountPerPage);
            }
            public PageRequest(int pageNumber, int rowCountPerPage)
            {
                PageNumber = pageNumber;
                RowCountPerPage = rowCountPerPage;
            }

            public int PageNumber { get; private set; }
            public int RowCountPerPage { get; private set; }
        }

        #endregion

        #region Methods

        private static bool HasPropertyInObject<T>(string propertyName)
            where T : class
        {
            return typeof(T).GetProperty(propertyName) != null;
        }

        #endregion
    }
}
