namespace TreinoApi.Dtos.Requests
{
	public sealed class TreinoRequest
	{
		public Guid Id { get; set; }
		public string? Nome { get; set; }
		public List<ExercicioRequest>? Exercicios { get; set; }
	}
}
