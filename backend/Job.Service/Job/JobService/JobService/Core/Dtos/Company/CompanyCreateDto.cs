using JobService.Core.Enums;

namespace JobService.Core.Dtos.Company
{
    public class CompanyCreateDto
    {
        public string Name { get; set; }
        public CompanySize Size { get; set; }
        public City CityLocation { get; set; }
    }
}
