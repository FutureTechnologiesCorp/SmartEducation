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

            var filterSettings = new FilterSettings<TestEntity>();

            filterSettings.FillFilteringObjects(null,
            new List<ConditionFilter<TestEntity>>
            {
                //new ConditionFilter<TestEntity>("Date",  new System.DateTime(2017, 3, 10), ComparisonTypes.LessThanOrEqualTo),
                //new ConditionFilter<TestEntity>("IsDeleted", true, ComparisonTypes.NotEqualTo),
                new ConditionFilter<TestEntity>("IsDeleted", true, ComparisonTypes.NotEqualTo),
            },
            new List<SortRequest<TestEntity>> {
                new SortRequest<TestEntity>("Name",SortingTypes.Desc)
            },
            new PageRequest(1, 25)
            );

            var result = repo.ApplyFilterByFillteringSettings(filterSettings);

            return string.Join(",", result.Select(r => new { r.Id, r.Name, r.Date, r.IsDeleted }).ToList());
        }
    }
}
