using Application.Core;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence;
using System.Text.Json.Serialization;

namespace Application.Services.ResultsService
{
    public class ResultsService : IResultsRepo
    {
        private readonly ProfileMatchDbContext _context;
        private readonly IMapper _mapper;
        private readonly CalculateMatch _calculate;
        public ResultsService(ProfileMatchDbContext context, IMapper mapper, CalculateMatch calculate)
        {
            _context = context;
            _mapper = mapper;
            _calculate = calculate;
        }

        public bool Add(CreateResultDto entity)
        {
            try
            {
                int review = _calculate.GetReview(entity.ResumeReview);
                int similarities = _calculate.CountSimilarities(entity.JobRequirements, entity.ApplicantSkills);
                double result = _calculate.GetPercentage(similarities, entity.JobRequirements, review);
                var MatchingResult = new ProfileMatchingResult()
                {
                    Result = result,
                    ApplicationId = entity.ApplicationId
                };
                _context.Results.Add(MatchingResult);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<ResultReadDto>> GetAll()
        {
            var results = await _context.Results.ToListAsync();
            var convertedResults = _mapper.Map<IEnumerable<ResultReadDto>>(results);
            return convertedResults;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _context.Results.FindAsync(id);
                if (result is null)
                {
                    throw new Exception("The given result does not exist");
                }
                _context.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResultReadDto> GetById(Guid id)
        {
            try
            {
                var result = await _context.Results.Where(result => result.Id.Equals(id)).FirstOrDefaultAsync();
                var convertedResult = _mapper.Map<ResultReadDto>(result);
                return convertedResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
