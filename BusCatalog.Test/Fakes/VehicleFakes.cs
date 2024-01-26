using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Test.ScenarioFakes;

public static class VehicleFakes
{
    public static Vehicle[] Vehicles()
    {
        return [
            new() { Id = 1, Identification = "MIDI", Description = "Midi Bus" },
            new() { Id = 2, Identification = "PADRON", Description = "Padron" },
            new() { Id = 3, Identification = "ARTICULATED", Description = "Articulated" }
        ];
    }
}
