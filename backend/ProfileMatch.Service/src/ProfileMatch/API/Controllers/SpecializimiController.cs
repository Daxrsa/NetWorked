﻿using Domain.Models;
using Domain.TestDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SpecializimiController : ControllerBase
    {
        private readonly ProfileMatchDbContext _context;

        public SpecializimiController(ProfileMatchDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Specializimet.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var result = await _context.Specializimet.Where(i => i.Id == id).FirstOrDefaultAsync();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(AddSpecializimiDto artikulli)
        {
            var article = new Specializimi()
            {
                Name = artikulli.Name
            };
            _context.Specializimet.Add(article);
            _context.SaveChanges();
            return Ok("Added successfully");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var artikulli = _context.Specializimet.Find(id);
            _context.Specializimet.Remove(artikulli);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, AddSpecializimiDto dto)
        {
            var existingArticle = await _context.Specializimet.FindAsync(id);
            existingArticle.Name = dto.Name;

            await _context.SaveChangesAsync();
            return Ok("edited successfully");
        }

    }
}
