namespace FoodTracker.Domain.Common.Results;

public sealed class Result<T> : Result
{
    private readonly T? _value;

    private Result(T value) : base(true, null)
    {
        _value = value;
    }
    private Result(Error error) : base(false, error)
    {
        _value = default;
    }

    public T Value => IsSuccess ? _value! : throw new InvalidOperationException("A value is not available for a failed result.");

    public static Result<T> Success(T value) => new(value);

    public static new Result<T> Failure(Error error) => new(error);
}
