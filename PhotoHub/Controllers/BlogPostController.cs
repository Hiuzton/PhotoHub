using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoHub.Models;
using PhotoHub.Services.Interfaces;
using PhotoHub.ViewModels;
using System.Runtime.InteropServices;

namespace PhotoHub.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IImageService _imageService;
        private readonly IS3Service _s3Service;

        public BlogPostController(IBlogPostService blogPostService, IImageService imageService, IS3Service s3Service)
        {
            _blogPostService = blogPostService;
            _imageService = imageService;
            _s3Service = s3Service;
        }
        // GET: BlogPostController
        public async Task<IActionResult> Index()
        {
            // Fetch all blog posts and images
            var blogPosts = await _blogPostService.GetAllBlogPosts();
            var images = await _imageService.GetAllImages();

            // Create a list of BlogPostViewModel
            var blogPostViewModels = blogPosts.Select(bp => new BlogPostViewModel
            {
                Title = bp.Title,
                Content = bp.Content,
                CreatedDate = bp.CreatedDate,
                // Find associated image for the blog post (if exists)
                ImageUrl = images.FirstOrDefault(img => img.IdBlogPost == bp.IdBlogPost)?.Url,
                AuthorName = bp.Author?.Username // Assuming Author is a UserModel with Username
            }).ToList();

            return View(blogPostViewModels);
        }


        // GET: BlogPostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BlogPostController/Create
        public ActionResult CreateBlogPost()
        {
            return View();
        }

        // POST: BlogPostController/Create
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

        // GET: BlogPostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogPostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogPostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BlogPostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
