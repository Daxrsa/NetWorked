using Application.Services.ResultsService;
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
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProfileMatchingResult p)
        {
            return Ok();
        }
    }
}
