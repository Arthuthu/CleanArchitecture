namespace TreinoApi.Dtos.Requests
{
	public sealed class ExercicioRequest
	{
		public Guid Id { get; set; }
		public string? Nome { get; set; }
		public decimal Carga { get; set; }
		public int Serie { get; set; }
		public int Repeticoes { get; set; }
	}
}
