using PhotoHub.Models;

namespace PhotoHub.Services.Interfaces
{
    public interface ICommentService
    {
        Task CreateComment(CommentModel commentModel);
        Task DeleteComment(Guid id);
        Task<CommentModel> GetCommentById(Guid id);
        Task UpdateComment(CommentModel commentModel);
    }
}