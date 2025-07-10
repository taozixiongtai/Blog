using Blog.Infrastructure.Models;
using Blog.Mapperly;
using Blog.Services.Interface;
using Blog.ViewModels;
using SqlSugar;

namespace Blog.Services;

public class BlogServices(ISqlSugarClient _client) : IBlogServices
{
    /// <summary>
    /// 主页视图
    /// </summary>
    /// <returns>返回主页视图</returns>
    public async Task<List<ArticleViewModel>> GetListAsync()
    {
        var articles = await _client.Queryable<Article>().Includes(s => s.Categories).ToListAsync();

        var mapper = new ArticleMapper();
        return mapper.ArticlesToArticleViewModels(articles);
    }

    /// <summary>
    /// 博客详情视图
    /// </summary>
    /// <param name="id">博客 ID</param>
    /// <returns>返回博客详情视图</returns>
    public async Task<ArticleViewModel> GetByIdAsync(int id)
    {
        var article = await _client.Queryable<Article>().Includes(s => s.Categories).FirstAsync(s => s.Id == id);

        var mapper = new ArticleMapper();
        return mapper.ArticleToArticleViewModel(article);
    }
}
