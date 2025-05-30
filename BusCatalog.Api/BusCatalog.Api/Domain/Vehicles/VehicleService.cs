using AutoMapper;
using BusCatalog.Api.Domain.General;
using BusCatalog.Api.Domain.Vehicles.Ports;
using static BusCatalog.Api.Domain.Vehicles.Messages.ServiceMessages;

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

public sealed class VehicleService : IVehicleService
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

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        _logger.LogInformation(FetchingAllVehicles);

        return await _repository.GetAllAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        _logger.LogInformation(FetchingVehicleById, id);
        var vehicle = await _repository.GetByAsync(x => x.Id == id, quantity: 1);

        return vehicle.FirstOrDefault();
    }

    public async Task<Vehicle?> GetByIdentificationAsync(string identification)
    {
        _logger.LogInformation(FetchingVehicleByIdentification, identification);

        var vehicle = await _repository.GetByAsync(
            x => x.Identification.Equals(identification.ToUpper()), quantity: 1);

        return vehicle.FirstOrDefault();
    }
    
    public async Task<Vehicle> SaveAsync(VehiclePostRequest request)
    {
        _logger.LogInformation(RegisteringVehicle, request.Identification);
        var vehicle = _mapper.Map<Vehicle>(request);
        await _repository.SaveAsync(vehicle);

        return vehicle;
    }

    public async Task<Vehicle> UpdateAsync(VehiclePostRequest request, Vehicle vehicle)
    {
        _logger.LogInformation(UpdatingVehicle, request.Identification);
        var updatedVehicle = _mapper.Map(request, vehicle);
        await _repository.UpdateAsync(updatedVehicle);

        return updatedVehicle;
    }

    public async Task<DeleteResponse> DeleteAsync(Vehicle vehicle)
    {
        _logger.LogInformation(DeletingVehicle, vehicle.Identification);
        await _repository.DeleteAsync(vehicle);

        return new DeleteResponse(vehicle.Id);
    }
}
