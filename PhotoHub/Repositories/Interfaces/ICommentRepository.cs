using PhotoHub.Models.DBObjects;

namespace PhotoHub.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task DeleteAsync(Guid id);
        Task<Comment> GetByIdAsync(Guid id);
        Task UpdateAsync(Comment comment);
    }
}