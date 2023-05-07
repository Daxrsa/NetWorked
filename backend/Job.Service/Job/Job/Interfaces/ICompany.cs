using Job.DTOs;
using Job.Models;

namespace Job.Interfaces
{
    public interface ICompany
    {
        Task<List<Company>> GetAll();
        Task<CompanyDTO> GetById(Guid id);
        Task<bool> Add(CompanyDTO entity);
        Task<bool> Delete(Guid id);
        Task<bool> Update(Guid id);
    }
}
