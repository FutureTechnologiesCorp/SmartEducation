using FT.DataAccess.CQRS;
using Xunit;

namespace FT.DataAccess.Tests
{
    public class CQRSTests
    {
        [Fact]
        public void QuerExecutor_Create_Test()
        {
            var executor = new Executor();
            var queryExecutor = executor.GetQuery<TestQuery>();
            var queryResult = queryExecutor.Process(q=>q.Execute());
            Assert.NotNull(queryResult);
        }

        [Fact]
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
