using System.ComponentModel.DataAnnotations;

namespace Job.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public virtual ICollection<JobPositon> JobPositions { get; set; }
    }
}
