namespace Domain.DTOs
{
    public class CreateResultDto
    {
        public string JobRequirements { get; set; }
        public string ApplicantSkills { get; set; }
        public string? ResumeReview { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
