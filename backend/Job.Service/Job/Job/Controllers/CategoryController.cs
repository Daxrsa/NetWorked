using Job.DTOs;
using Job.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Job.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController:ControllerBase
    {
        private readonly ICategory _contract;
        public CategoryController(ICategory contract)
        {
            _contract = contract;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            //return Ok(await _contract.GetAll());
            return Ok(await _contract.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult> AddCategories(CategoryDTO city)
        {
            return Ok(await _contract.Add(city));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok(await _contract.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return Ok(await _contract.Delete(id));
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            return Ok(await _contract.Update(id));
        }
    }
}
