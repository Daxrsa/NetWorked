using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile? formFile { get; set; }
    }
}
