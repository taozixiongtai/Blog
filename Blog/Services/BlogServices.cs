using Blog.Infrastructure.Models;
using Blog.Mapperly;
using Blog.Services.Interface;
using Blog.ViewModels;
using SqlSugar;

namespace Blog.Services;

public class BlogServices(ISqlSugarClient _client) : IBlogServices
{
    /// <summary>
    /// 获取博客列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<ArticleViewModel>> GetListAsync()
    {
        var articles = await _client.Queryable<Article>().Includes(s => s.Categories).ToListAsync();

        var mapper = new ArticleMapper();
        return mapper.ArticlesToArticleViewModels(articles);
    }

    /// <summary>
    /// 获取博客详情
    /// </summary>
    /// <param name="id">文章的主键</param>
    /// <returns></returns>
    public async Task<ArticleViewModel> GetByIdAsync(int id)
    {
        var article = await _client.Queryable<Article>().Includes(s => s.Categories).FirstAsync(s => s.Id == id);

        var mapper = new ArticleMapper();
        return mapper.ArticleToArticleViewModel(article);
    }
}
