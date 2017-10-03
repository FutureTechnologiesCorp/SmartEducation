namespace Core.DataAccess.Filters
{
    public static class FilteringCommonObjects
    {
        #region Обюъекты сортировки

        public const string SortingObject = "Sorting";

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

        #endregion

    }
}
