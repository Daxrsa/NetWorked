using AutoMapper;
using Domain.CreateDTOs;
using Domain.Models;
using Domain.ReadDTOs;

namespace Domain.AutoMapperConfig
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<CreateResultDto, ProfileMatchingResult>();
            CreateMap<ProfileMatchingResult, ResultReadDto>();
        }
    }
}
