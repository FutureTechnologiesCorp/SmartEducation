using FT.DataAccess.CQRS;
using NUnit.Framework;

namespace FT.DataAccess.Tests
{
    [TestFixture]
    public class CQRSTests
    {
        [Test]
        public void QuerExecutor_Create_Test()
        {
            var executor = new Executor();
            var queryExecutor = executor.GetQuery<TestQuery>();
            var queryResult = queryExecutor.Process(q=>q.Execute());
            Assert.IsNotNull(queryResult);
        }

        [Test]
        public void CommandExecutor_Create_Test()
        {
            var executor = new Executor();
            var commandExecutor = executor.GetCommand<TestCommand>();
            commandExecutor.Process(c=>c.Execute());
        }
    }

    public class TestQuery
    {
        public TestQuery Execute()
        {
            return new TestQuery();
        }
    }

    public class TestCommand
    {
        public void Execute()
        {
            
        }
    }
}
