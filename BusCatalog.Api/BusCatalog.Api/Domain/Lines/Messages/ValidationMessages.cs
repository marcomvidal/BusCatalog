namespace BusCatalog.Api.Domain.Lines.Messages;

public static class ValidationMessages
{
    public const string IdentificationShouldBeUnique = "'Identification' should be unique.";
    public const string VehiclesDoesNotExists = "'Vehicles' should refeer to vehicles that exist in database.";
}