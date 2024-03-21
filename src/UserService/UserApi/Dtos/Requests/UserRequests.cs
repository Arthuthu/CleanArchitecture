namespace UserApi.Dtos.Requests
{
	public sealed class UserRequest
	{
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
	}
}
