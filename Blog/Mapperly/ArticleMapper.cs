using Blog.Infrastructure.Models;
using Blog.ViewModels;
using Riok.Mapperly.Abstractions;

namespace Blog.Mapperly;

/// <summary>
/// 文章实体与视图模型的映射器
/// </summary>
[Mapper]
public partial class ArticleMapper
{

    private List<string> MapCategories(List<Category> categories)
        => categories?.Select(c => c.Name).ToList() ?? new List<string>();

    private string MapCategoryImage(List<Category> categories)
      => categories.FirstOrDefault()?.Image ?? string.Empty;

    public partial List<ArticleViewModel> ArticlesToArticleViewModels(List<Article> articles);

    [MapProperty(source: nameof(Article.Categories), target: nameof(ArticleViewModel.CategoriesList), Use = nameof(MapCategories))]
    [MapProperty(source: nameof(Article.Categories), target: nameof(ArticleViewModel.CategoryImage), Use = nameof(MapCategoryImage))]
    public partial ArticleViewModel ArticleToArticleViewModel(Article article);

}
