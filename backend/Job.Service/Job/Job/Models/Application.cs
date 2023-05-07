using System.ComponentModel.DataAnnotations;

namespace Job.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateApplied { get; set; }

        //Job
        public Guid JobId { get; set; }
        public JobPositon JobPosition { get; set; }

        //Applicant
    }
}
