using JobService.Core.Enums;

namespace JobService.Core.Dtos.JobPosition
{
    public class JobReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public JobCategory JobCategory { get; set; }
        public JobLevel JobLevel { get; set; }
        public Guid CompanyId { get; set;}
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
