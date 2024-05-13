using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Dtos.Authentication;
using UserApplication.Abstractions.AppServices;
using UserDomain.Entities;
using UserInfra.Context.Authentication;

namespace UserApi.Controllers.v1
{
	public class AccountController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly IAuthenticate _authentication;
		private readonly IUserAppService _userAppService;

		public AccountController(IConfiguration configuration, IAuthenticate authentication)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
		}

		[HttpPost("v1/register")]
		[ProducesResponseType (200)]
		[ProducesResponseType (400)]
		[AllowAnonymous]
		public async Task<ActionResult<UserToken>> Register([FromBody] RegisterModel model)
		{
			if (model.Password != model.ConfirmPassword)
			{
				ModelState.AddModelError("ConfirmPassword", "As senhas não conferem");
				return BadRequest(ModelState);
			}

			if (string.IsNullOrEmpty(model.Email) && string.IsNullOrWhiteSpace(model.Password))
			{
				return BadRequest("O campo email ou senha não foram preenchidos");
			}

			string response = await _authentication.RegisterUser(model.Email!, model.Password!, model.Username!);
			
			if (response == "Usuário registrado")
			{
				return Ok($"Usuário {model.Email} foi criado com sucesso");
			}

			ModelState.AddModelError("CreateUser", response);
			return BadRequest(ModelState);
		}

		[HttpPost("v1/login")]
		[AllowAnonymous]
		public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
		{
			if (string.IsNullOrEmpty(userInfo.Username) && string.IsNullOrWhiteSpace(userInfo.Password))
			{
				return NotFound("O campo usuário ou senha não foram preenchidos");
			}

			bool result = await _authentication.AuthenticateAsync(userInfo.Username!, userInfo.Password!);

			if (result)
			{
				return GenerateToken(userInfo);
			}

			ModelState.AddModelError("LoginUser", "Login inválido");
			return NotFound(ModelState);
		}

		private ActionResult<UserToken> GenerateToken(LoginModel userInfo)
		{
			Claim[] claims =
			[
				new Claim("meuToken", "token"),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			];

			string? appSettingsKeyValue = _configuration["Jwt:key"];
			if (string.IsNullOrEmpty(appSettingsKeyValue) || string.IsNullOrWhiteSpace(appSettingsKeyValue))
			{
				throw new Exception("The JWT key was not found");
			}

			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(appSettingsKeyValue));
			SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
			DateTime tokenExpiration = DateTime.UtcNow.AddDays(1);

			JwtSecurityToken token = new(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: tokenExpiration,
				signingCredentials: credentials);

			return new UserToken()
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				Expiration = tokenExpiration
			};
		}
	}
}
