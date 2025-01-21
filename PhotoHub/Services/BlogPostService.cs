using PhotoHub.Models;
using PhotoHub.Models.DBObjects;
using PhotoHub.Repositories.Interfaces;
using PhotoHub.Services.Interfaces;

namespace PhotoHub.Service
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public async Task<BlogPostModel> GetBlogPostById(Guid id)
        {
            var dbBlogPost = await _blogPostRepository.GetByIdAsync(id);
            return dbBlogPost == null ? null : MapToBusinessModel(dbBlogPost);
        }

        public async Task CreateBlogPost(BlogPostModel blogPost)
        {
            var dbBlogPost = MapToDbModel(blogPost);
            await _blogPostRepository.AddAsync(dbBlogPost);
        }

        public async Task UpdateBlogPost(BlogPostModel blogPost)
        {
            var dbBlogPost = MapToDbModel(blogPost);
            await _blogPostRepository.UpdateAsync(dbBlogPost);
        }

        public async Task DeleteBlogPost(Guid id)
        {
            await _blogPostRepository.DeleteAsync(id);
        }

        private BlogPostModel MapToBusinessModel(BlogPost dbBlogPost)
        {
            return new BlogPostModel
            {
                IdBlogPost = dbBlogPost.IdBlogPost,
                Title = dbBlogPost.Title,
                Content = dbBlogPost.Content,
                AuthorId = dbBlogPost.AuthorId,
            };
        }

        private BlogPost MapToDbModel(BlogPostModel blogPostModel)
        {
            return new BlogPost
            {
                IdBlogPost = blogPostModel.IdBlogPost,
                Title = blogPostModel.Title,
                Content = blogPostModel.Content,
                AuthorId = blogPostModel.AuthorId
            };
        }
    }
}
