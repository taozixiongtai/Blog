using Blog.Models;
using Blog.ViewModels;
using Riok.Mapperly.Abstractions;

namespace Blog.Mapperly;

[Mapper]
public partial class ArticleMapper
{
    public partial ArticleViewModel ArticleToArticleViewModel(Article article);

    public partial List<ArticleViewModel> ArticlesToArticleViewModels(List<Article> articles);
}
