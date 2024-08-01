using AtendimentoDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AtendimentoInfra.Context
{
    public sealed class AtendimentoContext : DbContext
    {
        public AtendimentoContext(DbContextOptions<AtendimentoContext> options) : base(options)
        {
        }

        public DbSet<Atendimento> Atendimento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
