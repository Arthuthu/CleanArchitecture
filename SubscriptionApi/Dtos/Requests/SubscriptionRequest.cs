namespace SubscriptionApi.Dtos.Requests
{
	public sealed class SubscriptionRequest
	{
		public string? Name { get; set; }
		public decimal MonthlyPrice { get; set; }
	}
}
