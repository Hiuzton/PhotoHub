using PhotoHub.Models;

namespace PhotoHub.Services.Interfaces
{
    public interface IBlogPostService
    {
        Task CreateBlogPost(BlogPostModel blogPost);
        Task DeleteBlogPost(Guid id);
        Task<BlogPostModel> GetBlogPostById(Guid id);
        Task UpdateBlogPost(BlogPostModel blogPost);
    }
}