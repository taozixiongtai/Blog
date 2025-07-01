using Blog.Models;
using Blog.ViewModels;
using Riok.Mapperly.Abstractions;

namespace Blog.Mapperly;

[Mapper]
public partial class ArticleMapper
{

    // 自定义映射：List<Category> -> List<string>
    [MapProperty(
        source: nameof(Article.Categories),
        target: nameof(ArticleViewModel.Categories)
    )]
    private List<string> MapCategories(List<Category> categories)
        => categories?.Select(c => c.CategoryName).ToList() ?? [];

    public partial ArticleViewModel ArticleToArticleViewModel(Article article);

    public partial List<ArticleViewModel> ArticlesToArticleViewModels(List<Article> articles);
}
