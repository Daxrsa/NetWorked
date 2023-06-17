using AutoMapper;
using Domain.DTOs;
using Domain.Models;

namespace Domain.AutoMapperConfig
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<ProfileMatchingResult, ResultReadDto>();
        }
    }
}
