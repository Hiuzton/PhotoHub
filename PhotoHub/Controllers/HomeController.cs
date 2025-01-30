using Microsoft.AspNetCore.Mvc;
using PhotoHub.Models.DBObjects;
using PhotoHub.Services;
using PhotoHub.Services.Interfaces;
using PhotoHub.ViewModels;

namespace PhotoHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IUserService _userService;
        private readonly IImageService _imageService;

        public HomeController(IBlogPostService blogPostService, IUserService userService, IImageService imageService)
        {
            _blogPostService = blogPostService;
            _userService = userService;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = (await _blogPostService.GetAllBlogPosts())
                .OrderByDescending(bp => bp.CreatedDate)
                .Take(6);
            var images = (await _imageService.GetAllImages()).ToList();

            var blogPostViewModels = new List<BlogPostViewModel>();

            foreach (var bp in blogPosts)
            {
                var blogPostViewModel = new BlogPostViewModel
                {
                    IdBlogPost = bp.IdBlogPost,
                    Title = bp.Title,
                    Content = bp.Content.Length > 150 ? bp.Content.Substring(0, 150) + "..." : bp.Content,
                    CreatedDate = bp.CreatedDate,
                    ImageUrl = images.FirstOrDefault(img => img.IdBlogPost == bp.IdBlogPost)?.Url,
                    AuthorName = await _userService.GetUserNameById(bp.IdAuthor)
                };
                blogPostViewModels.Add(blogPostViewModel);
            }

            var allUsers = await _userService.GetAllUsersAsync();
            var randomUsers = allUsers.OrderBy(u => Guid.NewGuid()).Take(4).ToList();
            var usersViewModel = randomUsers.Select(r => new UserListViewModel
            {
                IdUser = r.IdUser,
                Username = r.Username
            }).ToList();

            var viewModel = new HomeViewModel
            {
                LatestPosts = blogPostViewModels,
                RandomUsers = usersViewModel
            };

            return View(viewModel);
        }
    }
}
