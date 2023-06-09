using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using File.Package.FileService;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        public readonly DataContext _context;
        private readonly IFileService _fileService;

        public PostService(DataContext context, IMapper mapper, IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<Result<PostDTO>> AddPost(CreatePostDto postDto)
        {
            try
            {
                var post = _mapper.Map<Post>(postDto);
                if (postDto.formFile != null)
                {
                    var fileResult = _fileService.SaveImageAndVideo(postDto.formFile);
                    if (fileResult.Item1 == 1)
                    {
                        post.FilePath = fileResult.Item2;
                    }
                }
                _context.Posts.Add(post);
                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return Result<PostDTO>.Failure("Failed to add the post.");
                }
                var posts = _mapper.Map<PostDTO>(post);
                return Result<PostDTO>.Success(posts);
            }
            catch (Exception ex)
            {
                return Result<PostDTO>.Failure(ex.Message);
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
                if (!result)
                {
                    return Result<List<PostDTO>>.Failure("Failed to delete the post.");
                }
                var posts = await _context.Posts.ProjectTo<PostDTO>(_mapper.ConfigurationProvider).ToListAsync();
                _fileService.DeleteFile(post.FilePath);
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
                if (posts != null)
                {
                    return Result<List<PostDTO>>.Success(posts);
                }
                else
                {
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
                if (!result)
                {
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

        public async Task<Result<List<PostDTO>>> FilterPostsByUser(string username)
        {
            try
            {
                var userPosts = await _context.Posts
                    .Where(p => p.Username == username)
                    .ToListAsync();

                if (userPosts.Count == 0)
                {
                    return Result<List<PostDTO>>.Failure($"No posts found for user: {username}");
                }

                var postDtos = _mapper.Map<List<PostDTO>>(userPosts);

                return Result<List<PostDTO>>.Success(postDtos);
            }
            catch (Exception ex)
            {
                return Result<List<PostDTO>>.Failure(ex.Message);
            }
        }

        public async Task<int> GetPostCount()
        {
            int userCount = await _context.Posts.CountAsync();
            return userCount;
        }
    }
}
