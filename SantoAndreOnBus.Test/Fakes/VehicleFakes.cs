using System.Collections.Generic;
using SantoAndreOnBus.Api.Business.Vehicles;

namespace SantoAndreOnBus.Test.Fakes;

public static class VehicleFakes
{
    public static IList<Vehicle> Vehicles
    {
        get => new Vehicle[]
        {
            new() { Identification = "MIDI", Description = "Midi Bus"  },
            new() { Identification = "PADRON", Description = "Padron" },
            new() { Identification = "ARTICULATED", Description = "Articulated" }
        };
    }
}
