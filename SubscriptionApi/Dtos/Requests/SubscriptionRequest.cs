using SubscriptionDomain.Enums;

namespace SubscriptionApi.Dtos.Requests
{
	public sealed class SubscriptionRequest
	{
		public SubscriptionType Type { get; set; }
		public decimal MonthlyPrice { get; set; }
	}
}
