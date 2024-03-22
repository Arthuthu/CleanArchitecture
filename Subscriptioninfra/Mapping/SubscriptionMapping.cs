using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SubscriptionDomain.Entities;
using SubscriptionDomain.Enums;
using SubscriptionInfra.Mapping;

namespace SubscriptionInfra.Mapping
{
	public class SubscriptionMapping : BaseMapping<Subscription>
	{
        public SubscriptionMapping() : base("Subscriptions")
        {
        }

		public override void Configure(EntityTypeBuilder<Subscription> builder)
		{
			base.Configure(builder);

			builder.HasKey(x => x.Id);

			builder.Property(x => x.MonthlyPrice).HasPrecision(15, 2);
			builder.Property(x => x.Type).HasMaxLength(40).HasConversion(new EnumToStringConverter<SubscriptionType>());
		}
	}
}
