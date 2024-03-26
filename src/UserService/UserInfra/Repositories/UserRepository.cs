using Contracts;
using MassTransit;
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

		public UserRepository(UserContext context, IPublishEndpoint publishEndpoint)
		{
			_context = context;
			_publishEndpoint = publishEndpoint;
		}

		public async Task<User> Add(User user, CancellationToken cancellationToken)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync(cancellationToken);


			//await _publishEndpoint.Publish(new SubscriptionEvent
			//{
			//	Name = "User Created",
			//	MonthlyPrice = 50
			//}, cancellationToken);

			return user;
		}

		public async Task<User?> Get(Guid id, CancellationToken cancellationToken)
		{
			User? user = await _context.Users.FindAsync(id, cancellationToken);
			return user;
		}

		public async Task<List<User>> Get(CancellationToken cancellationToken)
		{
			List<User> users = await _context.Users.ToListAsync(cancellationToken);
			return users;
		}

		public async Task<User?> Update(User user, CancellationToken cancellationToken)
		{
			User? requestedUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == user.Id, cancellationToken);

			if (requestedUser is null)
			{
				return null;
			}

			requestedUser.Name = user.Name;
			requestedUser.Email = user.Email;

			_context.Users.Update(requestedUser);
			await _context.SaveChangesAsync(cancellationToken);

			return user;
		}

		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			int affectedRows = await _context.Users.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
			return affectedRows > 0;
		}

		public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
		{
			User? requestedUser = await _context.Users.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);
			return requestedUser;
		}
	}
}
