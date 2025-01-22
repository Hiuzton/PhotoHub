using Microsoft.AspNetCore.Mvc;
using PhotoHub.Services.Interfaces;

namespace PhotoHub.Controllers
{
    public class FileController : Controller
    {
        private readonly IS3Service _s3Service;

        public FileController(IS3Service s3Service)
        {
            _s3Service = s3Service;
        }

        [HttpGet]
        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Please select a file to upload.");
                return View();
            }

            try
            {
                var fileUrl = await _s3Service.UploadFileAsync(file);
                  
                ViewBag.FileUrl = fileUrl;
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"File upload failed: {ex.Message}");
                return View();
            }
        }
    }
}
