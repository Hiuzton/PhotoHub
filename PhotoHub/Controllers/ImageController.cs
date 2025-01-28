using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoHub.Services.Interfaces;

namespace PhotoHub.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        public async Task<IActionResult> Index()
        {
            var images = await _imageService.GetAllImages();
            return View(images);
        }
    }
}
