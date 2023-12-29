using AutoMapper;
using SantoAndreOnBus.Api.Domain.General;

namespace SantoAndreOnBus.Api.Domain.Places;

public interface IPlaceService
{
    Task<IEnumerable<Place>> GetAllAsync();
    Task<Place?> GetByIdAsync(int id);
    Task<Place> SaveAsync(PlacePostRequest request);
    Task<Place> UpdateAsync(PlacePutRequest request, Place place);
    Task<DeleteResponse> DeleteAsync(Place place);
}

public class PlaceService(
    IPlaceRepository repository,
    IMapper mapper,
    ILogger<PlaceService> logger) : IPlaceService
{
    private readonly IPlaceRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<PlaceService> _logger = logger;

    public async Task<IEnumerable<Place>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all registered places.");

        return await _repository.GetAllAsync();
    }

    public async Task<Place?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching place with ID {id}.", id);
        var place = await _repository.GetByAsync(x => x.Id == id, quantity: 1);

        return place.FirstOrDefault();
    }
    
    public async Task<Place> SaveAsync(PlacePostRequest request)
    {
        _logger.LogInformation("Registering place {identification}.", request.Identification);
        var place = _mapper.Map<Place>(request);
        await _repository.SaveAsync(place);

        return place;
    }

    public async Task<Place> UpdateAsync(PlacePutRequest request, Place place)
    {
        var updatedPlace = _mapper.Map(request, place);
        await _repository.UpdateAsync(updatedPlace);
        _logger.LogInformation("Updated place {identification}.", request.Identification);

        return updatedPlace;
    }

    public async Task<DeleteResponse> DeleteAsync(Place place)
    {
        await _repository.DeleteAsync(place);
        _logger.LogInformation("Deleted place {identification}.", place.Identification);

        return new DeleteResponse(place.Id);
    }
}
