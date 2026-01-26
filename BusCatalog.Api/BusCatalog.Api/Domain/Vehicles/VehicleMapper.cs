using BusCatalog.Api.Domain.Vehicles.Ports;

namespace BusCatalog.Api.Domain.Vehicles;

public static class VehicleMapper
{
    extension(VehiclePostRequest request)
    {
        public Vehicle ToVehicle() =>
            new()
            {
                Identification = request.Identification!,
                Description = request.Description!
            };
    }

    extension(VehiclePostRequest request)
    {
        public Vehicle MergeWithSavedVehicle(Vehicle vehicle)
        {
            vehicle.Identification = request.Identification!;
            vehicle.Description = request.Description!;

            return vehicle;   
        }
    }
}
