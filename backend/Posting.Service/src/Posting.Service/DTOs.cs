namespace Posting.Service
{
    public class DTOs
    {
        public record PostDTO
        (
            Guid Id,
            string Description,
            string FilePath
        );

        public record CreatePostDTO
        (
            Guid Id,
            string Description,
            string FilePath
        );

        public record UpdatePostDTO
        (
            Guid Id,
            string Description,
            string FilePath
        );
    }
}