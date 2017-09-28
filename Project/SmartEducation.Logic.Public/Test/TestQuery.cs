using System;
using System.Linq;
using Core.DataAccess;
using SmartEducation.Domain;

namespace SmartEducation.Logic.Public.Test
{
    public class TestQuery
    {
        IUnitOfWork _uow;

        public TestQuery(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public string Execute()
        {
            var repo = _uow.GetRepository<TestEntity>();

            if (repo.AsQueryable().Any() == false)
            {
                return "What a fuck. Collection is Empty EPTA. 0_o";
            }

            return repo.AsQueryable().First().Name;
        }
    }
}
