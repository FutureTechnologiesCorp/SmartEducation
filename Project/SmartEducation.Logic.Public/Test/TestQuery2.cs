using System;
using System.Linq;
using Core.DataAccess;
using SmartEducation.Domain;
using Core.Common;
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
            var result = repo.GetById(1);

            if (repo.AsQueryable().Any() == false)
            {
                return "What a fuck. Collection is Empty EPTA. 0_o";
            }

            repo.ApplyFilterByQueryParameters(new Dictionary<string, object>
            {
                //{ "Name", "name1" },
                //{ "Id", "1" },
                { "Date", null },
                //{ "IsDeleted", false },
                { CommonValues.SortingObject, new List<CommonValues.SortingData>
                            {
                                new CommonValues.SortingData
                                {
                                    PropertyName = "Name",
                                    SortingOperationType = CommonValues.SortingOperationTypes.Asc
                                },
                                new CommonValues.SortingData
                                {
                                    PropertyName = "IsDeleted",
                                    SortingOperationType = CommonValues.SortingOperationTypes.Desc
                                }
                            }
                }
            });

            return repo.AsQueryable().Skip(1).First().Name;
        }
    }
}
