namespace FoodTracker.Domain.Common.Results;

public class Error
{
	public Error(string code, string message)
	{
		Code = code;
		Message = message;
	}

	public string Code { get; }
	public string Message { get; }
}