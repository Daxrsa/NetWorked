using Domain;

namespace Application.Services.BlogService
{
    public interface IBlogService
    {
        Task<List<Blog>> GetBlogs();
        Task<Blog> GetSingleBlog(Guid id);
        Task<Blog> AddBlog(Blog blog);
        Task<Blog> EditBlog(Guid id, Blog blog);
        Task<Blog> DeleteBlog(Guid id);
    }
}