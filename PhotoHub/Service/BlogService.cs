using Microsoft.EntityFrameworkCore;
using PhotoHub.Models;
using ProgrammingClub.Data;

namespace PhotoHub.Service
{
    public class BlogService
    {
        private readonly ApplicationDbContext _context;

        public BlogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BlogPostModel>> GetAllBlogsAsync()
        {
            return await _context.BlogPosts.Include(b => b.Author).ToListAsync();
        }

        public async Task<BlogPostModel> GetBlogByIdAsync(Guid id)
        {
            return await _context.BlogPosts.Include(b => b.Author).FirstOrDefaultAsync(b => b.IdBlogPost == id);
        }

        public async Task<BlogPostModel> CreateBlogAsync(BlogPostModel BlogPostModel)
        {
            _context.BlogPosts.Add(BlogPostModel);
            await _context.SaveChangesAsync();
            return BlogPostModel;
        }

        public async Task UpdateBlogAsync(BlogPostModel BlogPostModel)
        {
            _context.BlogPosts.Update(BlogPostModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBlogAsync(Guid id)
        {
            var BlogPostModel = await _context.BlogPosts.FindAsync(id);
            if (BlogPostModel != null)
            {
                _context.BlogPosts.Remove(BlogPostModel);
                await _context.SaveChangesAsync();
            }
        }
    }
}
