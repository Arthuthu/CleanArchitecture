using SubscriptionApplication.Abstractions.AppServices;
using SubscriptionApplication.Abstractions.Repositories;
using SubscriptionDomain.Entities;

namespace SubscriptionApplication.Services
{
    public sealed class ContractAppService : IContractAppService
	{
		private readonly IContractRepository _repository;

		public ContractAppService(IContractRepository repository)
		{
			_repository = repository;
		}

		public async Task<Contract> Add(Contract contract, CancellationToken cancellationToken)
		{
			return await _repository.Add(contract, cancellationToken);
		}

		public async Task<Contract?> Get(Guid id, CancellationToken cancellationToken)
		{
			return await _repository.Get(id, cancellationToken);
		}

		public async Task<List<Contract>> Get(CancellationToken cancellationToken)
		{
			return await _repository.Get(cancellationToken);
		}

		public async Task<Contract?> Update(Contract contract, CancellationToken cancellationToken)
		{
			return await _repository.Update(contract, cancellationToken);
		}

		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			return await _repository.Delete(id, cancellationToken);
		}
	}
}
