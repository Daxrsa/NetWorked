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
        CompanyReadDto Update(Guid id, CompanyReadDto company);
        Task<int> GetCompanyCount();
    }
}
