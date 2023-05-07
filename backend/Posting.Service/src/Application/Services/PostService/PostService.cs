using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        public readonly DataContext _context;
        public PostService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;    
        }
        public async Task<Result<List<PostDTO>>> AddPost(PostDTO postDto)
        {
             try
            {
                var post = _mapper.Map<Post>(postDto);
                _context.Posts.Add(post);
                var result = await _context.SaveChangesAsync() > 0;
                if(!result) {
                    return Result<List<PostDTO>>.Failure("Failed to add the post.");
                }
                var candidates = await _context.Posts.ProjectTo<PostDTO>(_mapper.ConfigurationProvider).ToListAsync();
                return Result<List<PostDTO>>.Success(candidates);
            }
            catch (Exception ex)
            {
                return Result<List<PostDTO>>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<PostDTO>>> DeletePost(Guid id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);
                if (post is null)
                {
                    return Result<List<PostDTO>>.Failure($"Post with id {id} not found.");
                }
                _context.Posts.Remove(post);
                var result = await _context.SaveChangesAsync() > 0;
                if(!result) {
                    return Result<List<PostDTO>>.Failure("Failed to delete the post.");
                }
                var posts = await _context.Posts.ProjectTo<PostDTO>(_mapper.ConfigurationProvider).ToListAsync();
                return Result<List<PostDTO>>.Success(posts);
            }
            catch (Exception ex)
            {
                return Result<List<PostDTO>>.Failure(ex.Message);
            }
        }

        public async Task<Result<PostDTO>> GetPostById(Guid id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);
                if (post is null)
                {
                    return Result<PostDTO>.Failure($"Post with id {id} not found.");
                }
                var postDto = _mapper.Map<PostDTO>(post);
                return Result<PostDTO>.Success(postDto);
            }
            catch (Exception ex)
            {
                return Result<PostDTO>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<PostDTO>>> GetPosts()
        {
            try
            {
                var posts = await _context.Posts.ProjectTo<PostDTO>(_mapper.ConfigurationProvider).ToListAsync();
                if (posts != null){
                    return Result<List<PostDTO>>.Success(posts);
                }else{
                    return Result<List<PostDTO>>.Failure("No posts were found");
                }
            }
            catch (Exception ex)
            {
                return Result<List<PostDTO>>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<PostDTO>>> UpdatePost(Guid id, PostDTO requestDto)
        {
             try
            {
                var post = await _context.Posts.FindAsync(id);
                if (post is null)
                {
                    return Result<List<PostDTO>>.Failure($"Post with id {id} not found.");
                }
                
                _mapper.Map(requestDto, post);

                var result = await _context.SaveChangesAsync() > 0;
                if(!result) {
                    return Result<List<PostDTO>>.Failure("Failed to update the post.");
                }
                var candidates = await _context.Posts.ProjectTo<PostDTO>(_mapper.ConfigurationProvider).ToListAsync();
                return Result<List<PostDTO>>.Success(candidates);
            }
            catch (Exception ex)
            {
                return Result<List<PostDTO>>.Failure(ex.Message);
            }
        }
    }
}