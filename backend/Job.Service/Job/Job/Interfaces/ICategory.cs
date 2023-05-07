using Job.DTOs;
using Job.Models;

namespace Job.Interfaces
{
    public interface ICategory
    {
        Task<List<Category>> GetAll();
        Task<CategoryDTO> GetById(int id);
        Task<bool> Add(CategoryDTO entity);
        Task<bool> Delete(int id);
        Task<bool> Update(int id);
    }
}
