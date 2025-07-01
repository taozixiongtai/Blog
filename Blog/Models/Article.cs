using SqlSugar;

namespace Blog.Models;

/// <summary>
/// 表示一篇博客文章的实体类。
/// </summary>
public class Article
{
    /// <summary>
    /// 文章唯一标识符。
    /// </summary>
    [SugarColumn]
    public int Id { get; set; }

    /// <summary>
    /// 文章标题。
    /// </summary>
    [SugarColumn]
    public string Title { get; set; }

    /// <summary>
    /// 文章副标题。
    /// </summary>
    [SugarColumn]
    public string Subtitle { get; set; }

    /// <summary>
    /// 作者名称。
    /// </summary>
    [SugarColumn]
    public string Author { get; set; }

    /// <summary>
    /// 文章发布日期
    /// </summary>
    [SugarColumn]
    public DateTime Date { get; set; }

    /// <summary>
    /// 文章分类。
    /// </summary>
    [SugarColumn]
    public string CategoryId { get; set; }

    /// <summary>
    /// 文章封面图片URL。
    /// </summary>
    [SugarColumn]
    public string Image { get; set; }

    /// <summary>
    /// 文章正文内容（HTML格式）。
    /// </summary>
    [SugarColumn]
    public string Content { get; set; }

    /// <summary>
    /// 最后修改日期
    /// </summary>
    [SugarColumn]
    public DateTime LastModifyDate { get; set; }
}
