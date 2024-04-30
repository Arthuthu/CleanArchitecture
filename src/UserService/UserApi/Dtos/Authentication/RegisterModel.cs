using System.ComponentModel.DataAnnotations;

namespace UserApi.Dtos.Authentication
{
	public sealed class RegisterModel
	{
		public string? Username { get; set; }

		[Required]
		[EmailAddress]
		public string? Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "Passwords does not match")]
		public string? ConfirmPassword { get; set; }
	}
}
