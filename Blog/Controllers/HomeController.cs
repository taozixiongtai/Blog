using Blog.Infrastructure.Models;
using Blog.Mapperly;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Diagnostics;

namespace Blog.Controllers;

/// <summary>
/// ��ҳ��ؿ�����
/// </summary>
/// <remarks>
/// ���캯��
/// </remarks>
/// <param name="client">���ݿ�ͻ���</param>
public class HomeController(ISqlSugarClient client) : Controller
{
    private readonly ISqlSugarClient _client = client;

    /// <summary>
    /// ��ҳ��ͼ
    /// </summary>
    /// <returns>������ҳ��ͼ</returns>
    public async Task<IActionResult> Index()
    {
        var mapper = new ArticleMapper();
        var articles = await _client.Queryable<Article>().Includes(s => s.Categories).ToListAsync();
        return View(mapper.ArticlesToArticleViewModels(articles));
    }

    /// <summary>
    /// �����б���ͼ
    /// </summary>
    /// <returns>���ز����б���ͼ</returns>
    public async Task<IActionResult> BlogListAsync()
    {
        var mapper = new ArticleMapper();
        var articles = await _client.Queryable<Article>().Includes(s => s.Categories).ToListAsync();
        return View(mapper.ArticlesToArticleViewModels(articles));
    }

    /// <summary>
    /// ����������ͼ
    /// </summary>
    /// <param name="id">���� ID</param>
    /// <returns>���ز���������ͼ</returns>
    public async Task<IActionResult> DetailAsync(int id)
    {
        var mapper = new ArticleMapper();
        var article = await _client.Queryable<Article>().Includes(s => s.Categories).FirstAsync(s => s.Id == id);
        return View(mapper.ArticleToArticleViewModel(article));
    }

    /// <summary>
    /// ������ͼ
    /// </summary>
    /// <returns>���ش�����ͼ</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
