using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SmartEducation.Domain;
using FT.CQRS;
using SmartEducation.Logic.Public.Test;

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
        public IEnumerable<string> Index()
        {
            return new string[] { "A", "N", "G", "U", "L", "A", "R", " 2" };
        }
    }
}
