namespace JobService.Core.Dtos.Application
{
    public class ApplicationCreateDto
    {
        public int Id { get; set; }
        public Guid ApplicantId { get; set; }
        public int JobId { get; set; }
        public string ApplicantName { get; set; }
    }
}
