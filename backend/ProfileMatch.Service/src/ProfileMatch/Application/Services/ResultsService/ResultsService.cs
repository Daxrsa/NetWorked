using Domain.CreateDTOs;
using Domain.Models;
using Domain.ReadDTOs;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.ResultsService
{
    public class ResultsService : IResultsRepo
    {
        private readonly ProfileMatchDbContext _context;
        public ResultsService(ProfileMatchDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(ProfileMatchingResult entity)
        {
            var results = _context.Results.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool Add(CreateResultDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(ProfileMatchingResult entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProfileMatchingResult>> GetAll()
        {
            return await _context.Results.ToListAsync();
        }

        public Task<IEnumerable<ProfileMatchingResult>> GetById()
        {
            throw new NotImplementedException();
        }

        public Task<ResultReadDto> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ProfileMatchingResult entity)
        {
            throw new NotImplementedException();
        }

        public ProfileMatchingResult Update(Guid id, ProfileMatchingResult result)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ResultReadDto>> IResultsRepo.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
