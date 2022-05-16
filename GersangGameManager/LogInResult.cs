namespace GersangGameManager
{
	public record LogInResult
	{
		public LogInResultType Type { get; internal set; }
		public string? Message { get; internal set; }

		public LogInResult(LogInResultType resultType, string? message = null)
		{
			Type = resultType;
			Message = message;
		}
	}
}
