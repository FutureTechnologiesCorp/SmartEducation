namespace Core.DataAccess.Filters
{
    public static class FilteringCommonObjects
    {
        #region Обюъекты сортировки

        public const string SortingSettingsObject = "SortingSettings";
        public const string PagingSettingsObject = "PageSettings";

        public enum SortingTypes
        {
            Asc,
            Desc
        }

        public class SortingSetting
        {
            public string PropertyName { get; set; }
            public SortingTypes SortingOperationType { get; set; }
        }

        public class PageSetting
        {
            public int PageNumber { get; set; }
            public int RowCountPerPage { get; set; }
        }

        #endregion

    }
}
