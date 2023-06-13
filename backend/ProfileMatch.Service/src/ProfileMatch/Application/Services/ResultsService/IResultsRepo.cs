using Domain.DTOs;

namespace Application.Services.ResultsService
{
    public interface IResultsRepo
    {
        Task<IEnumerable<ResultReadDto>> GetAll();
        Task<ResultReadDto> GetById(Guid id);
        bool Add(CreateResultDto entity);
        Task<bool> Delete(Guid id);
    }
}
