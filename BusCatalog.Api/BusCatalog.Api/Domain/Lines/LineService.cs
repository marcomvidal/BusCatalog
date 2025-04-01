using AutoMapper;
using BusCatalog.Api.Domain.General;
using BusCatalog.Api.Domain.Lines.Ports;
using BusCatalog.Api.Domain.Vehicles;
using static BusCatalog.Api.Domain.Lines.Messages.ServiceMessages;

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

public sealed class LineService : ILineService
{
    private readonly ILineRepository _lineRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<LineService> _logger;

    public LineService(
        ILineRepository lineRepository,
        IVehicleRepository vehicleRepository,
        IMapper mapper,
        ILogger<LineService> logger)
    {
        _lineRepository = lineRepository;
        _vehicleRepository = vehicleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<Line>> GetAllAsync()
    {
        _logger.LogInformation(FetchingAllLines);

        return await _lineRepository.GetAllAsync();
    }

    public async Task<Line?> GetByIdAsync(int id)
    {
        _logger.LogInformation(FetchingLineById, id);
        var line = await _lineRepository.GetByAsync(x => x.Id == id, quantity: 1);

        return line.FirstOrDefault();
    }

    public async Task<LineResponse?> GetByIdentificationAsync(string identification)
    {
        _logger.LogInformation(FetchingLineByIdentification, identification);
        var line = await _lineRepository.GetByAsync(x => x.Identification == identification);

        return _mapper.Map<LineResponse>(line.FirstOrDefault());
    }

    public async Task<Line> SaveAsync(LinePostRequest request)
    {
        _logger.LogInformation(RegisteringLine, request.Identification);
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
        _logger.LogInformation(UpdatingLine, request.Identification);

        return updatedLine;
    }

    public async Task<DeleteResponse> DeleteAsync(Line line)
    {
        await _lineRepository.DeleteAsync(line);
        _logger.LogInformation(DeletingLine, line.Identification);

        return new DeleteResponse(line.Id);
    }
}
