namespace BusCatalog.Api.Domain.Vehicles.Messages;

public static class ServiceMessages
{
    public const string FetchingAllVehicles = "Fetching all registered vehicles.";
    public const string FetchingVehicleById = "Fetching registered vehicle with ID {id}.";
    public const string FetchingVehicleByIdentification = "Fetching registered vehicle with identification {identification}.";
    public const string RegisteringVehicle = "Registering vehicle {identification}.";
    public const string UpdatingVehicle = "Updated vehicle {identification}.";
    public const string DeletingVehicle = "Deleted vehicle {identification}.";
}
