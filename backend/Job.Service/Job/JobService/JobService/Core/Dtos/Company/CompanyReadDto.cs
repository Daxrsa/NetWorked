using JobService.Core.Enums;

namespace JobService.Core.Dtos.Company
{
    public class CompanyReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CompanySize Size { get; set; }
        public City CityLocation { get; set; }
        public string Logo { get; set; }
    }
}
