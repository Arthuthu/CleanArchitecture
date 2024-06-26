﻿using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserApplication.Abstractions.Repositories;

namespace UserInfra.Repositories
{
	public sealed class UserRepository : IUserRepository
	{
		private readonly IPublishEndpoint _publishEndpoint;
		private readonly UserManager<IdentityUser> _userManager;

		public UserRepository(IPublishEndpoint publishEndpoint, UserManager<IdentityUser> userManager)
		{
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
			?? throw new Exception("Usuário não foi encontrado por email");
			
			return user;
		}

		public async Task<IdentityUser?> GetByUsername(string username)
		{
			IdentityUser? user = await _userManager.FindByNameAsync(username)
			?? throw new Exception("Usuário não foi encontrado por nome");

			return user;
		}
	}
}
