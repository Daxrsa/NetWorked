using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence;

namespace Application.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        public readonly DataContext _context;
        public CommentService(DataContext context, IMapper mapper, HttpClient httpClient)
        {
            _context = context;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task<Result<List<CommentDTO>>> DeleteComment(int id)
        {
            try
            {
                var comment = await _context.Comments.FindAsync(id);
                if (comment is null)
                {
                    return Result<List<CommentDTO>>.Failure($"Comment with id {id} not found.");
                }
                _context.Comments.Remove(comment);
                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return Result<List<CommentDTO>>.Failure("Failed to delete the comment.");
                }
                var comments = await _context.Comments.ProjectTo<CommentDTO>(_mapper.ConfigurationProvider).ToListAsync();
                return Result<List<CommentDTO>>.Success(comments);
            }
            catch (Exception ex)
            {
                return Result<List<CommentDTO>>.Failure(ex.Message);
            }
        }

        public async Task<Result<CommentDTO>> GetCommentById(int id)
        {
            try
            {
                var comment = await _context.Comments.FindAsync(id);
                if (comment is null)
                {
                    return Result<CommentDTO>.Failure($"Comment with id {id} not found.");
                }
                var commentDTO = _mapper.Map<CommentDTO>(comment);
                return Result<CommentDTO>.Success(commentDTO);
            }
            catch (Exception ex)
            {
                return Result<CommentDTO>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<CommentDTO>>> GetComments(Guid postId)
        {
            try
            {
                var comments = await _context.Comments
                    .Where(x => x.Post.Id == postId)
                    .OrderBy(x => x.CreatedAt)
                    .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<CommentDTO>>.Success(comments);
            }
            catch (Exception ex)
            {
                return Result<List<CommentDTO>>.Failure(ex.Message);
            }

        }

        public async Task<Result<List<CommentDTO>>> UpdateComment(int id, CommentDTO requestDto)
        {
            try
            {
                var comment = await _context.Comments.FindAsync(id);
                if (comment is null)
                {
                    return Result<List<CommentDTO>>.Failure($"Comment with id {id} not found.");
                }

                _mapper.Map(requestDto, comment);

                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return Result<List<CommentDTO>>.Failure("Failed to update the comment.");
                }
                var comments = await _context.Comments.ProjectTo<CommentDTO>(_mapper.ConfigurationProvider).ToListAsync();
                return Result<List<CommentDTO>>.Success(comments);
            }
            catch (Exception ex)
            {
                return Result<List<CommentDTO>>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<CommentDTO>>> AddCommentToPost(Guid postId, CommentDTO commentDto) //Index was outside the bounds of the array.
        {
            try
            {
                var post = await _context.Posts.FindAsync(postId);

                if (post == null)
                {
                    return Result<List<CommentDTO>>.Failure($"Post with ID {postId} not found.");
                }

                var comment = _mapper.Map<Comment>(commentDto);
                comment.Post = post;
                _context.Comments.Add(comment);

                await _context.SaveChangesAsync();

                try
                {
                    var comments = await _context.Comments
                        .Where(c => c.PostId == postId)
                        .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync();

                    return Result<List<CommentDTO>>.Success(comments);
                }
                catch (ArgumentOutOfRangeException)
                {
                    return Result<List<CommentDTO>>.Failure("An error occurred while retrieving comments for the post.");
                }
            }
            catch (Exception ex)
            {
                return Result<List<CommentDTO>>.Failure(ex.Message);
            }
        }

    }
}