using JobService.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobService.Core.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public CompanySize Size { get; set; }
        public City CityLocation { get; set; }
        public string? Logo { get; set; }

        [NotMapped]
        public IFormFile? ImageFile {get; set;}

        //JOBS HERE
        public ICollection<JobPosition> jobPositions { get; set; }
    }
}
