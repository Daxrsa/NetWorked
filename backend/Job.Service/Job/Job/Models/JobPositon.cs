using System.ComponentModel.DataAnnotations;

namespace Job.Models
{
    public class JobPositon
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string SkillSet { get; set; }

        //Company
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
