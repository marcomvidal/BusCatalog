using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.General;
using SantoAndreOnBus.Api.Infrastructure.Filters;

namespace SantoAndreOnBus.Api.Business.Companies;
    
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _service;
    
    public CompaniesController(ICompanyService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<CompanyListResponse>> Get() =>
        Ok(await _service.GetAllAsync());

    [ValidateModel]
    [HttpPost]
    public async Task<ActionResult<CompanyResponse>> Post([FromBody] CompanySubmitRequest request) =>
        Ok(await _service.SaveAsync(request));

    [ValidateModel]
    [HttpPut("{id}")]
    public async Task<ActionResult<CompanyResponse>> Put(int id, [FromBody] CompanySubmitRequest request)
    {
        var company = await _service.GetByIdAsync(id);

        return company.Data is not null
            ? Ok(await _service.UpdateAsync(request, company.Data))
            : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeleteResponse>> Delete(int id)
    {
        var company = await _service.GetByIdAsync(id);

        return company.Data is not null
            ? Ok(await _service.DeleteAsync(company.Data))
            : NotFound();
    }
}
