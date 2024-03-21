using Microsoft.EntityFrameworkCore;
using UserApplication.Abstractions.Repositories;
using UserDomain.Context;
using UserDomain.Entities;

namespace UserInfra.Repositories
{
	public sealed class UserRepository : IUserRepository
	{
		private readonly UserContext _context;

		public UserRepository(UserContext context)
		{
			_context = context;
		}

		public async Task<User> Add(User user, CancellationToken cancellationToken)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync(cancellationToken);

			return user;
		}

		public async Task<User?> Get(Guid id, CancellationToken cancellationToken)
		{
			User? result = await _context.Users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
			return result;
		}

		public async Task<List<User>> Get(CancellationToken cancellationToken)
		{
			List<User> result = await _context.Users.ToListAsync(cancellationToken);
			return result;
		}

		public async Task Update(User user, CancellationToken cancellationToken)
		{
			User? requestedUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == user.Id, cancellationToken);

			if (requestedUser is null)
			{
				return;
			}

			requestedUser.Name = user.Name;
			requestedUser.Email = user.Email;

			_context.Users.Update(requestedUser);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			int result = await _context.Users.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
			return result > 0;
		}

		public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
		{
			User? requestedUser = await _context.Users.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);
			return requestedUser;
		}
	}
}
