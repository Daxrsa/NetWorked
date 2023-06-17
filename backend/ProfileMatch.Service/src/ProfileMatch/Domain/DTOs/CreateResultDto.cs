namespace Domain.DTOs
{
    public class CreateResultDto
    {
        public string JobRequirements { get; set; }
        public string ApplicantSkills { get; set; }
        public int ApplicationId { get; set; }
        public string? ResumeReview { get; set; }
    }
}
