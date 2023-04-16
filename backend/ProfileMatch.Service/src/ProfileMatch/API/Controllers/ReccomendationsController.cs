using Application.Core.InterfaceRepos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReccomendationsController : ControllerBase
    {
        private readonly IRecommendationsRepo _contract;
        public ReccomendationsController(IRecommendationsRepo contract)
        {
            _contract = contract;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok( await _contract.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Reccomendation r)
        {
            return Ok(await _contract.Add(r));
        }
    }
}
