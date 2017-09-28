using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess
{
	public class BaseDbContext : DbContext
	{
		public BaseDbContext(DbContextOptions options)
			: base(options)
		{

		}
	}
}
