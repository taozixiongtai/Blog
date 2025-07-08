using Blog.Infrastructure.Models;
using SqlSugar;

namespace Blog.Infrastructure.SqlSugar;

/// <summary>
/// 数据库种子数据初始化工具类
/// </summary>
public static class SeedData
{
    /// <summary>
    /// 初始化数据库种子数据
    /// </summary>
    /// <param name="db">SqlSugar 客户端实例</param>
    public static void Init(ISqlSugarClient db)
    {
        // 插入分类数据
        if (db.Queryable<Category>().Count() == 0)
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "技术" , Image="" },
                new Category { Id = 2, Name = "生活", Image=""  },
                new Category { Id = 3, Name = "随笔", Image="" }
            };
            db.Insertable(categories).ExecuteCommand();
        }

        // 插入文章数据
        if (db.Queryable<Article>().Count() == 0)
        {
            var articles = new List<Article>
            {
                new Article
                {
                    Id = 1,
                    Title = "欢迎使用极简博客",
                    Date = DateTime.Now.AddDays(-10),
                    Content = "<p>这是第一篇文章内容。</p>",
                    LastModifyDate = DateTime.Now.AddDays(-5)
                },
                new Article
                {
                    Id = 2,
                    Title = "生活点滴",
                    Date = DateTime.Now.AddDays(-8),
                    Content = "<p>生活随笔内容。</p>",
                    LastModifyDate = DateTime.Now.AddDays(-3)
                }
            };
            db.Insertable(articles).ExecuteCommand();
        }

        // 插入文章-分类关系数据
        if (db.Queryable<ArticleAndCategoryRelation>().Count() == 0)
        {
            var relations = new List<ArticleAndCategoryRelation>
            {
                new ArticleAndCategoryRelation { ArticleId = 1, CategoryId = 1 },
                new ArticleAndCategoryRelation { ArticleId = 2, CategoryId = 2 },
                new ArticleAndCategoryRelation { ArticleId = 1, CategoryId = 3 }
            };
            db.Insertable(relations).ExecuteCommand();
        }
    }
}

