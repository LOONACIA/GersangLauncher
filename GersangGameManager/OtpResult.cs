namespace GersangGameManager
{
	public record OtpResult
	{
		public OtpResultType Type { get; internal set; }
		public string? Message { get; internal set; }

		public OtpResult(OtpResultType resultType, string? message = null)
		{
			Type = resultType;
			Message = message;
		}
	}
}
