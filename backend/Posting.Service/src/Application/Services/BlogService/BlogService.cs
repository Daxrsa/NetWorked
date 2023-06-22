using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.BlogService
{
    public class BlogService : IBlogService
    {
        private readonly DataContext _context;
        public BlogService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetBlogs()
        {
            var jobs = await _context.Blogs.ToListAsync();
            return jobs;
        }

        public async Task<Blog> GetSingleBlog(Guid id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            return blog;
        }

        public async Task<Blog> AddBlog(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog> EditBlog(Guid id, Blog blog)
        {
            var existingBlog = await _context.Blogs.FindAsync(id);

            if (existingBlog == null)
            {
                return null;
            }

            existingBlog.Title = blog.Title;
            existingBlog.Body = blog.Body;
            existingBlog.CreatedAt = blog.CreatedAt;

            await _context.SaveChangesAsync();

            return existingBlog;
        }

        public async Task<Blog> DeleteBlog(Guid id)
        {
            var existingBlog = await _context.Blogs.FindAsync(id);

            if (existingBlog == null)
            {
                return null;
            }

            _context.Blogs.Remove(existingBlog);
            await _context.SaveChangesAsync();

            return existingBlog;
        }
    }
}