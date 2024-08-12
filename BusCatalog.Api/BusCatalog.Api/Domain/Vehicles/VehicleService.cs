using AutoMapper;
using BusCatalog.Api.Domain.General;
using BusCatalog.Api.Domain.Vehicles.Ports;

namespace BusCatalog.Api.Domain.Vehicles;

public interface IVehicleService
{
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(int id);
    Task<Vehicle?> GetByIdentificationAsync(string identification);
    Task<Vehicle> SaveAsync(VehiclePostRequest request);
    Task<Vehicle> UpdateAsync(VehiclePostRequest request, Vehicle vehicle);
    Task<DeleteResponse> DeleteAsync(Vehicle vehicle);
}

public class VehicleService(
    IVehicleRepository repository,
    IMapper mapper,
    ILogger<VehicleService> logger) : IVehicleService
{
    private readonly IVehicleRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<VehicleService> _logger = logger;

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all registered vehicles.");

        return await _repository.GetAllAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching vehicle with ID {id}.", id);
        var vehicle = await _repository.GetByAsync(x => x.Id == id, quantity: 1);

        return vehicle.FirstOrDefault();
    }

    public async Task<Vehicle?> GetByIdentificationAsync(string identification)
    {
        _logger.LogInformation("Fetching vehicle with identification {id}.", identification);

        var vehicle = await _repository.GetByAsync(
            x => x.Identification.Equals(identification.ToUpper()), quantity: 1);

        return vehicle.FirstOrDefault();
    }
    
    public async Task<Vehicle> SaveAsync(VehiclePostRequest request)
    {
        _logger.LogInformation("Registering vehicle {identification}.", request.Identification);
        var vehicle = _mapper.Map<Vehicle>(request);
        await _repository.SaveAsync(vehicle);

        return vehicle;
    }

    public async Task<Vehicle> UpdateAsync(VehiclePostRequest request, Vehicle vehicle)
    {
        _logger.LogInformation("Updating vehicle {identification}.", request.Identification);
        var updatedVehicle = _mapper.Map(request, vehicle);
        await _repository.UpdateAsync(updatedVehicle);

        return updatedVehicle;
    }

    public async Task<DeleteResponse> DeleteAsync(Vehicle vehicle)
    {
        _logger.LogInformation("Deleting vehicle {identification}.", vehicle.Identification);
        await _repository.DeleteAsync(vehicle);

        return new DeleteResponse(vehicle.Id);
    }
}
