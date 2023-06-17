using Application.Services.ResultsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    [AllowAnonymous]
    public class ResultsController : ControllerBase
    {
        private readonly IResultsRepo _contract;
        public ResultsController(IResultsRepo contract)
        {
            _contract = contract;
        }

        [HttpGet]
        [Authorize(Roles ="Recruiter")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _contract.GetAll());
            }catch(Exception)
            {
                return BadRequest("Bad request");
            }
            
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Recruiter")]
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
        [Authorize(Roles ="Recruiter")]
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
