namespace Domain.DTOs
{
    public class CreateResultDto
    {
        public List<string> JobRequirements { get; set; }
        public List<string> ApplicantSkills { get; set; }
        public string? ResumeReview { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
