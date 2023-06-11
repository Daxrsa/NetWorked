using Application.Core;
using Application.DTOs;
using AutoMapper;
using Persistence;

namespace Application.Services.LikesService
{
    public class LikesService : ILikesService
    {
        private readonly IMapper _mapper;
        public readonly DataContext _context;
        public LikesService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<PostDTO>> LikePost(Guid id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);

                if (post == null)
                    return Result<PostDTO>.Failure($"Post with id{id} not found.");

                post.Likes++;
                await _context.SaveChangesAsync();

                var result = _mapper.Map<PostDTO>(post);
                return Result<PostDTO>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<PostDTO>.Failure(ex.Message);
            }
        }

        public async Task<Result<PostDTO>> UnlikePost(Guid id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);

                if (post == null)
                    return Result<PostDTO>.Failure($"Post with id{id} not found.");

                post.Likes--;
                await _context.SaveChangesAsync();

                var result = _mapper.Map<PostDTO>(post);
                return Result<PostDTO>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<PostDTO>.Failure(ex.Message);
            }
        }
    }
}