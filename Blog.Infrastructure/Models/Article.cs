﻿using SqlSugar;

namespace Blog.Infrastructure.Models;

/// <summary>
/// 表示一篇博客文章的实体类。
/// </summary>
[SugarTable]
public class Article
{
    /// <summary>
    /// 文章唯一标识符。
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    /// <summary>
    /// 文章标题。
    /// </summary>
    [SugarColumn]
    public string Title { get; set; }

    /// <summary>
    /// 文章发布日期
    /// </summary>
    [SugarColumn]
    public DateTime Date { get; set; }

    /// <summary>
    /// 文章正文内容（markDown格式）。
    /// </summary>
    [SugarColumn]
    public string Content { get; set; }

    /// <summary>
    /// 文章正文内容（html格式）。
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? ContentHtml { set; get; }

    /// <summary>
    /// 最后修改日期
    /// </summary>
    [SugarColumn]
    public DateTime LastModifyDate { get; set; }

    /// <summary>
    /// 文章分类。
    /// </summary>
    [Navigate(typeof(ArticleAndCategoryRelation), nameof(ArticleAndCategoryRelation.ArticleId), nameof(ArticleAndCategoryRelation.CategoryId))]
    public List<Category> Categories { get; set; }
}
