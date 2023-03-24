using AutoMapper;
using Posting.Service.DTOs;
using Posting.Service.Models;

namespace Posting.Service.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Job, JobDTO>().ReverseMap();
        }
    }
}