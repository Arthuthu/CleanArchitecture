namespace UserApi.Dtos.Responses
{
	public sealed class UserResponse
	{
		public Guid Id { get; set; }
		public string? Username { get; set; }
		public string? Email { get; set; }
	}
}
