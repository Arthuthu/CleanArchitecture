using System.ComponentModel.DataAnnotations;

namespace UserApi.Dtos.Authentication
{
	public sealed class LoginModel
	{
		[Required(ErrorMessage = "Usuario não informado")]
		public string? Username { get; set; }

		[Required(ErrorMessage = "Senha não informada")]
		public string? Password { get; set; }
	}
}
