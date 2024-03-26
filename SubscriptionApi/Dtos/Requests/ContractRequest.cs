namespace SubscriptionApi.Dtos.Requests
{
	public sealed class ContractRequest
	{
		public Guid? UserId { get; set; }
		public Guid? SubscriptionId { get; set; }
	}
}
