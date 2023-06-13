using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SearchController: Controller
    {
        private readonly ISearch _contract;
        public SearchController(ISearch contract) 
        {
            _contract = contract;
        }
        [HttpGet]
        public async Task<ActionResult> GetSearch(string? result)
        {
            var searchResult =await _contract.Search(result);
            return Ok(searchResult);
        }

    }
}
