﻿using Blog.Admin.ViewModels;
using Blog.Infrastructure.Models;
using Riok.Mapperly.Abstractions;

namespace Blog.Admin.Mapperly;

/// <summary>
/// 文章实体与视图模型的映射器
/// </summary>
[Mapper]
public partial class ArticleMapper
{
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
      => categories.FirstOrDefault()?.Image ?? string.Empty;

    public partial List<ArticleViewModel> ArticlesToArticleViewModels(List<Article> articles);

    public partial ArticleViewModel ArticleToArticleViewModel(Article article);
}
