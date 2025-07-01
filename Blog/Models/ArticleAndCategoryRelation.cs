using SqlSugar;

namespace Blog.Models;

/// <summary>
/// 文章与分类的多对多关系表
/// </summary>
public class ArticleAndCategoryRelation
{
    /// <summary>
    /// 文章ID
    /// </summary>
    [SugarColumn]
    public int ArticleId { get; set; }

    /// <summary>
    /// 分类ID
    /// </summary>
    [SugarColumn]
    public int CategoryId { get; set; }
}
