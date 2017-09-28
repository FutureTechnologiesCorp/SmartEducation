using Core.CQRS;
using Core.DataAccess;
using Xunit;

namespace FT.DataAccess.Tests
{
    public class CQRSTests
    {
        readonly BaseDbContext _context = new BaseDbContext(null);

        [Fact]
        public void QuerExecutor_Create_Test()
        {
            var executor = new Executor(_context, null);
            var queryExecutor = executor.GetQuery<TestQuery>();
            var queryResult = queryExecutor.Process(q=>q.Execute());
            Assert.NotNull(queryResult);
        }

        [Fact]
        public void CommandExecutor_Create_Test()
        {
            var executor = new Executor(_context, null);
            var commandExecutor = executor.GetCommand<TestCommand>();
            commandExecutor.Process(c=>c.Execute());
        }

        [Fact]
        public void SagaExecutor_Test()
        {
            var executor = new Executor(_context, null);
            executor.CommandChain()
                .AddCommand<TestCommand>(c=>c.Execute())
                .AddCommand<TestCommand2>(c2=>c2.Execute2())
                   .ExecuteAllWithTransaction();
        }
    }

    public class TestQuery
    {
        public TestQuery Execute()
        {
            return new TestQuery();
        }
    }

    public class TestCommand: BaseCommand
    {
        public void Execute()
        {
            
        }
    }

	public class TestCommand2 : BaseCommand
	{
		public void Execute2()
		{

		}
	}
}
