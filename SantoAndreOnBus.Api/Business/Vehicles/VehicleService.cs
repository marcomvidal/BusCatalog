using AutoMapper;
using SantoAndreOnBus.Api.Business.General;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public interface IVehicleService
{
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(int id);
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

        return await _repository.GetByIdAsync(id);
    }

    public async Task<Vehicle?> GetByIdentificationAsync(string identification)
    {
        _logger.LogInformation("Fetching vehicle with identification {identification}.", identification);

        return await _repository.GetByIdentificationAsync(identification);
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
