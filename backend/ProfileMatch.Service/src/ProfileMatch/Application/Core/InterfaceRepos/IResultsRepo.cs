using Domain.CreateDTOs;
using Domain.Models;
using Domain.ReadDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Core.InterfaceRepos
{
    public interface IResultsRepo 
    {
        Task<IEnumerable<ResultReadDto>> GetAll();
        Task<ResultReadDto> GetById(Guid id);
        bool Add(CreateResultDto entity);
        Task<bool> Delete(Guid id);
        ProfileMatchingResult Update(Guid id, ProfileMatchingResult result);
    }
}
