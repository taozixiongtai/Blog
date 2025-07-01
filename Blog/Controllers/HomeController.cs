using System.Diagnostics;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISqlSugarClient _client;

        public HomeController(ILogger<HomeController> logger, ISqlSugarClient client)
        {
            _logger = logger;
            _client = client;
        }

        public IActionResult Index()
        {
            var a = _client.Queryable<Article>().ToList();
            return View();
        }

        public IActionResult BlogList()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
