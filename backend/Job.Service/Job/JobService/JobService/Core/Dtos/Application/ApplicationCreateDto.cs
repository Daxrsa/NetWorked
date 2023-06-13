namespace JobService.Core.Dtos.Application
{
    public class ApplicationCreateDto
    {
        public Guid ApplicantId { get; set; }
        public int JobId { get; set; }
        public string ApplicantName { get; set; }
    }
}
