using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Test.Fakes;

public class VehiclesFactory : IFakeFactory<Vehicle>
{
    public static Vehicle[] Generate() =>
        [
            new() { Id = 1, Identification = "MIDI", Description = "Midi Bus" },
            new() { Id = 2, Identification = "PADRON", Description = "Padron" },
            new() { Id = 3, Identification = "ARTICULATED", Description = "Articulated" }
        ];
}
