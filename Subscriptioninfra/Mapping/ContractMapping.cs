using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionDomain.Entities;

namespace SubscriptionInfra.Mapping
{
	public sealed class ContractMapping : BaseMapping<Contract>
	{
		public ContractMapping() : base("Contracts")
		{
		}

		public override void Configure(EntityTypeBuilder<Contract> builder)
		{
			base.Configure(builder);

			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.Subscription)
				.WithOne(x => x.Contract)
				.HasForeignKey<Contract>(x => x.SubscriptionId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
