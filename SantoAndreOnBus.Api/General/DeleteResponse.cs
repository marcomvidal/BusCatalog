namespace SantoAndreOnBus.Api.General;

public record DeleteResponse : Response<DeleteData>
{
    public DeleteResponse() {}

    public DeleteResponse(int id) => Data = new DeleteData { Deleted = id };
}

public record DeleteData
{
    public int Deleted { get; set; }
}
