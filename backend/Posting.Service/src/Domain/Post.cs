using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string? FilePath { get; set; }

        [NotMapped]
        public IFormFile? formFile { get; set; }
    }
}