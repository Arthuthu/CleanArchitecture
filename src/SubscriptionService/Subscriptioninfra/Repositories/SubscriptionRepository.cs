using Microsoft.EntityFrameworkCore;
using SubscriptionApplication.Repositories;
using SubscriptionDomain.Entities;
using SubscriptionInfra.Context;

namespace SubscriptionInfra.Repositories
{
	public sealed class SubscriptionRepository : ISubscriptionRepository
	{
		private readonly ApplicationDbContext _context;

		public SubscriptionRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Subscription> Add(Subscription subscription, CancellationToken cancellationToken)
		{
			_context.Subscription.Add(subscription);
			await _context.SaveChangesAsync(cancellationToken);

			return subscription;
		}

		public async Task<Subscription?> Get(Guid id, CancellationToken cancellationToken)
		{
			Subscription? subscription = await _context.Subscription.FindAsync(id, cancellationToken);
			return subscription;
		}

		public async Task<List<Subscription>> Get(CancellationToken cancellationToken)
		{
			List<Subscription> subscriptions = await _context.Subscription.ToListAsync(cancellationToken);
			return subscriptions;
		}

		public async Task<Subscription?> Update(Subscription subscription, CancellationToken cancellationToken)
		{
			Subscription? requestedSubscription = await _context.Subscription.SingleOrDefaultAsync(x => x.Id == subscription.Id);

			if (requestedSubscription is null)
			{
				return null;
			}

			requestedSubscription.Name = subscription.Name;
			requestedSubscription.MonthlyPrice = subscription.MonthlyPrice;

			_context.Subscription.Update(requestedSubscription);
			await _context.SaveChangesAsync(cancellationToken);

			return subscription;
		}

		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			int affectedRows = await _context.Subscription.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
			return affectedRows > 0;
		}
	}
}
