
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobService.Core.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        public Guid ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public DateTime DateApplied { get; set; } = DateTime.Now;
        public string ResumeUrl { get; set; }

        public int JobId { get; set; }
        public JobPosition JobPosition { get; set; }
    }
}
