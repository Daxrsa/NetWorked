using JobService.Core.Dtos.Company;
using JobService.Core.Models;

namespace JobService.Services.Interfaces
{
    public interface ICompany
    {
        Task<IEnumerable<CompanyReadDto>> GetAll();
        Task<CompanyReadDto> GetById(Guid id);
        bool Add(CompanyCreateDto entity);
        Task<bool> Delete(Guid id);
        Company Update(Guid id, Company company);
        Task<int> GetCompanyCount();
    }
}
