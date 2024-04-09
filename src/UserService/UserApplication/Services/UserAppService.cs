using UserApplication.Abstractions.AppServices;
using UserApplication.Abstractions.Repositories;
using UserDomain.Entities;

namespace UserApplication.Services
{
	public class UserAppService : IUserAppService
	{
		private readonly IUserRepository _repository;

		public UserAppService(IUserRepository repository)
		{
			_repository = repository;
		}

		public async Task<User?> Add(User user, CancellationToken cancellationToken)
		{
			User? requestedUser = await _repository.GetByEmail(user.Email!, cancellationToken);

			if (requestedUser is not null)
			{
				return null;
			}

			return await _repository.Add(user, cancellationToken);
		}

		public async Task<User?> Get(Guid id, CancellationToken cancellationToken)
		{
			return await _repository.Get(id, cancellationToken);
		}

		public async Task<List<User>> Get(CancellationToken cancellationToken)
		{
			return await _repository.Get(cancellationToken);
		}

		public async Task<User?> Update(Guid userId, User user, CancellationToken cancellationToken)
		{
			return await _repository.Update(userId, user, cancellationToken);
		}

		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			return await _repository.Delete(id, cancellationToken);
		}
	}
}
