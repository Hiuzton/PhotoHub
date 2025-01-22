using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoHub.ViewModels;

namespace PhotoHub.Controllers
{
    public class BlogPostController : Controller
    {
        // GET: BlogPostController
        public ActionResult Index()
        {
            var model = new List<BlogPostViewModel>
            {
                new BlogPostViewModel
                {
                    Title = "Title",
                    Content = "Content",
                    CreatedDate = DateTime.Now,
                    ImageUrl = "https://cdn.assets.lomography.com/2e/bb967e1721a89289c4f6a32cd58c3a97047e02/1216x806x2.jpg?auth=53647ba9e10cfbc8304ddd0c0c778fbaed881138",
                    AuthorName = "Jim"
                }
            };

            return View(model);
        }

        // GET: BlogPostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BlogPostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
