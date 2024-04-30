using Microsoft.AspNetCore.Identity;
using UserApplication.Abstractions.AppServices;
using UserApplication.Abstractions.Repositories;

namespace UserApplication.Services
{
	public class UserAppService : IUserAppService
	{
		private readonly IUserRepository _repository;

		public UserAppService(IUserRepository repository)
		{
			_repository = repository;
		}

		public async Task<IdentityUser?> Get(Guid id)
		{
			return await _repository.Get(id);
		}

		public async Task<List<IdentityUser>> Get()
		{
			return await _repository.Get();
		}

		public async Task<bool> Delete(Guid id)
		{
			return await _repository.Delete(id);
		}

		public async Task<IdentityUser?> GetByEmail(string email)
		{
			IdentityUser? requestedUser = await _repository.GetByEmail(email);
			return requestedUser;
		}
	}
}
