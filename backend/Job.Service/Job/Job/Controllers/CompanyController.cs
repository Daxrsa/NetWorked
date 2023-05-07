using Job.DTOs;
using Job.Interfaces;
using Job.Models;
using Microsoft.AspNetCore.Mvc;

namespace Job.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController:ControllerBase
    {
        private readonly ICompany _contract;
        public CompanyController(ICompany contract)
        {
            _contract = contract;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            return Ok(await _contract.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult> AddCompany(CompanyDTO company)
        {
            return Ok(await _contract.Add(company));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            return Ok(await _contract.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            return Ok(await _contract.Delete(id));
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> UpdateCompany(Guid id)
        {
            return Ok(await _contract.Update(id));
        }
    }
}
