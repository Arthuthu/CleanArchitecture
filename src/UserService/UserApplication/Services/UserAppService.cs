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

		public async Task<User?> Get(Guid id)
		{
			return await _repository.Get(id);
		}

		public async Task<List<User>> Get()
		{
			return await _repository.Get();
		}

		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			return await _repository.Delete(id, cancellationToken);
		}

		public async Task<User?> GetByEmail(string email)
		{
			User? requestedUser = await _repository.GetByEmail(email);
			return requestedUser;
		}

		public async Task<User?> GetByUsername(string username)
		{
			User? requestedUser = await _repository.GetByUsername(username);
			return requestedUser;
		}
	}
}
