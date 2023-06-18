using JobService.Core.Dtos.JobPosition;
using JobService.Core.Models;

namespace JobService.Services.Interfaces
{
    public interface IJobPosition
    {
        Task<IEnumerable<JobReadDto>> GetAll();
        Task<JobReadDto> GetById(int id);
        Task<bool> Add(JobCreateDto dto, string authorizationHeader);
        Task<bool> Delete(int id);
        JobReadDto Update(int id, JobReadDto job);
        Task<int> GetJobPositionCount();
        Task<IEnumerable<JobReadDto>> GetAllDashboard();
        Task<IEnumerable<JobReadDto>> GetByRecruiterId(string authorizationHeader);
    }
}
