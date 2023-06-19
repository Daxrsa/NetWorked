using AutoMapper;
using JobService.Core.Dtos;
using JobService.Core.Dtos.Company;
using JobService.Core.Models;
using JobService.Data;
using JobService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobService.Services
{
    public class CategoryService : ICategory
    {
        private readonly JobDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(JobDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public bool Add(CategoryDto entity)
        {
            try
            {
                var category = _mapper.Map<Category>(entity);
                _context.Categories.Add(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    throw new Exception("The given category does not exist!");
                }
                _context.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto> GetById(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                return _mapper.Map<CategoryReadDto>(category);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
