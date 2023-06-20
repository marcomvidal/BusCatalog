using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Infrastructure;
using SantoAndreOnBus.Api.Business.Vehicles;

namespace SantoAndreOnBus.Api.Business.Lines;

public class LineService
{
    private readonly DatabaseContext _db;
    private readonly IMapper _mapper;

    public LineService(DatabaseContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Line> GetByLineNameAsync(string lineName)
    {
        var partsOfName = new LineName(lineName);
        
        return await _db.Lines
            .Include(l => l.InterestPoints)
            .Include(l => l.Places)
            .Include(l => l.Company)
                .ThenInclude(c => c.Prefixes)
            .Include(l => l.Vehicles)
            .Where(l => l.Letter == partsOfName.Letter)
            .Where(l => l.Number == partsOfName.Number)
            .FirstAsync();
    }

    public async Task<int> GetCountByNameAsync(LineSubmitRequest line)
    {
        return await _db.Lines!
            .Where(l => l.Number == line.Number)
            .Where(l => l.Letter == line.Letter)
            .CountAsync();
    }

    public async Task<Line> SaveAsync(LineSubmitRequest request)
    {
        var line = LineDTOToModel.Map(request, new Line());
        line.Company = await _db.Companies!.FindAsync(request.CompanyId);
        
        _db.Lines!.Add(line);
        await AddVehiclesToLineAsync(line, request.Vehicles);
        await _db.SaveChangesAsync();
        
        return line;
    }

    public async Task<Line> UpdateAsync(Line line, LineSubmitRequest request)
    {
        line = LineDTOToModel.Map(request, line);
        line.Company = await _db.Companies!.FindAsync(request.CompanyId);
        await AddVehiclesToLineAsync(line, request.Vehicles);

        _db.Entry(line).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        return line;
    }

    public Line ClearRelationships(LineSubmitRequest request, Line line)
    {
        if (request.InterestPoints.Count > 0)
            _db.InterestPoints.RemoveRange(line.InterestPoints);

        if (request.Places.Count > 0)
            _db.Places.RemoveRange(line.Places);
            
        line.Vehicles.Clear();

        return line;
    }

    public async Task AddVehiclesToLineAsync(Line line, IList<Vehicle> vehicles)
    {
        foreach (var vehicle in vehicles)
            line.Vehicles.Add(vehicle);
    }
}
