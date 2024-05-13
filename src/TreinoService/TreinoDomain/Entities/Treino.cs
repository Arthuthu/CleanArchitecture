using UserDomain.Primitives;

namespace TreinoDomain.Entities
{
	public sealed class Treino : Entity
	{
		public Guid UserId { get; set; }
		public string? Nome { get; set; }
		public List<Exercicio>? Exercicios { get; set; }
	}
}
