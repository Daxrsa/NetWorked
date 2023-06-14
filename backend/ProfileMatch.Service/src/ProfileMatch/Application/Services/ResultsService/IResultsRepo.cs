using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ResultsService
{
    public interface IResultsRepo
    {
        bool Add(CreateResultDto entity);
        Task<IEnumerable<ResultReadDto>> GetAll();
        Task<bool> Delete(Guid id);
        Task<ResultReadDto> GetById(Guid id);
    }
}
