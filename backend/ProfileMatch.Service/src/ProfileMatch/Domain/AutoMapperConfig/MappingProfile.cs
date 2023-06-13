using AutoMapper;
using Domain.DTOs;
using Domain.Models;

namespace Domain.AutoMapperConfig
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            //CreateMap<CreateResultDto, ProfileMatchingResult>();
            CreateMap<ProfileMatchingResult, ResultReadDto>();
        }
    }
}
