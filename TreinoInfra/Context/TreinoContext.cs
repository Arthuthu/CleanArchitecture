using Microsoft.EntityFrameworkCore;
using TreinoDomain.Entities;

namespace TreinoInfra.Context
{
	public sealed class TreinoContext : DbContext
	{
		public TreinoContext(DbContextOptions<TreinoContext> options) : base(options)
		{
		}

		public DbSet<Treino> Treinos { get; set; }
		public DbSet<Exercicio> Exercicios { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		}
	}
}
