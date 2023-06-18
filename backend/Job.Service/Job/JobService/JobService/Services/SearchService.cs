using AutoMapper;
using JobService.Core.Dtos.JobPosition;
using JobService.Core.Enums;
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
            IQueryable<JobPosition> query = _context.JobPositions.Include("JobCategory").Include("Company");
            if(title is not null)
            {
                query = query.Where(t => t.ExpireDate >= DateTime.Now && (t.Title.Contains(title) || t.Requirements.Contains(title)|| t.Company.Name.Contains(title) || t.Username.Contains(title)));
            }
            var jobs = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();
            var convertedJobs = _mapper.Map<List<JobReadDto>>(jobs);
            return convertedJobs;
        }

        public async Task<List<JobReadDto>> FilterByCategory(string statement)
        {
            try
            {
                IQueryable<JobPosition> query = _context.JobPositions.Include("JobCategory").Include("Company");
                query = query.Where(j => j.JobCategory.Name.Equals(statement));
                var jobs = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();
                var convertedJobs = _mapper.Map<List<JobReadDto>>(jobs);
                return convertedJobs;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
