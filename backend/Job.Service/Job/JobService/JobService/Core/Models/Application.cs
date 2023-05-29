using System.ComponentModel.DataAnnotations;

namespace JobService.Core.Models
{
    public class Application
    {
        public Guid ApplicantId { get; set; }
        public DateTime DateApplied { get; set; } = DateTime.Now;
        public string ResumeUrl { get; set; }

        public int JobId { get; set; }
        public JobPosition JobPosition { get; set; }
    }
}
