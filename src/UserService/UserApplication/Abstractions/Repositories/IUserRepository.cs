using Microsoft.AspNetCore.Identity;

namespace UserApplication.Abstractions.Repositories
{
	public interface IUserRepository
	{
		Task<IdentityUser?> Get(Guid id);
		Task<List<IdentityUser>> Get();
		Task<bool> Delete(Guid id);
		Task<IdentityUser?> GetByEmail(string email);
		Task<IdentityUser?> GetByUsername(string username);
	}
}
