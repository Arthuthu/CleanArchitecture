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
		private readonly IPublishEndpoint _publishEndpoint;
		private readonly UserContext _context;

		public UserRepository(IPublishEndpoint publishEndpoint, UserContext context)
		{
			_publishEndpoint = publishEndpoint;
			_context = context;
		}

		public async Task<User?> Get(Guid id)
		{
			User? user = await _context.User.FindAsync(id);
			return user;
		}

		public async Task<List<User>> Get()
		{
			List<User> users = await _context.User.ToListAsync();
			return users;
		}
		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			User? user = await _context.User.FindAsync(id)
				?? throw new Exception("Ocorreu um erro ao deletar o usuário: Usuário não encontrado");

			int affectedRows = await _context.User.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
			return affectedRows > 0;
		}

		public async Task<User?> GetByEmail(string email)
		{
			User? user = await _context.User.Where(x => x.Email == email).FirstOrDefaultAsync()
			?? throw new Exception("Usuário não foi encontrado por email");

			return user;
		}

		public async Task<User?> GetByUsername(string username)
		{
			User? user = await _context.User.Where(x => x.Username == username).FirstOrDefaultAsync()
			?? throw new Exception("Usuário não foi encontrado por nome");

			return user;
		}
	}
}
