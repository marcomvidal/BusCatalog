using AutoMapper;
using BusCatalog.Api.Domain.General;
using BusCatalog.Api.Domain.Lines.Ports;
using BusCatalog.Api.Domain.Vehicles;

namespace BusCatalog.Api.Domain.Lines;

public interface ILineService
{
    Task<IEnumerable<Line>> GetAllAsync();
    Task<Line?> GetByIdAsync(int id);
    Task<LineResponse?> GetByIdentificationAsync(string identification);
    Task<Line> SaveAsync(LinePostRequest request);
    Task<Line> UpdateAsync(LinePutRequest request, Line line);
    Task<DeleteResponse> DeleteAsync(Line line);
}

public class LineService(
    ILineRepository lineRepository,
    IVehicleRepository vehicleRepository,
    IMapper mapper,
    ILogger<LineService> logger) : ILineService
{
    private readonly ILineRepository _lineRepository = lineRepository;
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<LineService> _logger = logger;

    public async Task<IEnumerable<Line>> GetAllAsync()
    {
        _logger.LogInformation(Messages.FetchingAllLines);

        return await _lineRepository.GetAllAsync();
    }

    public async Task<Line?> GetByIdAsync(int id)
    {
        _logger.LogInformation(Messages.FetchingLineById, id);
        var line = await _lineRepository.GetByAsync(x => x.Id == id, quantity: 1);

        return line.FirstOrDefault();
    }

    public async Task<LineResponse?> GetByIdentificationAsync(string identification)
    {
        _logger.LogInformation(Messages.FetchingLineByIdentification, identification);
        var line = await _lineRepository.GetByAsync(x => x.Identification == identification);

        return _mapper.Map<LineResponse>(line.FirstOrDefault());
    }

    public async Task<Line> SaveAsync(LinePostRequest request)
    {
        _logger.LogInformation(Messages.RegisteringLine, request.Identification);
        var vehicles = await _vehicleRepository.GetByIdentificatorsAsync(request.Vehicles);

        var line = new LineBuilder(_mapper.Map<Line>(request))
            .WithVehicles(vehicles)
            .Build();
        
        await _lineRepository.SaveAsync(line);

        return line;
    }

    public async Task<Line> UpdateAsync(LinePutRequest request, Line line)
    {
        var vehicles = await _vehicleRepository.GetByIdentificatorsAsync(request.Vehicles);
        
        var updatedLine = new LineBuilder(_mapper.Map(request, line))
            .WithVehicles(vehicles)
            .Build();
        
        await _lineRepository.UpdateAsync(updatedLine);
        _logger.LogInformation(Messages.UpdatingLine, request.Identification);

        return updatedLine;
    }

    public async Task<DeleteResponse> DeleteAsync(Line line)
    {
        await _lineRepository.DeleteAsync(line);
        _logger.LogInformation(Messages.DeletingLine, line.Identification);

        return new DeleteResponse(line.Id);
    }
}
