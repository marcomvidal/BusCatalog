namespace SantoAndreOnBus.Api.Domain.General;

public record Response<T>
{
    public T? Data { get; set; }
    public Dictionary<string, string[]> Errors { get; set; } = [];

    public Response() {}
    public Response(T? data) => Data = data;

    public Response(T? data, Dictionary<string, string[]> errors)
    {
        Data = data;
        Errors = errors;
    }
}
