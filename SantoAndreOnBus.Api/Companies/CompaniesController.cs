using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Companies.DTOs;
using SantoAndreOnBus.Api.General;
using SantoAndreOnBus.Api.Infrastructure.Filters;

namespace SantoAndreOnBus.Api.Companies;
    
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyRepository _repository;
    private readonly IMapper _mapper;
    
    public CompaniesController(ICompanyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<CompanyListResponse>> Get() =>
        Ok(new CompanyListResponse(await _repository.GetAllAsync()));

    [ValidateModel]
    [HttpPost]
    public async Task<ActionResult<CompanyResponse>> Post([FromBody] CompanySubmitRequest request)
    {
        var company = _mapper.Map<Company>(request);
        await _repository.SaveAsync(company);

        return Ok(new CompanyResponse(company));
    }

    [ValidateModel]
    [HttpPut("{id}")]
    public async Task<ActionResult<CompanyResponse>> Put(int id, [FromBody] CompanySubmitRequest request)
    {
        var currentCompany = await _repository.GetByIdAsync(id);

        if (currentCompany is null)
            return NotFound();

        await _repository.FlushPrefixesAsync(currentCompany);
        var company = _mapper.Map(request, currentCompany);
        await _repository.UpdateAsync(company);

        return Ok(new CompanyResponse(company));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var company = await _repository.GetByIdAsync(id);
        
        if (company is null)
            return NotFound();

        await _repository.FlushPrefixesAsync(company);
        await _repository.DeleteAsync(company);

        return Ok(new DeleteResponse(id));
    }
}
