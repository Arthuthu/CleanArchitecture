using SubscriptionDomain.Enums;
using UserDomain.Primitives;

namespace SubscriptionDomain.Entities
{
	public class Subscription : Entity
	{
		public SubscriptionType Type { get; set; }
		public decimal MonthlyPrice { get; set; }
	}
}
