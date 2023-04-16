using Application.Core;
using Application.DTOs;

namespace Application.Services.PostService
{
    public interface IPostService
    {
        Task<Result<List<PostDTO>>> GetPosts();
        Task<Result<PostDTO>> GetPostById(Guid id);
        Task<Result<List<PostDTO>>> AddPost(PostDTO postDto);
        Task<Result<List<PostDTO>>> UpdatePost(Guid id, PostDTO postDto);
        Task<Result<List<PostDTO>>> DeletePost(Guid id);
    }
}