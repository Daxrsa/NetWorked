using Job.Configuration;
using Job.DTOs;
using Job.Interfaces;
using Job.Models;
using Microsoft.EntityFrameworkCore;

namespace Job.Services
{
    public class CategoryService : ICategory
    {
        private readonly JobDbContext _context;
        public CategoryService(JobDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(CategoryDTO entity)
        {
            Category category = new()
            {
                Id = entity.Id,
                Name = entity.Name
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        //Duhet me ndryshu
        public async Task<bool> Update(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetAll()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            Category c = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            CategoryDTO categoryDTO = new()
            {
                Id = id,
                Name = c.Name
            };
            return categoryDTO;
        }
    }
}
