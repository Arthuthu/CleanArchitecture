﻿namespace UserApi.Dtos.Authentication
{
	public sealed class UserToken
	{
		public string? Token { get; set; }
		public string? Id { get; set; }
		public DateTime Expiration { get; set; }
	}
}
