namespace Domain
{
    public class Blog 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
}