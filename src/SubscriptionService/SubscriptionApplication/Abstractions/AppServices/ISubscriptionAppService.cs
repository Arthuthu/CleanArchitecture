using SubscriptionDomain.Entities;

namespace SubscriptionApplication.Abstractions.AppServices
{
    public interface ISubscriptionAppService
    {
        Task<Subscription> Add(Subscription subscription, CancellationToken cancellationToken);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
        Task<List<Subscription>> Get(CancellationToken cancellationToken);
        Task<Subscription?> Get(Guid id, CancellationToken cancellationToken);
        Task<Subscription?> Update(Subscription subscription, CancellationToken cancellationToken);
    }
}