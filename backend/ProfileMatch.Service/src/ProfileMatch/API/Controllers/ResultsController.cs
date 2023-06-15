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
            try
            {
                var result = await _contract.GetById(id);
                return Ok(result);
            }catch(Exception) {
                return BadRequest("No result found with id \""+id+"\"");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _contract.Delete(id);
                return Ok(result);
            }catch(Exception)
            {
                return BadRequest("No result found with the given id");
            }
            
        }
    }
}
