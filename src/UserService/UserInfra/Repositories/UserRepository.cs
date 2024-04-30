using Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserApplication.Abstractions.Repositories;
using UserDomain.Context;
using UserDomain.Entities;

namespace UserInfra.Repositories
{
	public sealed class UserRepository : IUserRepository
	{
		private readonly UserContext _context;
		private readonly IPublishEndpoint _publishEndpoint;
		private readonly UserManager<IdentityUser> _userManager;

		public UserRepository(UserContext context,
			IPublishEndpoint publishEndpoint,
			UserManager<IdentityUser> userManager)
		{
			_context = context;
			_publishEndpoint = publishEndpoint;
			_userManager = userManager;
		}

		public async Task<IdentityUser?> Get(Guid id)
		{
			IdentityUser? user = await _userManager.FindByIdAsync(id.ToString());
			return user;
		}

		public async Task<List<IdentityUser>> Get()
		{
			List<IdentityUser> users = await _userManager.Users.ToListAsync();
			return users;
		}
		public async Task<bool> Delete(Guid id )
		{
			IdentityUser? user = await _userManager.FindByIdAsync(id.ToString())
				?? throw new Exception("Ocorreu um erro ao deletar o usuário: Usuário não encontrado");

			IdentityResult result = await _userManager.DeleteAsync(user);
			return result.Succeeded;
		}

		public async Task<IdentityUser?> GetByEmail(string email)
		{
			IdentityUser? user = await _userManager.FindByEmailAsync(email) 
			?? throw new Exception("Usuário não foi encontrado");
			
			return user;
		}
	}
}
