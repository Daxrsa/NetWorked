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
        private readonly SearchClient _searchClient;
        private readonly SearchIndex _jobIndex;
        private readonly JobDbContext _context;
        private readonly IMapper _mapper;
        //SearchClient client = new SearchClient("ATO7HNMOJI", "YourWriteAPIKey");
        //SearchIndex index = client.InitIndex("contact");
        public SearchService(SearchClient searchClient, JobDbContext context, IMapper mapper)
        {
            _context= context;
            _mapper= mapper;
            _searchClient = searchClient;
            _jobIndex = _searchClient.InitIndex("job_positions");
        }

        public List<JobReadDto> Search(string title)
        {
            List<JobPosition> jobs = _context.JobPositions.Include("Company").ToList();
            var convertedJobs = _mapper.Map<List<JobReadDto>>(jobs);
            _jobIndex.SaveObjects(convertedJobs);

            var query = new Query(title)
            {
                Filters = $"Title:{title}",
                HitsPerPage = 10
            };

            var searchResult = _jobIndex.Search<JobReadDto>(query);
            var searchHits = searchResult.Hits;
            //var searchResults = searchHits.Select(hit => hit.Object().ToList());
            var searchResults = searchHits.Select(hit => hit).ToList();

            return searchResults;
        }

       /* public async Task IndexJobDataAsync()
        {
            var jobs = await _context.JobPositions.ToListAsync();

            var index = _searchClient.InitIndex("job_positions");

            foreach (var job in jobs)
            {
                var jobObject = new
                {
                    objectID = job.Id,
                    Title = job.Title
                    // Add other properties as needed
                };

                await index.SaveObjectAsync(jobObject);
            }

            await index.WaitTaskAsync(); // Wait for indexing to finish
        }*/


    }
}
