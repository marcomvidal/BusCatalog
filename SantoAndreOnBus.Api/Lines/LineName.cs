namespace SantoAndreOnBus.Api.Lines;

public record LineName
{
    public LineName(string letterAndNumber)
    {
        string[] partsOfName = letterAndNumber.Split('-');
        Letter = partsOfName[0];
        Number = partsOfName[1];
    }

    public string Letter { get; set; }
    public string Number { get; set; }
}
