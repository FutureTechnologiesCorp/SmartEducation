using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common
{
    public static class CommonValues
    {
        #region Обюъекты сортировки

        public const string SortingObject = "Sorting";

        public enum SortingOperationTypes
        {
            Asc,
            Desc
        }

        public class SortingData
        {
            public string PropertyName { get; set; }
            public SortingOperationTypes SortingOperationType { get; set; }
        }

        #endregion

    }
}
