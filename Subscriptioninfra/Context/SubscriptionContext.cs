using Microsoft.EntityFrameworkCore;
using SubscriptionDomain.Entities;

namespace SubscriptionInfra.Context
{
	public class SubscriptionContext : DbContext
	{
        public SubscriptionContext(DbContextOptions<SubscriptionContext> options) : base(options)
        {
        }

        public DbSet<Subscription> Subscription { get; set; }
		public DbSet<Contract> Contract { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		}
	}
}
