using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionInfra.Mapping;
using TreinoDomain.Entities;

namespace TreinoInfra.Mapping
{
	public sealed class ExercicioMapping : BaseMapping<Exercicio>
	{
        public ExercicioMapping() : base("Exercicios")
        {
        }

		public override void Configure(EntityTypeBuilder<Exercicio> builder)
		{
			base.Configure(builder);

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Nome).HasMaxLength(256);
			builder.Property(x => x.Carga).HasPrecision(10, 3);
			builder.Property(x => x.Serie);
			builder.Property(x => x.Repeticoes);
		}
	}
}
