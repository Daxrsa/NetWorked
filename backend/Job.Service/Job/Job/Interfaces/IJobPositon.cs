using Job.DTOs;
using Job.Models;

namespace Job.Interfaces
{
    public interface IJobPositon
    {
        Task<List<JobPositon>> GetAll();
        Task<JobPositonDTO> GetById(Guid id);
        Task<bool> Add(JobPositonDTO entity);
        Task<bool> Delete(Guid id);
        Task<bool> Update(Guid id);
    }
}
