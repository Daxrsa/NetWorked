using Application.Core;
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
        private readonly CalculateMatch _calculate;
        public ResultsController(IResultsRepo contract, CalculateMatch calculate)
        {
            _contract = contract;
            _calculate = calculate;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            Console.WriteLine(_calculate.GetScore());
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProfileMatchingResult p)
        {
            return Ok();
        }
    }
}
