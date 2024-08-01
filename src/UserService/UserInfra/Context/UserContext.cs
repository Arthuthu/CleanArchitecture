using Microsoft.EntityFrameworkCore;
using UserDomain.Entities;

namespace UserDomain.Context
{
    public sealed class UserContext : DbContext
	{
		public UserContext(DbContextOptions<UserContext> options) : base(options)
		{
		}

		public DbSet<User> User { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		}
	}
}
