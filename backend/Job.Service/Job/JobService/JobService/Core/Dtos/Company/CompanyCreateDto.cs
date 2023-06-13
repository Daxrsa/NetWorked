using JobService.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobService.Core.Dtos.Company
{
    public class CompanyCreateDto
    {
        public string Name { get; set; }
        public CompanySize Size { get; set; }
        public City CityLocation { get; set; }

        //[NotMapped]
        public IFormFile? file { get; set; }
    }
}
