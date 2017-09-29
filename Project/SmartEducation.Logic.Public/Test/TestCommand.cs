using System;
using System.Linq;
using Core.CQRS;
using Core.DataAccess;
using SmartEducation.Domain;

namespace SmartEducation.Logic.Public.Test
{
    public class TestCommand: BaseCommand
    {
        
		IUnitOfWork _uow;

		public TestCommand(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public void Execute()
		{
			var repo = _uow.GetRepository<TestEntity>();

            var ent = new TestEntity
            {
                Name = "2 Name"
            };

            repo.Create(ent);
            _uow.SaveChanges();
		}
    }
}
