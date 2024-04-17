using Contracts.Enums;

namespace Contracts.Entites
{
	public sealed class Result
	{
		public ResultType Type { get; set; }
		public string? Description { get; set; }
	}
}
