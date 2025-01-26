using Microsoft.EntityFrameworkCore;
using PhotoHub.Models;
using PhotoHub.Models.DBObjects;
using PhotoHub.Repositories.Interfaces;
using PhotoHub.Services.Interfaces;

namespace PhotoHub.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentModel> GetCommentById(Guid id)
        {
            var dbComment = await _commentRepository.GetByIdAsync(id);
            return dbComment == null ? null : MapToBusinessModel(dbComment);
        }

        public async Task<IEnumerable<CommentModel>> GetCommentsByBlogPostId(Guid id)
        {
            var dbComment = await _commentRepository.GetCommentsByBlogPostId(id);
            return dbComment.Select(MapToBusinessModel);
        }

        public async Task CreateComment(CommentModel commentModel)
        {
            var dbComment = MapToDbModel(commentModel);
            await _commentRepository.AddAsync(dbComment);
        }

        public async Task UpdateComment(CommentModel commentModel)
        {
            var dbComment = MapToDbModel(commentModel);
            await _commentRepository.UpdateAsync(dbComment);
        }

        public async Task DeleteComment(Guid id)
        {
            await _commentRepository.DeleteAsync(id);
        }

        private CommentModel MapToBusinessModel(Comment dbCommment)
        {
            return new CommentModel
            {
                IdComment = dbCommment.IdComment,
                Content = dbCommment.Content,
                CreatedDate = dbCommment.CreatedDate,
                IdBlogPost = dbCommment.IdBlogPost,
                IdUser = dbCommment.IdUser,
            };
        }

        private Comment MapToDbModel(CommentModel commentModel)
        {
            return new Comment
            {
                IdComment = commentModel.IdComment,
                Content = commentModel.Content,
                CreatedDate = commentModel.CreatedDate,
                IdBlogPost = commentModel.IdBlogPost,
                IdUser = commentModel.IdUser,
            };
        }
    }
}
