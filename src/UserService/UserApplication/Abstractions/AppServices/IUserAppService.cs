using Microsoft.AspNetCore.Identity;
using UserDomain.Entities;

namespace UserApplication.Abstractions.AppServices
{
    public interface IUserAppService
    {
        Task<List<IdentityUser>> Get();
        Task<IdentityUser?> Get(Guid id);
		Task<bool> Delete(Guid id);
		Task<IdentityUser?> GetByEmail(string email);
		Task<IdentityUser?> GetByUsername(string username);
	}
}