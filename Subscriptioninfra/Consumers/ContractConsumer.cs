using Contracts.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SubscriptionDomain.Entities;
using SubscriptionInfra.Context;

namespace SubscriptionInfra.Consumers
{
    public sealed class ContractConsumer : IConsumer<ContractEvent>
	{
		private readonly ApplicationDbContext _context;

		public ContractConsumer(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Consume(ConsumeContext<ContractEvent> context)
		{
			Contract newContract = new() { UserId = context.Message.UserId };

			Subscription? freeSubscription = await _context.Subscription
				.Where(x => x.MonthlyPrice == 0).FirstOrDefaultAsync() 
				?? throw new Exception("An error has ocurred when adding the first contract, no free contract has been found");
			
			newContract.SubscriptionId = freeSubscription.Id;

			_context.Contract.Add(newContract);
			await _context.SaveChangesAsync();
		}
	}
}
