﻿namespace JobService.Core.Dtos.Application
{
    public class ApplicationReadDto
    {
        public Guid ApplicantId { get; set; }
        public DateTime DateApplied { get; set; }
        public string ResumeUrl { get; set; }
        public string ApplicantName { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public int Id { get; set; }
    }
}
