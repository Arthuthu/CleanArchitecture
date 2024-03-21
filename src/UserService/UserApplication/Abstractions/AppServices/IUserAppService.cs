using UserDomain.Entities;

namespace UserApplication.Abstractions.AppServices
{
    public interface IUserAppService
    {
        Task<User?> Add(User user, CancellationToken cancellationToken);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
        Task<List<User>> Get(CancellationToken cancellationToken);
        Task<User?> Get(Guid id, CancellationToken cancellationToken);
        Task Update(User user, CancellationToken cancellationToken);
    }
}