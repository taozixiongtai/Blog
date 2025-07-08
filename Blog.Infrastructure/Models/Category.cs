using SqlSugar;

namespace Blog.Infrastructure.Models;

/// <summary>
/// 分类实体类
/// </summary>
[SugarTable]
public class Category
{
    /// <summary>
    /// 分类唯一标识符。
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { set; get; }

    /// <summary>
    /// 分类名称。
    /// </summary>
    [SugarColumn]
    public string Name { set; get; }

    /// <summary>
    /// 分类的图标
    /// </summary>
    [SugarColumn]
    public string Image { set; get; }
}
