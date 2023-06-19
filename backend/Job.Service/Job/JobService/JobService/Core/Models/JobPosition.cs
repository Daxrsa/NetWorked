using JobService.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobService.Core.Models
{
    public class JobPosition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ExpireDate { get; set;}
        [Required]
        public string Requirements { get; set; }
     
        public JobLevel JobLevel { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public int CategoryId { get; set; }
        public Category JobCategory { get; set; }

        public ICollection<Application> Applications { get; set; }

        public string Username { get; set; }
    }
}
