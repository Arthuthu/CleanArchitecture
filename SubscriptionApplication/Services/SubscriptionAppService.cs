using SubscriptionApplication.Abstractions.AppServices;
using SubscriptionApplication.Repositories;
using SubscriptionDomain.Entities;

namespace SubscriptionApplication.Services
{
    public sealed class SubscriptionAppService : ISubscriptionAppService
	{
		private readonly ISubscriptionRepository _repository;

		public SubscriptionAppService(ISubscriptionRepository repository)
		{
			_repository = repository;
		}

		public async Task<Subscription> Add(Subscription subscription, CancellationToken cancellationToken)
		{
			return await _repository.Add(subscription, cancellationToken);
		}

		public async Task<Subscription?> Get(Guid id, CancellationToken cancellationToken)
		{
			return await _repository.Get(id, cancellationToken);
		}

		public async Task<List<Subscription>> Get(CancellationToken cancellationToken)
		{
			return await _repository.Get(cancellationToken);
		}

		public async Task<Subscription?> Update(Subscription subscription, CancellationToken cancellationToken)
		{
			return await _repository.Update(subscription, cancellationToken);
		}

		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			return await _repository.Delete(id, cancellationToken);
		}
	}
}
