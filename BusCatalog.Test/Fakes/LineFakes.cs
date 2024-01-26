using BusCatalog.Api.Domain.Lines;

namespace BusCatalog.Test.ScenarioFakes;

public static class LineFakes
{
    public static Line[] Lines()
    {
        return [
            new()
            {
                Identification = "001",
                Fromwards = "New York",
                Towards = "Massachussets",
                DeparturesPerDay = 20
            },
            new()
            {
                Identification = "002",
                Fromwards = "London",
                Towards = "Sussex",
                DeparturesPerDay = 50
            },
            new()
            {
                Identification = "003",
                Fromwards = "Brisbane",
                Towards = "Sydney",
                DeparturesPerDay = 30
            }
        ];
    }
}
