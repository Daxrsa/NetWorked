using JobService.Core.Dtos.JobPosition;
using JobService.Core.Models;

namespace JobService.Services.Interfaces
{
    public interface ISearch
    {
        Task<List<JobReadDto>> Search(string? title);
        Task<List<JobReadDto>> FilterByCategory(string statement);
    }
}
