using Microsoft.EntityFrameworkCore;
using PhotoHub.Data;
using PhotoHub.Models.DBObjects;
using PhotoHub.Repositories.Interfaces;

namespace PhotoHub.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var comment = await GetByIdAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<List<Comment>> GetCommentsByBlogPostId(Guid blogPostId)
        {
            return await _context.Comments
                .Where(c => c.IdBlogPost == blogPostId)
                .OrderByDescending(c => c.CreatedDate).ToListAsync();
        }
    }
}
