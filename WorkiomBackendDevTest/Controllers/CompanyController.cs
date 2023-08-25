using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WorkiomBackendDevTest.Cashing;
using WorkiomBackendDevTest.Entities;
using WorkiomBackendDevTest.Repositories.Classes;
using WorkiomBackendDevTest.Services;

namespace WorkiomBackendDevTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyService _companyService;

        public CompanyController(CompanyService companyService)
        {
            _companyService = companyService;
        }


        //we use here Caching for performance issues.
        [HttpGet]
        [Cache(10)]
        public async Task<ActionResult<List<Company>>> GetAllCompanies()
        {
            var companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompanyById(string id)
        {
            var company = await _companyService.GetByIdAsync(id);
            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Company>>> SearchCompanies([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name filter is required.");

            var companies = await _companyService.FindCompaniesByLocationAsync(name);
            return Ok(companies);
        }

        [HttpPost]
        public async Task<ActionResult<Company>> CreateCompany(Company company)
        {
            await _companyService.CreateAsync(company);
            return CreatedAtAction(nameof(GetCompanyById), new { id = company.Id }, company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(string id, Company company)
        {
            if (id != company.Id)
                return BadRequest();

            await _companyService.UpdateAsync(company);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(string id)
        {
            await _companyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
