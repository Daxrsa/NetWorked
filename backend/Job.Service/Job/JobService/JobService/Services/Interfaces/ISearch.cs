﻿using JobService.Core.Dtos.JobPosition;
using JobService.Core.Models;

namespace JobService.Services.Interfaces
{
    public interface ISearch
    {
        List<JobReadDto> Search(string title);
    }
}
