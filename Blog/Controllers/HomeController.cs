using Blog.Mapperly;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Blog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISqlSugarClient _client;

    public HomeController(ILogger<HomeController> logger, ISqlSugarClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task<IActionResult> Index()
    {
        var mapper = new ArticleMapper();
        var articles = await _client.Queryable<Article>().Includes(s => s.Categories).ToListAsync();
        return View(mapper.ArticlesToArticleViewModels(articles));
    }

    public async Task<IActionResult> BlogListAsync()
    {
        var mapper = new ArticleMapper();
        var articles = await _client.Queryable<Article>().Includes(s => s.Categories).ToListAsync();
        return View(mapper.ArticlesToArticleViewModels(articles));
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
