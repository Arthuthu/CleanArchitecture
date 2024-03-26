using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionDomain.Entities;

namespace SubscriptionInfra.Mapping
{
	public sealed class SubscriptionMapping : BaseMapping<Subscription>
	{
        public SubscriptionMapping() : base("Subscriptions")
        {
        }

		public override void Configure(EntityTypeBuilder<Subscription> builder)
		{
			base.Configure(builder);

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Name).HasMaxLength(40);
			builder.Property(x => x.MonthlyPrice).HasPrecision(15, 2);
		}
	}
}
