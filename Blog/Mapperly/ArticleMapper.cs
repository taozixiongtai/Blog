using Blog.Models;
using Blog.ViewModels;
using Riok.Mapperly.Abstractions;

namespace Blog.Mapperly;

/// <summary>
/// 文章实体与视图模型的映射器
/// </summary>
[Mapper]
public partial class ArticleMapper
{

    /// <summary>
    /// List&lt;Category&gt; 转 List&lt;string&gt;（分类名）
    /// </summary>
    [MapProperty(
        source: nameof(Article.Categories),
        target: nameof(ArticleViewModel.Categories)
    )]
    private List<string> MapCategories(List<Category> categories)
        => categories?.Select(c => c.Name).ToList() ?? new List<string>();

    [MapProperty(
      source: nameof(Article.Categories),
      target: nameof(ArticleViewModel.CategoryImage)
  )]
    private string MapCategoryImage(List<Category> categories)
      => categories.FirstOrDefault().Image ?? string.Empty;

    public partial List<ArticleViewModel> ArticlesToArticleViewModels(List<Article> articles);

    public partial ArticleViewModel ArticleToArticleViewModel(Article article);
}
