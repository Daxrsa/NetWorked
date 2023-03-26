using Common.Library;

namespace Posting.Service.Models
{
    public class Job : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string FullOrPartTime { get; set; }
        public decimal MonthlySalary { get; set; }
        public string Currency { get; set; }
    }
}