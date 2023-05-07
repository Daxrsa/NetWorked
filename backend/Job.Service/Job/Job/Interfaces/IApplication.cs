using Job.DTOs;
using Job.Models;

namespace Job.Interfaces
{
    public interface IApplication
    {
        Task<List<Application>> GetAll();
        Task<ApplicationDTO> GetById(int id);
        Task<bool> Add(ApplicationDTO entity);
        Task<bool> Delete(int id);
        Task<bool> Update(int id);
    }
}
