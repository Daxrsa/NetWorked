using JobService.Core.Dtos;
using JobService.Core.Dtos.Company;
using JobService.Core.Models;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompany _contract;
        public CompanyController(ICompany contract)
        {
            _contract = contract;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _contract.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Ok(await _contract.GetById(id));
        }

        [HttpPost]
        public IActionResult AddCompany([FromForm]CompanyCreateDto model)
        {
            var status = new Status();
            if(!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.StatusMessage = "Please write valid data";
                return Ok(status);
            }
            var companyResult = _contract.Add(model);
            if (companyResult)
            {
                status.StatusCode = 1;
                status.StatusMessage = "Added successfully";
            }
            else
            {
                status.StatusCode = 0;
                status.StatusMessage = "Error on adding company";
            }

            return Ok(status);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(Guid id)
        {
            var result = await _contract.Delete(id);
            var status = new Status()
            {
                StatusCode = result ? 1 : 0,
                StatusMessage = result ? "Deleted successfully": "Error while deleting data"
            };
            return Ok(status);
        }

        [HttpGet("countCompanies")]
        public async Task<ActionResult<int>> CouGetCompanyCountntUsers()
        {
            return Ok(await _contract.GetCompanyCount());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCompany(Guid id, [FromBody] CompanyReadDto company)
        {
            return Ok(_contract.Update(id, company));
        }
    }
}
