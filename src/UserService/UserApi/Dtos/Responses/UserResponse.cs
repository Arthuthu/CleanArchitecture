namespace UserApi.Dtos.Responses
{
	public sealed class UserResponse
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
	}
}
