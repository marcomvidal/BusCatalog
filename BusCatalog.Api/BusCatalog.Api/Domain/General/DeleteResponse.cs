namespace BusCatalog.Api.Domain.General;

public sealed record DeleteResponse : Response<DeleteData>
{
    public DeleteResponse() {}
    public DeleteResponse(int id) => Data = new DeleteData { Deleted = id };
}

public sealed record DeleteData
{
    public int Deleted { get; set; }
}
