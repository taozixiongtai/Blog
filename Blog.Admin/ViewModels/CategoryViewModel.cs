using SqlSugar;

namespace Blog.Admin.ViewModels;

/// <summary>
/// 分类视图模型，用于页面展示
/// </summary>
public class CategoryViewModel
{
    /// <summary>
    /// 分类ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 分类图标
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// 上级分类id
    /// </summary>
    public int ParentId { set; get; }

    /// <summary>
    /// 上级分类名称
    /// </summary>
    public string ParentName { set; get; }
}