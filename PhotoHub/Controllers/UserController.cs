using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PhotoHub.Models;
using PhotoHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using PhotoHub.Models.DBObjects;
using PhotoHub.ViewModels;

namespace PhotoHub.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBlogPostService _blogPostService;
        private readonly IImageService _imageService;

        public UserController(IUserService userService, IBlogPostService blogPostService, IImageService imageService)
        {
            _userService = userService;
            _blogPostService = blogPostService;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            var users = (await _userService.GetAllUsersAsync()).ToList();
            var blogPosts = (await _blogPostService.GetAllBlogPosts()).ToList();


            var userViewModeList = new List<UserListViewModel>();
            foreach (var user in users)
            {
                var userModel = new UserListViewModel
                {
                    IdUser = user.IdUser,
                    Username = user.Username,
                    PostsNumber = (await _blogPostService.GetBlogPostByAuthorId(user.IdUser)).Count().ToString(),
                };
                userViewModeList.Add(userModel);
            }

            return View(userViewModeList);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            var blogPosts = await _blogPostService.GetBlogPostByAuthorId(user.IdUser);

            var images = new List<ImageModel>();

            foreach (var bp in blogPosts)
            {
                var image = await _imageService.GetImageByBlogPostId(bp.IdBlogPost);
                if (image != null)
                {
                    images.Add(new ImageModel
                    {
                        IdImage = image.IdImage,
                        IdBlogPost = bp.IdBlogPost,
                        Url = image.Url
                    });
                }
            }

            var userDetails = new UserDetailsViewModel
            {
                Username = user.Username,
                Images = images
            };

            return View(userDetails);
        }

        public ActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                if (userViewModel.PasswordHash != userViewModel.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                    return View(userViewModel);
                }

                var user = new UserModel
                {
                    IdUser = Guid.NewGuid(),
                    Username = userViewModel.Username,
                    Email = userViewModel.Email,
                    PasswordHash = userViewModel.PasswordHash,
                    Role = "User"
                };

                await _userService.Register(user);
                return RedirectToAction("Login");
            }

            return View(userViewModel);
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(IFormCollection collection)
        {
            var email = collection["email"];
            var password = collection["passwordhash"];
            var rememberMe = collection["rememberme"].Count > 0;
            var user = await _userService.GetUserByEmail(email);

            if (await _userService.Login(email, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserGuid", user.IdUser.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties 
                { 
                    IsPersistent = rememberMe,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View("Login");
        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
