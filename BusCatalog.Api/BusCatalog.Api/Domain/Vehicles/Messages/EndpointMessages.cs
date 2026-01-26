namespace BusCatalog.Api.Domain.Vehicles.Messages;

public static class EndpointMessages
{
    public const string GetVehicles = "Gets all registered vehicles.";
    public const string GetVehicle = "Gets a vehicle by its identification.";
    public const string PostVehicle = "Registers a new vehicle.";
    public const string PutVehicle = "Updates data of a previously registered vehicle.";
    public const string DeleteVehicle = "Deletes a vehicle by its ID.";
}
