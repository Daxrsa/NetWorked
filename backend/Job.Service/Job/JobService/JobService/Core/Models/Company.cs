using JobService.Core.Enums;
using System.ComponentModel.DataAnnotations;

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
        public byte[] Logo { get; set; }

        //JOBS HERE
        public ICollection<JobPosition> jobPositions { get; set; }
    }
}
