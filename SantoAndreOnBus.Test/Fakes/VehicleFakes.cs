using SantoAndreOnBus.Api.Domain.Vehicles;

namespace SantoAndreOnBus.Test.ScenarioFakes;

public static class VehicleFakes
{
    public static Vehicle[] Vehicles()
    {
        return [
            new() { Identification = "MIDI", Description = "Midi Bus" },
            new() { Identification = "PADRON", Description = "Padron" },
            new() { Identification = "ARTICULATED", Description = "Articulated" }
        ];
    }
}
