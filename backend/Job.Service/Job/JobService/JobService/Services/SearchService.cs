using Algolia.Search.Clients;
using Algolia.Search.Http;
using Algolia.Search.Models.Search;
using AutoMapper;
using JobService.Core.Dtos.JobPosition;
using JobService.Core.Models;
using JobService.Data;
using JobService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobService.Services
{
    public class SearchService: ISearch
    {
        private readonly JobDbContext _context;
        private readonly IMapper _mapper;
        public SearchService( JobDbContext context, IMapper mapper)
        {
            _context= context;
            _mapper= mapper;
        }

        public async Task<List<JobReadDto>> Search(string? title)
        {
            IQueryable<JobPosition> query = _context.JobPositions;
            if(title is not null)
            {
                query = query.Where(t => t.Title.Contains(title));
            }
            var jobs = await query.ToListAsync();
            var convertedJobs = _mapper.Map<List<JobReadDto>>(jobs);
            return convertedJobs;
        }
    }
}
