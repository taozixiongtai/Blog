using Blog.Infrastructure.Models;
using Blog.Mapperly;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Diagnostics;

namespace Blog.Controllers;

/// <summary>
/// 主页相关控制器
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="client">数据库客户端</param>
public class HomeController(ISqlSugarClient client) : Controller
{
    private readonly ISqlSugarClient _client = client;

    /// <summary>
    /// 主页视图
    /// </summary>
    /// <returns>返回主页视图</returns>
    public async Task<IActionResult> Index()
    {
        var mapper = new ArticleMapper();
        var articles = await _client.Queryable<Article>().Includes(s => s.Categories).ToListAsync();
        return View(mapper.ArticlesToArticleViewModels(articles));
    }

    /// <summary>
    /// 博客列表视图
    /// </summary>
    /// <returns>返回博客列表视图</returns>
    public async Task<IActionResult> BlogListAsync()
    {
        var mapper = new ArticleMapper();
        var articles = await _client.Queryable<Article>().Includes(s => s.Categories).ToListAsync();
        return View(mapper.ArticlesToArticleViewModels(articles));
    }

    /// <summary>
    /// 博客详情视图
    /// </summary>
    /// <param name="id">博客 ID</param>
    /// <returns>返回博客详情视图</returns>
    public async Task<IActionResult> DetailAsync(int id)
    {
        var mapper = new ArticleMapper();
        var article = await _client.Queryable<Article>().Includes(s => s.Categories).FirstAsync(s => s.Id == id);
        return View(mapper.ArticleToArticleViewModel(article));
    }

    /// <summary>
    /// 错误视图
    /// </summary>
    /// <returns>返回错误视图</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
