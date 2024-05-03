namespace UserDomain.Primitives
{
	public abstract class Entity
	{
		public Guid Id { get; init; } = Guid.NewGuid();
	}
}
