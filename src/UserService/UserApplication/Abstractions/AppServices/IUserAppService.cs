using Microsoft.AspNetCore.Identity;
using UserDomain.Entities;

namespace UserApplication.Abstractions.AppServices
{
    public interface IUserAppService
    {
        Task<List<IdentityUser>> Get();
        Task<IdentityUser?> Get(Guid id);
        Task<IdentityUser?> Update(Guid userId, IdentityUser user);
		Task<bool> Delete(Guid id);
		Task<IdentityUser?> GetByEmail(string email);
	}
}