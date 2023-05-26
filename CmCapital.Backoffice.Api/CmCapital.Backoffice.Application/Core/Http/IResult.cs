using System.Net;

namespace CmCapital.Backoffice.Application.Core.Http;

public interface IResult
{
    public object? Value { get; }
    public Type? Type { get; }
    public bool IsSuccess { get; }
    public string? Message { get; init; }
    public string? Location { get; set; }
    public HttpStatusCode StatusCode { get; }
    public IEnumerable<string>? Errors { get; }
}

public interface IResult<out TValue> : IResult 
{
    new TValue? Value { get; }
}