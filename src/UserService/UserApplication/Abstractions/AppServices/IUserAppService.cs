using Microsoft.AspNetCore.Identity;
using UserDomain.Entities;

namespace UserApplication.Abstractions.AppServices
{
    public interface IUserAppService
    {
        Task<List<User>> Get();
        Task<User?> Get(Guid id);
		Task<bool> Delete(Guid id, CancellationToken cancellationToken);
		Task<User?> GetByEmail(string email);
		Task<User?> GetByUsername(string username);
	}
}