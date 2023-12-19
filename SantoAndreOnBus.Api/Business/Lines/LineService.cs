using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Infrastructure;
using AutoMapper;
using SantoAndreOnBus.Api.Business.General;
using SantoAndreOnBus.Api.Business.Vehicles;
using SantoAndreOnBus.Api.Business.Places;

namespace SantoAndreOnBus.Api.Business.Lines;

public interface ILineService
{
    Task<IEnumerable<Line>> GetAllAsync();
    Task<Line?> GetByIdAsync(int id);
    Task<Line?> GetByIdentificationAsync(string identification);
    Task<Line> SaveAsync(LinePostRequest request);
    Task<Line> UpdateAsync(LinePostRequest request, Line line);
    Task<DeleteResponse> DeleteAsync(Line line);
}

public class LineService(
    DatabaseContext db,
    ILineRepository lineRepository,
    IVehicleRepository vehicleRepository,
    IPlaceRepository placeRepository,
    ILogger<LineService> logger,
    IMapper mapper) : ILineService
{
    private readonly DatabaseContext _db = db;
    private readonly ILineRepository _lineRepository = lineRepository;
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
    private readonly IPlaceRepository _placeRepository = placeRepository;
    private readonly ILogger<LineService> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<Line>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all registered lines.");

        return await _lineRepository.GetAllAsync();
    }

    public async Task<Line?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching registered line with ID {id}.", id);

        return await _lineRepository.GetByIdAsync(id);
    }

    public async Task<Line?> GetByIdentificationAsync(string identification)
    {
        _logger.LogInformation(
            "Fetching registered line with identification {identification}.", identification);

        return await _lineRepository.GetByIdentificationAsync(identification);
    }

    public async Task<Line> SaveAsync(LinePostRequest request)
    {
        _logger.LogInformation("Registering line {identification}.", request.Identification);
        var line = _mapper.Map<Line>(request);
        
        line.Vehicles.AddRange(
            await _vehicleRepository.GetByIdentificationAsync(request.VehiclesIdentifications));
        
        line.Places.AddRange(
            await _placeRepository.GetByIdAsync(request.PlacesIdList));
        
        await _lineRepository.SaveAsync(line);

        return line;
    }

    public async Task<Line> UpdateAsync(LinePostRequest request, Line line)
    {
        var updatedLine = _mapper.Map(request, line);
        _db.Entry(updatedLine).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        return line;
    }

    public async Task<Line> UpdateAsync(LinePutRequest request, Line place)
    {
        var updatedLine = _mapper.Map(request, place);
        await _lineRepository.UpdateAsync(updatedLine);
        _logger.LogInformation("Updated place {identification}.", request.Identification);

        return updatedLine;
    }

    public async Task<DeleteResponse> DeleteAsync(Line line)
    {
        await _lineRepository.DeleteAsync(line);
        _logger.LogInformation("Deleted line {identification}.", line.Identification);

        return new DeleteResponse(line.Id);
    }
}
