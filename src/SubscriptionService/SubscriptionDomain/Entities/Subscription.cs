using UserDomain.Primitives;

namespace SubscriptionDomain.Entities
{
	public class Subscription : Entity
	{
		public string? Name { get; set; }
		public decimal MonthlyPrice { get; set; }

		public Contract? Contract { get; set; }
	}
}
