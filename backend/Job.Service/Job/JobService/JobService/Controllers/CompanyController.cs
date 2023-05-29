using JobService.Core.Dtos.Company;
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
        public ActionResult AddCompany(CompanyCreateDto dto)
        {
            _contract.Add(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(Guid id)
        {
            return Ok(await _contract.Delete(id));
        }

    }
}
