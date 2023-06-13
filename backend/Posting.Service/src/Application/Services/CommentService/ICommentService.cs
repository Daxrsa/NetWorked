using Application.Core;
using Application.DTOs;

namespace Application.Services.CommentService
{
    public interface ICommentService
    {
//        Task<Result<List<CommentDTO>>> AddComment(Guid postId, CommentDTO commentDto);
        Task<Result<List<CommentDTO>>> DeleteComment(int id);
        Task<Result<CommentDTO>> GetCommentById(int id);
        Task<Result<List<CommentDTO>>> GetComments(Guid postId);
        Task<Result<List<CommentDTO>>> UpdateComment(int id, CommentDTO requestDto);
    }
}