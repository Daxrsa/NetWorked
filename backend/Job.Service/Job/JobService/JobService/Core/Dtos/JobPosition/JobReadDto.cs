using JobService.Core.Enums;
using Newtonsoft.Json;

namespace JobService.Core.Dtos.JobPosition
{
    public class JobReadDto
    {
        [JsonPropertyAttribute("objectID")]
        public string ObjectId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public JobCategory JobCategory { get; set; }
        public JobLevel JobLevel { get; set; }
        public Guid CompanyId { get; set;}
        public string CompanyName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
