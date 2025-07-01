using SqlSugar;

namespace Blog.ViewModels;

public class ArticleViewModel
{
    /// <summary>
    /// 文章唯一标识符。
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 文章标题。
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 文章发布日期
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// 文章分类。
    /// </summary>
    public List<string> Categories { get; set; }

    /// <summary>
    /// 文章封面图片URL。
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// 文章正文内容（HTML格式）。
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 最后修改日期
    /// </summary>
    public DateTime LastModifyDate { get; set; }
}
