﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoHub.Models;
using PhotoHub.Services.Interfaces;
using PhotoHub.ViewModels;
using X.PagedList.Extensions;

namespace PhotoHub.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IImageService _imageService;
        private readonly IS3Service _s3Service;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;

        public BlogPostController(IBlogPostService blogPostService, IImageService imageService, IS3Service s3Service, IUserService userService, ICommentService commentService)
        {
            _blogPostService = blogPostService;
            _imageService = imageService;
            _userService = userService;
            _s3Service = s3Service;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            var blogPosts = (await _blogPostService.GetAllBlogPosts());
            var images = (await _imageService.GetAllImages()).ToList();

            var blogPostViewModels = new List<BlogPostViewModel>();

            foreach (var bp in blogPosts)
            {
                var viewModel = new BlogPostViewModel
                {
                    IdBlogPost = bp.IdBlogPost,
                    Title = bp.Title,
                    Content = bp.Content,
                    CreatedDate = bp.CreatedDate,
                    ImageUrl = images.FirstOrDefault(img => img.IdBlogPost == bp.IdBlogPost)?.Url,
                    AuthorName = await _userService.GetUserNameById(bp.AuthorId)
                };
                blogPostViewModels.Add(viewModel);
            }
            var pagedList = blogPostViewModels.OrderByDescending(bp => bp.CreatedDate).ToPagedList(page, pageSize);

            return View(pagedList);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var blogPost = await _blogPostService.GetBlogPostById(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            var comments = await _commentService.GetCommentsByBlogPostId(id);
            var commentViewModels = comments.Select(c => new CommentViewModel
            {
                IdComment = c.IdComment,
                Content = c.Content,
                IdAuthor = c.IdUser,
                AuthorName = _userService.GetUserNameById(c.IdUser).Result,
                CreatedDate = c.CreatedDate
            }).ToList();

            var viewModel = new BlogPostDetailsViewModel
            {
                IdBlogPost = blogPost.IdBlogPost,
                Title = blogPost.Title,
                Content = blogPost.Content,
                ImageUrl = (await _imageService.GetImageByBlogPostId(blogPost.IdBlogPost))?.Url,
                IdAuthor = blogPost.AuthorId,
                AuthorName = await _userService.GetUserNameById(blogPost.AuthorId),
                CreatedDate = blogPost.CreatedDate,
                Comments = commentViewModels
            };

            if (TempData["EditingCommentId"] != null)
            {
                ViewData["EditingCommentId"] = TempData["EditingCommentId"];
            }

            return View(viewModel);
        }


        [Authorize]
        public ActionResult CreateBlogPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateBlogPost(IFormCollection collection)
        {
            try
            {
                if (collection.Files.Count == 0)
                {
                    ModelState.AddModelError("file", "Please upload an image for the blog post.");
                    return View();
                }

                var file = collection.Files["file"];

                if (file == null || file.Length == 0)
                {
                    ModelState.AddModelError("file", "The uploaded file is empty.");
                    return View();
                }

                var blogPostVM = new BlogPostViewModel
                {
                    CreatedDate = DateTime.Now,
                    AuthorName = User.Identity?.Name,
                    ImageUrl = await _s3Service.UploadFileAsync(file),
                    Title = collection["Title"],
                    Content = collection["Content"]
                };

                var blogPost = new BlogPostModel();

                blogPost.IdBlogPost = Guid.NewGuid();
                blogPost.Title = blogPostVM.Title;
                blogPost.Content = blogPostVM.Content;
                blogPost.CreatedDate = blogPostVM.CreatedDate;
                blogPost.AuthorId = Guid.Parse(User.FindFirst("UserGuid")?.Value);
                

                if(blogPost != null) 
                    await _blogPostService.CreateBlogPost(blogPost);

                var image = new ImageModel
                {
                    IdImage = Guid.NewGuid(),
                    Url = blogPostVM.ImageUrl,
                    IdBlogPost = blogPost.IdBlogPost
                };

                if(image != null)
                    await _imageService.CreateImage(image);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
