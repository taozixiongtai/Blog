namespace Blog.ViewModels;

/// <summary>
/// 文章视图模型，用于页面展示
/// </summary>
public class ArticleViewModel
{
    /// <summary>
    /// 文章ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// 分类名称列表
    /// </summary>
    public List<string> Categories { get; set; }

    /// <summary>
    /// 封面图片
    /// </summary>
    public string CategoryImage { get; set; }

    /// <summary>
    /// 正文内容
    /// </summary>
    public string ContentHtml { get; set; }

    /// <summary>
    /// 最后修改日期
    /// </summary>
    public DateTime LastModifyDate { get; set; }
}
