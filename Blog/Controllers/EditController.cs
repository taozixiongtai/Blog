using Microsoft.AspNetCore.Mvc;
using Blog.ViewModels;

namespace Blog.Controllers
{
    public class EditController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new ArticleViewModel
            {
                Title = "",
                Content = "",
                Categories = new List<string>(),
                Date = DateTime.Now,
                LastModifyDate = DateTime.Now
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: ±£´æÂß¼­
                return RedirectToAction("Detail", "Home", new { id = model.Id });
            }
            return View(model);
        }
    }
}