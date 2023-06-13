using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;


namespace Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string? FilePath { get; set; }
        public int Likes { get; set; }

        [NotMapped]
        public IFormFile? formFile { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}