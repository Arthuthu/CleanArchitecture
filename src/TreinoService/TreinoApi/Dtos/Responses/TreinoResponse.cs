namespace TreinoApi.Dtos.Responses
{
    public class TreinoResponse
    {
		public string? Nome { get; set; }
		public List<ExercicioResponse>? Exercicios { get; set; }
	}
}
