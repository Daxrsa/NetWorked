﻿using AutoMapper;
using JobService.Core.Dtos;
using JobService.Core.Dtos.Application;
using JobService.Core.Dtos.Company;
using JobService.Core.Dtos.JobPosition;
using JobService.Core.Models;

namespace JobService.Core.AutoMapperConfig
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            //Company mapping
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<Company, CompanyReadDto>();

            //JobPosition mapping
            CreateMap<JobCreateDto, JobPosition>();
            CreateMap<JobPosition, JobReadDto>()
                .ForMember(dest => dest.CompanyName,
                opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.CompanyLogo,
                opt => opt.MapFrom(src => src.Company.Logo))
                .ForMember(dest => dest.JobCategory,
                opt => opt.MapFrom(src => src.JobCategory.Name));

            //Application mapping
            CreateMap<ApplicationCreateDto, Application>();
            CreateMap<Application, ApplicationReadDto>()
                .ForMember(dest => dest.JobTitle,
                opt => opt.MapFrom(src => src.JobPosition.Title));

            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryReadDto, Category>().ReverseMap();
        }
    }
}
