using Contracts;
using MassTransit;
using SubscriptionDomain.Entities;
using SubscriptionInfra.Context;

namespace SubscriptionInfra.Consumers
{
	public sealed class SubscriptionConsumer : IConsumer<SubscriptionEvent>
	{
		private readonly SubscriptionContext _context;

		public SubscriptionConsumer(SubscriptionContext context)
		{
			_context = context;
		}

		public async Task Consume(ConsumeContext<SubscriptionEvent> context)
		{
			Subscription subscription = new()
			{
				Name = context.Message.Name,
				MonthlyPrice = context.Message.MonthlyPrice
			};

			_context.Add(subscription);
			await _context.SaveChangesAsync();
		}
	}
}
