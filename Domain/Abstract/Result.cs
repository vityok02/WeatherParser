namespace Domain.Abstract;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; protected set; }

    protected Result()
    {
        IsSuccess = true;
    }

    protected Result(string code, string? description = null)
    {
        IsSuccess = false;
        Error = new Error(code, description);
    }

    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result Success() => new();
    public static Result Failure(Error error) => new(error);
    public static Result Failure(string code, string? description = null) => new(code, description);
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    private Result()
    {
        IsSuccess = true;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    private Result(string code, string? description = null)
    {
        IsSuccess = false;
        Error = new(code, description);
    }



    public static Result<T> Success(T value) => new(value);
    public new static Result<T> Failure(Error error) => new(error);
    public new static Result<T> Failure(string code, string? description = null) => new(code, description);
}