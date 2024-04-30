using UserDomain.Primitives;

namespace UserDomain.Entities
{
	public class User : Entity
	{
		public string? Username { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }

		public Guid? SubscriptionId { get; set; }
	}
}
