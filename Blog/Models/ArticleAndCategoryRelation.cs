using SqlSugar;

namespace Blog.Models;

public class ArticleAndCategoryRelation
{
    [SugarColumn(IsPrimaryKey = true)]
    public int ArticleId { get; set; }

    [SugarColumn(IsPrimaryKey = true)]
    public int CategoryId { get; set; }
}
