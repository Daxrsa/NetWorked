using Posting.Service.Models;

namespace Posting.Service.Repos
{
    public interface IJobRepo
    {
        Task CreateAsync(Job entity);
        Task<IReadOnlyCollection<Job>> GetAllAsync();
        Task<Job> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Job entity);
    }
}