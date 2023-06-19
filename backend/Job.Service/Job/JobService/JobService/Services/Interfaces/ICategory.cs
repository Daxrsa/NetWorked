using JobService.Core.Dtos;
using JobService.Core.Models;

namespace JobService.Services.Interfaces
{
    public interface ICategory
    {
        Task<IEnumerable<CategoryReadDto>> GetAll();
        Task<CategoryReadDto> GetById(int id);
        bool Add(CategoryDto entity);
        Task<bool> Delete(int id);
    }
}
