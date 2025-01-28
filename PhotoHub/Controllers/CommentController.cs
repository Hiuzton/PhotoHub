using Microsoft.AspNetCore.Mvc;
using PhotoHub.Models;
using PhotoHub.Models.DBObjects;
using PhotoHub.Services;
using PhotoHub.Services.Interfaces;
using PhotoHub.ViewModels;
using System.Security.Claims;

namespace PhotoHub.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IBlogPostService _blogPostService;
        private readonly IImageService _imageService;

        public CommentController(ICommentService commentService, IUserService userService, IBlogPostService blogPostService, IImageService imageService)
        {
            _commentService = commentService;
            _userService = userService;
            _blogPostService = blogPostService;
            _imageService = imageService;
        }

        // Add Comment
        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst("UserGuid")?.Value;

                if (userId == null)
                {
                    return Unauthorized();
                }

                var comment = new CommentModel
                {
                    IdComment = Guid.NewGuid(),
                    IdBlogPost = model.IdBlogPost,
                    IdUser = Guid.Parse(userId),
                    Content = model.Content,
                    CreatedDate = DateTime.UtcNow
                };

                await _commentService.CreateComment(comment);

                return RedirectToAction("Details", "BlogPost", new { id = model.IdBlogPost });
            }

            return RedirectToAction("Details", "BlogPost", new { id = model.IdBlogPost });
        }

        [HttpGet]
        public async Task<IActionResult> EditComment(Guid id)
        {
            TempData["EditingCommentId"] = id;

            var comment = await _commentService.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }

            return RedirectToAction("Details", "BlogPost", new { id = comment.IdBlogPost });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditComment(EditCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = await _commentService.GetCommentById(model.IdComment);
                if (comment == null)
                {
                    return NotFound();
                }

                comment.Content = model.Content;
                comment.CreatedDate = DateTime.UtcNow;

                await _commentService.UpdateComment(comment);
            }

            return RedirectToAction("Details", "BlogPost", new { id = model.IdBlogPost });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var comment = await _commentService.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }

            await _commentService.DeleteComment(id);

            return RedirectToAction("Details", "BlogPost", new { id = comment.IdBlogPost });
        }
    }
}
