using Posting.Service.Models;
using static Posting.Service.DTOs;

namespace Posting.Service
{
    public static class Extensions
    {
        public static PostDTO AsDto(this Post post)
        {
            return new PostDTO(post.Id, post.Description, post.FilePath);
        }
    }
}