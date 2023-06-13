namespace Application.DTOs
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string? FilePath { get; set; }
        public int Likes { get; set; }
    }
}