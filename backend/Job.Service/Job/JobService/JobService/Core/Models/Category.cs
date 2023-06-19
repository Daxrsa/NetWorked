using System.ComponentModel.DataAnnotations;

namespace JobService.Core.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<JobPosition> jobPositions { get; set; }
    }
}
