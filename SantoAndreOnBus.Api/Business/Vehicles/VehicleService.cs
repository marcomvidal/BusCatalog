using AutoMapper;
using SantoAndreOnBus.Api.General;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public interface IVehicleService
{
    Task<VehicleListResponse> GetAllAsync();
    Task<VehicleResponse> GetByIdAsync(int id);
    Task<VehicleResponse> SaveAsync(VehicleSubmitRequest request);
    Task<VehicleResponse> UpdateAsync(VehicleSubmitRequest request, Vehicle vehicle);
    Task<DeleteResponse> DeleteAsync(Vehicle vehicle);
}

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<VehicleService> _logger;

    public VehicleService(
        IVehicleRepository repository,
        IMapper mapper,
        ILogger<VehicleService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<VehicleListResponse> GetAllAsync()
    {
        _logger.LogInformation("Fetching all registered vehicles.");

        return new VehicleListResponse(await _repository.GetAllAsync());
    }

    public async Task<VehicleResponse> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching vehicle with ID {id}.", id);

        return new VehicleResponse(await _repository.GetByIdAsync(id));
    }
    
    public async Task<VehicleResponse> SaveAsync(VehicleSubmitRequest request)
    {
        _logger.LogInformation("Registering vehicle {identification}.", request.Identification);
        var vehicle = _mapper.Map<Vehicle>(request);
        await _repository.SaveAsync(vehicle);

        return new VehicleResponse(vehicle);
    }

    public async Task<VehicleResponse> UpdateAsync(VehicleSubmitRequest request, Vehicle vehicle)
    {
        _logger.LogInformation("Updating vehicle {identification}.", request.Identification);
        await _repository.UpdateAsync(_mapper.Map(request, vehicle));

        return new VehicleResponse(vehicle);
    }

    public async Task<DeleteResponse> DeleteAsync(Vehicle vehicle)
    {
        _logger.LogInformation("Deleting vehicle {identification}.", vehicle.Identification);
        await _repository.DeleteAsync(vehicle);

        return new DeleteResponse(vehicle.Id);
    }
}
