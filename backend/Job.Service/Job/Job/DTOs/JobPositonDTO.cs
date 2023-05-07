namespace Job.DTOs
{
    public class JobPositonDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string SkillSet { get; set; }
    }
}
