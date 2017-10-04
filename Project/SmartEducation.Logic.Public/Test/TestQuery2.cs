using System.Linq;
using Core.DataAccess;
using SmartEducation.Domain;
using System.Collections.Generic;

using static Core.DataAccess.Filters.FilteringCommonObjects;

namespace SmartEducation.Logic.Public.Test
{
    public class TestQuery2
    {
        IUnitOfWork _uow;

        public TestQuery2(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public string Execute()
        {
            var repo = _uow.GetRepository<TestEntity>();
            //var result = repo.GetById(1);

            if (repo.AsQueryable().Any() == false)
            {
                return "What a fuck. Collection is Empty EPTA. 0_o";
            }

            /*
            var result = repo.ApplyFilterByQueryParameters(new Dictionary<string, object>
            {
                //{ "Name", "name1" },
                //{ "Id", "1" },
               // { "Date", null },
                //{ "IsDeleted", false },
                { FilteringCommonObjects.SortingSettingsObject, new List<FilteringCommonObjects.SortingSetting<TestEntity>>
                    {
                    new FilteringCommonObjects.SortingSetting<TestEntity>("Name",FilteringCommonObjects.SortingTypes.Asc),
                    new FilteringCommonObjects.SortingSetting<TestEntity>("IsDeleted",FilteringCommonObjects.SortingTypes.Desc),
                    new FilteringCommonObjects.SortingSetting<TestEntity>("Date",FilteringCommonObjects.SortingTypes.Desc)
                    }
                }
            });*/

            var filterSettings = new FilterSettings<TestEntity>();

            filterSettings.FillFilteringObjects(null, new List<FilteringSetting<TestEntity>>
            {
                new FilteringSetting<TestEntity>("",1,ComparisonTypes.Equals)
            });

            var result = repo.ApplyFilterByFillteringSettings(filterSettings);

            return string.Join(",", result.Select(r => new { r.Id, r.Name, r.Date, r.IsDeleted }).ToList());
        }
    }
}
