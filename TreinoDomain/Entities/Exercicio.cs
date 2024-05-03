using UserDomain.Primitives;

namespace TreinoDomain.Entities
{
	public sealed class Exercicio : Entity
	{
		public string? Nome { get; set; }
		public decimal Carga { get; set; }
		public int Serie { get; set; }
		public int Repeticoes { get; set; }

		public Treino? Treino { get; set; }
	}
}
