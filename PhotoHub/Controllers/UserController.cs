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

        // GET: UserController
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

        // GET: UserController/Details/5
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

        // GET: UserController/Create
        public ActionResult Register()
        {
            return View("Register");
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(IFormCollection collection)
        {
            try
            {
                var user = new UserModel();
                user.IdUser = Guid.NewGuid();
                user.Role = "User";
                if (await TryUpdateModelAsync(user))
                {
                    await _userService.Register(user);
                    return RedirectToAction("Login");
                }
                return RedirectToAction("Login");
            }
            catch
            {
                return View("Login");
            }
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
                    //ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
