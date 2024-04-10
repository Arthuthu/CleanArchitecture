using System.ComponentModel.DataAnnotations;

namespace UserApi.Dtos.Authentication
{
	public sealed class LoginModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email format is invalid")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[StringLength(20, ErrorMessage = "The {0} needs to be at least {2} and maximum {1} length",
			MinimumLength = 10)]
		public string? Password { get; set; }
	}
}
