using AutoMapper;
using SantoAndreOnBus.Api.General;

namespace SantoAndreOnBus.Api.Business.Companies;

public interface ICompanyService
{
    Task<CompanyListResponse> GetAllAsync();
    Task<CompanyResponse> GetByIdAsync(int id);
    Task<CompanyResponse> SaveAsync(CompanySubmitRequest request);
    Task<CompanyResponse> UpdateAsync(CompanySubmitRequest request, Company company);
    Task<DeleteResponse> DeleteAsync(Company company);
}

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CompanyService> _logger;

    
    public CompanyService(
        ICompanyRepository repository,
        IMapper mapper,
        ILogger<CompanyService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<CompanyListResponse> GetAllAsync()
    {
        _logger.LogInformation("Fetching all registered companies.");

        return new CompanyListResponse(await _repository.GetAllAsync());
    }
        

    public async Task<CompanyResponse> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching company with ID {id}.", id);

        return new CompanyResponse(await _repository.GetByIdAsync(id));
    }
        
    public async Task<CompanyResponse> SaveAsync(CompanySubmitRequest request)
    {
        _logger.LogInformation("Registering company {name}.", request.Name);
        var company = _mapper.Map<Company>(request);
        await _repository.SaveAsync(company);

        return new CompanyResponse(company);
    }

    public async Task<CompanyResponse> UpdateAsync(CompanySubmitRequest request, Company company)
    {
        _logger.LogInformation("Updating company {name}.", company.Name);
        await _repository.FlushPrefixesAsync(company);
        await _repository.UpdateAsync(_mapper.Map(request, company));

        return new CompanyResponse(company);
    }

    public async Task<DeleteResponse> DeleteAsync(Company company)
    {
        _logger.LogInformation("Deleting company {name}.", company.Name);
        await _repository.FlushPrefixesAsync(company);
        await _repository.DeleteAsync(company);

        return new DeleteResponse(company.Id);
    }
}
