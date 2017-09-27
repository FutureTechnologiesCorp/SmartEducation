using Microsoft.EntityFrameworkCore;

namespace FT.DataAccess
{
	public class BaseDbContext : DbContext
	{
		public BaseDbContext(DbContextOptions options)
			: base(options)
		{

		}
	}
}
