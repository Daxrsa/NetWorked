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

        public async Task<Blog> AddContentToBlog(Guid blogId, Content newContent)
        {
            // Retrieve the blog from the database
            var blog = await _context.Blogs.FindAsync(blogId);

            if (blog != null)
            {
                // Initialize the Contents collection if it's null
                if (blog.Contents == null)
                    blog.Contents = new List<Content>();

                // Add the new content to the Contents collection
                blog.Contents.Add(newContent);

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            return blog;
        }

        public async Task<Content> DeleteContent(Guid contentId)
        {
            // Retrieve the content from the database
            var content = await _context.Contents.FindAsync(contentId);

            if (content != null)
            {
                // Remove the content from the database context
                _context.Contents.Remove(content);

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            return content;
        }

        public async Task<Content> EditContent(Guid contentId, string newBody)
        {

            // Retrieve the content from the database
            var content = await _context.Contents.FindAsync(contentId);

            if (content != null)
            {
                // Update the content's text with the new value
                content.Body = newBody;

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }

            return content;
        }

        public async Task<List<Content>> GetAllContent()
        {
            // Retrieve all content from the database
            var allContent = await _context.Contents.ToListAsync();

            return allContent;
        }

        public async Task<Content> GetContentById(Guid Id)
        {
            // Retrieve the content from the database by its ID
            var content = await _context.Contents.FindAsync(Id);

            return content;
        }
    }
}