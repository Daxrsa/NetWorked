using Domain.DTOs;

namespace Application.Services.ResultsService
{
    public interface IResultsRepo
    {
        bool Add(CreateResultDto entity);
        Task<IEnumerable<ResultReadDto>> GetAll();
        Task<bool> Delete(Guid id);
        Task<ResultReadDto> GetById(Guid id);
        Task<ResultReadDto> GetByApplicantId(int id);
    }
}
