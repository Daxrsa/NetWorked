using Job.DTOs;
using Job.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Job.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICity _contract;
        public CityController(ICity contract)
        {
            _contract = contract;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            //return Ok(await _contract.GetAll());
            return Ok(await _contract.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult> AddCity(CityDTO city)
        {
            return Ok(await _contract.Add(city));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(int id)
        {
            return Ok(await _contract.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            return Ok(await _contract.Delete(id));
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> UpdateCity(int id)
        {
            return Ok(await _contract.Update(id));
        }
    }
}
