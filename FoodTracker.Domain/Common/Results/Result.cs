namespace FoodTracker.Domain.Common.Results;

public class Result
{
    protected Result(bool isSuccess, Error? error)
    {
        if (isSuccess && error is not null)
        {
            throw new ArgumentException("Success result cannot contain an error.", nameof(error));
        }
        if (!isSuccess && error is null)
        {
            throw new ArgumentException("Failure result must contain an error.", nameof(error));
        }
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }
    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error);
}
