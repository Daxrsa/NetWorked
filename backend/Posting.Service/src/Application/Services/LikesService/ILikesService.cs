using Application.Core;
using Application.DTOs;

namespace Application.Services.LikesService
{
    public interface ILikesService
    {
        Task<Result<PostDTO>> LikePost(Guid id);
        Task<Result<PostDTO>> UnlikePost(Guid id);
    }
}