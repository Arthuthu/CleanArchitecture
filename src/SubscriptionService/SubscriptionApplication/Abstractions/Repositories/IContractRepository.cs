using SubscriptionDomain.Entities;

namespace SubscriptionApplication.Abstractions.Repositories
{
	public interface IContractRepository
	{
		Task<Contract> Add(Contract contract, CancellationToken cancellationToken);
		Task<bool> Delete(Guid id, CancellationToken cancellationToken);
		Task<List<Contract>> Get(CancellationToken cancellationToken);
		Task<Contract?> Get(Guid id, CancellationToken cancellationToken);
		Task<Contract?> Update(Contract contract, CancellationToken cancellationToken);
	}
}