using PhotoHub.Models;

namespace PhotoHub.Services.Interfaces
{
    public interface IBlogPostService
    {
        Task CreateBlogPost(BlogPostModel blogPost);
        Task<IEnumerable<BlogPostModel>> GetAllBlogPosts();
        Task DeleteBlogPost(Guid id);
        Task<BlogPostModel> GetBlogPostById(Guid id);
        Task UpdateBlogPost(BlogPostModel blogPost);
        Task<IEnumerable<BlogPostModel>> GetBlogPostByAuthorId(Guid id);
    }
}