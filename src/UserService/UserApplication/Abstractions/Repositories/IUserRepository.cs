using Microsoft.AspNetCore.Identity;

namespace UserApplication.Abstractions.Repositories
{
	public interface IUserRepository
	{
		Task<IdentityUser?> Get(Guid id);
		Task<List<IdentityUser>> Get();
		Task<IdentityUser?> Update(Guid userId, IdentityUser user);
		Task<bool> Delete(Guid id);
		Task<IdentityUser?> GetByEmail(string email);
	}
}
