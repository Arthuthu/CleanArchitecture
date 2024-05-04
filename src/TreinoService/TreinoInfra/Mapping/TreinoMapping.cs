using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionInfra.Mapping;
using TreinoDomain.Entities;

namespace TreinoInfra.Mapping
{
	public sealed class TreinoMapping : BaseMapping<Treino>
	{
        public TreinoMapping() : base("Treinos")
        {
        }

		public override void Configure(EntityTypeBuilder<Treino> builder)
		{
			base.Configure(builder);

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Nome).HasMaxLength(50);
			builder.HasMany(x => x.Exercicios).WithOne(x => x.Treino)
				.HasForeignKey(x => x.TreinoId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
