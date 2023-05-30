namespace SantoAndreOnBus.Api.General;

public record Response<T>
{
    public T? Data { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }
        = new Dictionary<string, string[]>();

    public Response() {}

    public Response(T? data) => Data = data;

    public Response(T? data, Dictionary<string, string[]> errors)
    {
        Data = data;
        Errors = errors;
    }
}
