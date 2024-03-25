namespace Contracts
{
	public record SubscriptionEvent
	{
		public string? Name { get; set; }
		public decimal MonthlyPrice { get; set; }
	}
}
