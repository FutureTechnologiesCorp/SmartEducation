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

            return repo.AsQueryable().Skip(1).First().Name;
        }
    }
}
