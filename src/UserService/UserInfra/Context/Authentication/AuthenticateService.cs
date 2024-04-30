
using Microsoft.AspNetCore.Identity;

namespace UserInfra.Context.Authentication
{
	public sealed class AuthenticateService : IAuthenticate
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;

		public AuthenticateService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public async Task<bool> AuthenticateAsync(string username, string password)
		{
			var result = await _signInManager.PasswordSignInAsync(username, password,
				false, lockoutOnFailure: false);

			return result.Succeeded;
		}

		public async Task Logout()
		{
			await _signInManager.SignOutAsync();
		}

		public async Task<string> RegisterUser(string email, string password, string username)
		{
			IdentityUser appUser = new()
			{ 
				UserName = username,
				Email = email
			};

			var result = await _userManager.CreateAsync(appUser, password);

			if (result.Errors.Any())
			{
				return result.Errors.FirstOrDefault()!.Description;
			}

			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(appUser, isPersistent: false);
			}

			return "Usuário registrado";
		}
	}
}
