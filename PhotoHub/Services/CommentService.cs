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
            var dbCommment = await _commentRepository.GetByIdAsync(id);
            return dbCommment == null ? null : MapToBusinessModel(dbCommment);
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
                BlogPost = dbCommment.BlogPost,
                IdBlogPost = dbCommment.IdBlogPost,
                IdUser = dbCommment.IdUser,
                User = dbCommment.User
            };
        }

        private Comment MapToDbModel(CommentModel commentModel)
        {
            return new Comment
            {
                IdComment = commentModel.IdComment,
                Content = commentModel.Content,
                CreatedDate = commentModel.CreatedDate,
                BlogPost = commentModel.BlogPost,
                IdBlogPost = commentModel.IdBlogPost,
                IdUser = commentModel.IdUser,
                User = commentModel.User
            };
        }
    }
}
