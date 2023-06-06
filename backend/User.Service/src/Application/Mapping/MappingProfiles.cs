using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, User>();
            CreateMap<EditUserDTO, User>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}