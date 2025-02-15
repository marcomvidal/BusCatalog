namespace BusCatalog.Api.Domain.Vehicles;

public static class Messages
{
    public const string GetVehiclesEndpoint = "Gets all registered vehicles.";
    public const string GetVehicleEndpoint = "Gets a vehicle by its identification.";
    public const string PostVehicleEndpoint = "Registers a new vehicle.";
    public const string PutVehicleEndpoint = "Updates data of a previously registered vehicle.";
    public const string DeleteVehicleEndpoint = "Deletes a vehicle by its ID.";
    public const string FetchingAllVehicles = "Fetching all registered vehicles.";
    public const string FetchingVehicleById = "Fetching registered vehicle with ID {id}.";
    public const string FetchingVehicleByIdentification = "Fetching registered vehicle with identification {identification}.";
    public const string RegisteringVehicle = "Registering vehicle {identification}.";
    public const string UpdatingVehicle = "Updated vehicle {identification}.";
    public const string DeletingVehicle = "Deleted vehicle {identification}.";
}