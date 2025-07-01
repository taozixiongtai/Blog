using SqlSugar;

namespace Blog.Models;

/// <summary>
/// 分类实体类
/// </summary>
public class Category
{
    /// <summary>
    /// 唯一表示
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public int Id { set; get; }

    /// <summary>
    /// 分类名称
    /// </summary>
    [SugarColumn]
    public string CategoryName { set; get; }
}
