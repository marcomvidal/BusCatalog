using System.Collections.Generic;
using SantoAndreOnBus.Api.Business.Companies;

namespace SantoAndreOnBus.Test.Fakes;

public static class CompanyFakes
{
    public static IList<Company> Companies
    {
        get => new Company[]
        {
            new() { Name = "Company 1", Prefixes = new [] { Prefixes[0], Prefixes[1] } },
            new() { Name = "Company 2", Prefixes = new [] { Prefixes[2], Prefixes[3] } },
            new() { Name = "Company 3", Prefixes = new [] { Prefixes[4], Prefixes[5] } },
        };
    }

    public static IList<Prefix> Prefixes
    {
        get => new Prefix[]
        {
            new() { Identification = "A" },
            new() { Identification = "B" },
            new() { Identification = "C" },
            new() { Identification = "D" },
            new() { Identification = "E" },
            new() { Identification = "F" },
        };
    }
}
