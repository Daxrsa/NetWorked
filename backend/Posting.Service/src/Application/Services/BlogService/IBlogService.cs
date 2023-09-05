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
        Task<Blog> AddContentToBlog(Guid blogId, Content newContent);
        Task<Content> DeleteContent(Guid contentId);
        Task<Content> EditContent(Guid contentId, string newBody);
        Task<List<Content>> GetAllContent();
        Task<Content> GetContentById(Guid Id);
    }
}