using System.Linq;
using Core.DataAccess;
using Core.DataAccess.Filters;
using SmartEducation.Domain;
using System.Collections.Generic;

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

           var result = repo.ApplyFilterByQueryParameters(new Dictionary<string, object>
            {
                //{ "Name", "name1" },
                //{ "Id", "1" },
               // { "Date", null },
                //{ "IsDeleted", false },
                { FilteringCommonObjects.SortingObject, new List<FilteringCommonObjects.SortingSetting>
                            {
                                new FilteringCommonObjects.SortingSetting
                                {
                                    PropertyName = "Name",
                                    SortingOperationType = FilteringCommonObjects.SortingTypes.Asc
                                },
                                new FilteringCommonObjects.SortingSetting
                                {
                                    PropertyName = "IsDeleted",
                                    SortingOperationType = FilteringCommonObjects.SortingTypes.Desc
                                },
                                new FilteringCommonObjects.SortingSetting
                                {
                                    PropertyName = "Date",
                                    SortingOperationType = FilteringCommonObjects.SortingTypes.Desc
                                }
                            }
                }
            });

            return string.Join(",", result.Select(r => new { r.Id, r.Name, r.Date, r.IsDeleted }).ToList());
        }
    }
}
