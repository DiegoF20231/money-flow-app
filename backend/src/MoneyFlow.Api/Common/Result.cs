namespace MoneyFlow.Api.Common;

public class Result
{
    protected bool IsSuccess { get; }
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException("The result is successful but the error is not None");
        }

        if (!isSuccess && error != Error.None)
        {
            throw new InvalidOperationException("The result is failure but the error is not None");
        }

        IsSuccess = isSuccess;
        Error = error;
    }


    public static Result Success() => new(true, Error.None);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    
    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
}

public class Result<TValue> : Result
{
    public readonly TValue Value;

    public Result(TValue value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        Value = value;
    }

    public TValue GetValue()
    {
        if (!IsSuccess)
        {
            throw new InvalidOperationException("Value of the result is not available in failure results");
        }

        return Value;
    }
}