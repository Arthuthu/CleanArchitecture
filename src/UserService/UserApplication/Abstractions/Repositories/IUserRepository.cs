using UserDomain.Entities;

namespace UserApplication.Abstractions.Repositories
{
	public interface IUserRepository
	{
		Task<bool> Delete(Guid id, CancellationToken cancellationToken);
		Task<List<User>> Get();
		Task<User?> Get(Guid id);
		Task<User?> GetByEmail(string email);
		Task<User?> GetByUsername(string username);
	}
}