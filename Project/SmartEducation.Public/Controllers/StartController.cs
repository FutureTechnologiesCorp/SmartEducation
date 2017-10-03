using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SmartEducation.Domain;
using Core.CQRS;
using SmartEducation.Logic.Public.Test;
using System.Linq;

namespace SmartEducation.Public.Controllers
{
    [Route("api/[controller]")]
    public class StartController
    {
        private TestContext _context;
        private IExecutor _execitor;
        
        public StartController(TestContext context, IExecutor executor)
        {
            _context = context;
            _execitor = executor;
        }

        [HttpGet]
        [Route("test")]
        public string Test()
        {
            var testEnt = new TestEntity()
            {
                Id = 1,
                Name = "Test2"
            };

            if(_context.TestEnt.Find(1) != null)
            {
                return "Already exist";
            }

            _context.TestEnt.Add(testEnt);
            _context.SaveChanges();


            return _context.TestEnt.Find(1).Name;
        }

        [HttpGet]
        [Route("testQuery")]
        public string TestQuery()
        {
            var queryExecutor = _execitor.GetQuery<TestQuery>();
            return queryExecutor.Process(q => q.Execute()); 
        }

        [HttpGet]
        [Route("testChain")]
        public string TestChain()
        {
            var chainExecutor = _execitor.CommandChain();
            chainExecutor
                .AddCommand<TestCommand>(c=>c.Execute())
                .AddCommand<TestCommand2>(c=>c.Execute())
                .ExecuteAll();

            return _execitor.GetQuery<TestQuery2>().Process(q=>q.Execute());
        }

        [HttpGet]
        public IEnumerable<string> Index()
        {
            return new string[] { "A", "N", "G", "U", "L", "A", "R", " 2" };
        }


        [HttpGet]
        [Route("TestFilter")]
        public string TestFilter()
        {
            var testEnt = new TestEntity() { Name = "name3", IsDeleted = false };
            var testEnt1 = new TestEntity() { Name = "name3", IsDeleted = true };
            var testEnt2 = new TestEntity() { Name = "name4", Date = new System.DateTime(2017, 3, 10) };
            var testEnt3 = new TestEntity() { Name = "name5", Date = new System.DateTime(2017, 3, 11) };

            _context.TestEnt.AddRange(new List<TestEntity> { testEnt, testEnt1, testEnt2, testEnt3 });
            _context.SaveChanges();

            var queryExecutor = _execitor.GetQuery<TestQuery2>();
            return queryExecutor.Process(q => q.Execute());
        }
    }
}
