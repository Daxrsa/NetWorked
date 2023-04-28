using Domain.CreateDTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICompanyRepo
    {
        Task<IEnumerable<Company>> GetAll();
        Task<Company> GetById(Guid id);
        Task<bool> Add(CompanyDTO entity);
        Task<bool> Delete(Guid id);
        Task<bool> Update(Guid id);
    }
}
