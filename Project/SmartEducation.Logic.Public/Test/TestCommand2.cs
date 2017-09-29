using System;
using System.Linq;
using Core.DataAccess;
using Core.CQRS;
using SmartEducation.Domain;

namespace SmartEducation.Logic.Public.Test
{
    public class TestCommand2: BaseCommand
    {
		IUnitOfWork _uow;

		public TestCommand2(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public void Execute()
		{
			var repo = _uow.GetRepository<TestEntity>();

            var ent = new TestEntity
            {
                Name = "3 Name"
            };

            repo.Create(ent);
            _uow.SaveChanges();
		}
    }
}
