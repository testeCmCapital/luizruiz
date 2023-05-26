using System.Net;

namespace CmCapital.Backoffice.Application.Core.Http;

public sealed class Result<TValue> : Result, IResult<TValue>, IResult
{
    internal Result(HttpStatusCode httpStatusCode, TValue? value) 
        : base(httpStatusCode)
    {
        Value = value;
    }

    internal Result(HttpStatusCode httpStatusCode, TValue? value, string? message, string? location)
        :base(httpStatusCode, message, location)
    {
        Value = value;
    }

    public new static Result<TValue> Success() 
    {
        return new Result<TValue>(HttpStatusCode.OK, default);
    }

    public new static Result<TValue> Success(string message)
    {
        ArgumentNullException.ThrowIfNull(message, "message");
        return new Result<TValue>(HttpStatusCode.OK, default, message, null);
    }

    public static Result<TValue> Success(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value, "value");
        return new Result<TValue>(HttpStatusCode.OK, value);
    }

    public static Result<TValue> Success(TValue value, string message)
    {
        ArgumentNullException.ThrowIfNull(value, "value");
        ArgumentNullException.ThrowIfNull(message, "message");
        return new Result<TValue>(HttpStatusCode.OK, value, message, null);
    }

    public static Result<TValue> Created(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value, "value");
        return new Result<TValue>(HttpStatusCode.Created, value);
    }

    public new static Result<TValue> Created(string location)
    {
        ArgumentNullException.ThrowIfNull(location, "location");
        return new Result<TValue>(HttpStatusCode.Created, default, null, location);
    }

    public static Result<TValue> Created(TValue value, string location)
    {
        ArgumentNullException.ThrowIfNull(value, "value");
        ArgumentNullException.ThrowIfNull(location, "location");
        return new Result<TValue>(HttpStatusCode.Created, value, null, location);
    }

    public static Result<TValue> Created(TValue value, string message, string location)
    {
        ArgumentNullException.ThrowIfNull(value, "value"); 
        ArgumentNullException.ThrowIfNull(message, "message");
        ArgumentNullException.ThrowIfNull(location, "location");
        return new Result<TValue>(HttpStatusCode.Created, value, message, location);
    }

    public new static Result<TValue> Accepted()
    {
        return new Result<TValue>(HttpStatusCode.Accepted, default);
    }

    public new static Result<TValue> Accepted(string location)
    {
        ArgumentNullException.ThrowIfNull(location, "location");
        return new Result<TValue>(HttpStatusCode.Accepted, default, null, location);
    }

    public static Result<TValue> Accepted(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value, "value");
        return new Result<TValue>(HttpStatusCode.Accepted, value);
    }

    public static Result<TValue> Accepted(TValue value, string location)
    {
        ArgumentNullException.ThrowIfNull(value, "value");
        ArgumentNullException.ThrowIfNull(location, "location");
        return new Result<TValue>(HttpStatusCode.Accepted, value, null, location);
    }

    public new static Result<TValue> NoContent()
    {
        return new Result<TValue>(HttpStatusCode.NoContent, default);
    }

    public new static Result<TValue?> BadRequest(params string[] errorMessage) 
    {
        return new Result<TValue?>(HttpStatusCode.BadRequest, default)
        {
            Errors = errorMessage
        };
    }

    public new static Result<TValue> NotFound()
    {
        return new Result<TValue>(HttpStatusCode.NotFound, default);
    }

    public new static Result<TValue?> NotFound(params string[] errorMessage)
    {
        return new Result<TValue?>(HttpStatusCode.NotFound, default)
        {
            Errors = errorMessage
        };
    }

    public static Result<TValue> Forbidden()
    {
        return new Result<TValue>(HttpStatusCode.Forbidden, default);
    }

    public static Result<TValue> Unauthorized()
    {
        return new Result<TValue>(HttpStatusCode.Unauthorized, default);
    }

    public new static Result<TValue?> Conflict(params string[] errorMessage)
    {
        return new Result<TValue?>(HttpStatusCode.Conflict, default)
        {
            Errors = errorMessage
        };
    }

    public static Result<TValue?> Conflict(TValue? value, params string[] errorMessage)
    {
        return new Result<TValue?>(HttpStatusCode.Conflict, value)
        {
            Errors = errorMessage
        };
    }

    public new static Result<TValue> InternalServerError()
    {
        return new Result<TValue>(HttpStatusCode.InternalServerError, default);
    }

    public static implicit operator TValue?(Result<TValue?> result) 
    {
        return result.Value;
    }

    public static implicit operator Result<TValue?>(TValue value) 
    {
        return new Result<TValue?>(HttpStatusCode.OK, value);
    }

    public new TValue? Value { get; }
    object? IResult.Value => Value;
}

public class Result : IResult
{
    public object? Value { get; }
    public Type? Type => Value?.GetType();
    public bool IsSuccess 
    {
        get 
        {
            if ((int?)StatusCode >= 200)
                return (int?)StatusCode <= 299;

            return false;
        }
    }
    public string? Message { get; init; }
    public string? Location { get; set; }
    public HttpStatusCode StatusCode { get; }
    public IEnumerable<string>? Errors { get; protected init; }

    protected Result(HttpStatusCode httpStatusCode)
    {
        StatusCode = httpStatusCode;
    }

    protected Result(
        HttpStatusCode httpStatusCode,
        string? message,
        string? location)
        : this(httpStatusCode) 
    {
        Message = message;
        Location = location;
    }

    public static Result Success() 
    {
        return new Result(HttpStatusCode.OK);
    }

    public static Result Success(string message) 
    {
        ArgumentNullException.ThrowIfNull(message);
        return new Result(HttpStatusCode.OK, message, null);
    }

    public static Result Created(string location) 
    {
        ArgumentNullException.ThrowIfNull(location);
        return new Result(HttpStatusCode.Created, null, location);
    }

    public static Result Accepted() 
    {
        return new Result(HttpStatusCode.Accepted);
    }

    public static Result Accepted(string location)
    {
        ArgumentNullException.ThrowIfNull(location);
        return new Result(HttpStatusCode.Accepted, null, location);
    }

    public static Result NoContent() 
    {
        return new Result(HttpStatusCode.NoContent);
    }

    public static Result BadRequest(params string[] errorMessage) 
    {
        return new Result(HttpStatusCode.BadRequest)
        {
            Errors = errorMessage
        };
    }

    public static Result NotFound() 
    {
        return new Result(HttpStatusCode.NotFound);
    }

    public static Result NotFound(params string[] errorMessage)
    {
        return new Result(HttpStatusCode.NotFound)
        {
            Errors = errorMessage
        };
    }

    public static Result Unathorized() 
    {
        return new Result(HttpStatusCode.Unauthorized);
    }

    public static Result Conflict(params string[] errorMessage) 
    {
        return new Result(HttpStatusCode.Conflict)
        {
            Errors = errorMessage
        };
    }

    public static Result InternalServerError() 
    {
        return new Result(HttpStatusCode.InternalServerError);
    }

    public static Result InternalServerError(params string[] errorMessage)
    {
        return new Result(HttpStatusCode.InternalServerError) 
        {
            Errors = errorMessage
        };
    }
}