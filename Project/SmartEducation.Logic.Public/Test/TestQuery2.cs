using System;
using System.Linq;
using Core.DataAccess;
using SmartEducation.Domain;

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

            repo.AddFilterByQueryParameters(new System.Collections.Generic.Dictionary<string, object>
            {
                { "Name", "name1" },
                { "Id", 1 },
                //{"Date", DateTime.Now },
                {"IsDeleted",false }

            });

            return repo.AsQueryable().Skip(1).First().Name;
        }
    }
}
