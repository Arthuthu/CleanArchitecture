using UserDomain.Primitives;

namespace SubscriptionDomain.Entities
{
	public class Contract : Entity
	{
		public Guid? UserId { get; set; }
		public Guid? SubscriptionId { get; set; }

		public Subscription? Subscription { get; set; }
	}
}
