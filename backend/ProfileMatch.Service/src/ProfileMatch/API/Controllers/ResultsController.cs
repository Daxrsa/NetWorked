using Application.Core;
using Application.Services.ResultsService;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly IResultsRepo _contract;
        public ResultsController(IResultsRepo contract)
        {
            _contract = contract;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _contract.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _contract.GetById(id));
        }

        [HttpPost]
        public IActionResult Add([FromForm]CreateResultDto dto)
        {
            var result = _contract.Add(dto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _contract.Delete(id));
        }
    }
}
