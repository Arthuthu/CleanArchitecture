using SubscriptionDomain.Enums;

namespace SubscriptionApi.Dtos.Responses
{
	public sealed class SubscriptionResponse
	{
		public SubscriptionType Type { get; set; }
		public decimal MonthlyPrice { get; set; }
	}
}
