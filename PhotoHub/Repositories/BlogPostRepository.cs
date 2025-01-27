using Microsoft.EntityFrameworkCore;
using PhotoHub.Data;
using PhotoHub.Models.DBObjects;
using PhotoHub.Repositories.Interfaces;

namespace PhotoHub.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> GetByIdAsync(Guid id)
        {
            return await _context.BlogPosts.FindAsync(id);
        }
        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetAllPostsByAuthorId(Guid id)
        {
            return await _context.BlogPosts
                .Where(b => b.AuthorId == id)
                .ToListAsync();
        }

        public async Task AddAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BlogPost blogPost)
        {
            _context.BlogPosts.Update(blogPost);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var blogPost = await GetByIdAsync(id);
            if (blogPost != null)
            {
                _context.BlogPosts.Remove(blogPost);
                await _context.SaveChangesAsync();
            }
        }
    }
}
