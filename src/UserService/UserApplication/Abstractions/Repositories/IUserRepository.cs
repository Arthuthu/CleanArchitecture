using UserDomain.Entities;

namespace UserApplication.Abstractions.Repositories
{
	public interface IUserRepository
	{
		Task<User> Add(User user, CancellationToken cancellationToken);
		Task<User?> Get(Guid id, CancellationToken cancellationToken);
		Task<List<User>> Get(CancellationToken cancellationToken);
		Task<User?> Update(Guid userId, User user, CancellationToken cancellationToken);
		Task<bool> Delete(Guid id, CancellationToken cancellationToken);
		Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
	}
}
