using BusCatalog.Api.Domain.Lines;

namespace BusCatalog.Test.Fakes;

public class LinesFactory : IFakeFactory<Line>
{
    public static Line[] Generate() =>
        [
            new()
            {
                Id = 1,
                Identification = "001",
                Fromwards = "New York",
                Towards = "Massachussets",
                DeparturesPerDay = 20
            },
            new()
            {
                Id = 2,
                Identification = "002",
                Fromwards = "London",
                Towards = "Sussex",
                DeparturesPerDay = 50
            },
            new()
            {
                Id = 3,
                Identification = "003",
                Fromwards = "Brisbane",
                Towards = "Sydney",
                DeparturesPerDay = 30
            }
        ];
}
