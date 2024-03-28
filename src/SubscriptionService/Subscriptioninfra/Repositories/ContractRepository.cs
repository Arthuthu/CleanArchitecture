using Microsoft.EntityFrameworkCore;
using SubscriptionApplication.Abstractions.Repositories;
using SubscriptionDomain.Entities;
using SubscriptionInfra.Context;

namespace SubscriptionInfra.Repositories
{
	public sealed class ContractRepository : IContractRepository
	{
		private readonly ApplicationDbContext _context;

		public ContractRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Contract> Add(Contract contract, CancellationToken cancellationToken)
		{
			_context.Contract.Add(contract);
			await _context.SaveChangesAsync(cancellationToken);

			return contract;
		}

		public async Task<Contract?> Get(Guid id, CancellationToken cancellationToken)
		{
			Contract? contract = await _context.Contract.FindAsync(id, cancellationToken);
			return contract;
		}

		public async Task<List<Contract>> Get(CancellationToken cancellationToken)
		{
			List<Contract> contracts = await _context.Contract.ToListAsync(cancellationToken);
			return contracts;
		}

		public async Task<Contract?> Update(Contract contract, CancellationToken cancellationToken)
		{
			Contract? requestedContract = await _context.Contract.SingleOrDefaultAsync(x => x.Id == contract.Id);

			if (requestedContract is null)
			{
				return null;
			}

			requestedContract.SubscriptionId = contract.SubscriptionId;

			_context.Contract.Update(requestedContract);
			await _context.SaveChangesAsync(cancellationToken);

			return contract;
		}

		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			int affectedRows = await _context.Contract.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
			return affectedRows > 0;
		}
	}
}
