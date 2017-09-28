using Core.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace SmartEducation.Domain
{
    public class TestContext: BaseDbContext
    {
		public TestContext(DbContextOptions<TestContext> options)
            :base(options)
        {
        }

        public DbSet<TestEntity> TestEnt { get; set; }
    }
}
