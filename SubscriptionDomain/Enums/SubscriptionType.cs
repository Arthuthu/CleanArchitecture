using System.ComponentModel;

namespace SubscriptionDomain.Enums
{
	public enum SubscriptionType
	{
		[Description("No subscription")]
		None = 0,

		[Description("Basic subscription")]
		Basic = 1,

		[Description("Premium subscription")]
		Premium = 2
	}
}
