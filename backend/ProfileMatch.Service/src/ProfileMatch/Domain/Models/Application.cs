using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateApplied { get; set; } = DateTime.Now;
        public string ResumeUrl { get; set; }

        public Guid ApplicantId { get; set; }
        public int JobId { get; set; }
    }
}
