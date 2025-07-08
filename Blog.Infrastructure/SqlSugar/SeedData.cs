using Blog.Infrastructure.Models;
using SqlSugar;

namespace Blog.Infrastructure.SqlSugar;

/// <summary>
/// ���ݿ��������ݳ�ʼ��������
/// </summary>
public static class SeedData
{
    /// <summary>
    /// ��ʼ�����ݿ���������
    /// </summary>
    /// <param name="db">SqlSugar �ͻ���ʵ��</param>
    public static void Init(ISqlSugarClient db)
    {
        // �����������
        if (db.Queryable<Category>().Count() == 0)
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "����" , Image="" },
                new Category { Id = 2, Name = "����", Image=""  },
                new Category { Id = 3, Name = "���", Image="" }
            };
            db.Insertable(categories).ExecuteCommand();
        }

        // ������������
        if (db.Queryable<Article>().Count() == 0)
        {
            var articles = new List<Article>
            {
                new Article
                {
                    Id = 1,
                    Title = "��ӭʹ�ü��򲩿�",
                    Date = DateTime.Now.AddDays(-10),
                    Content = "<p>���ǵ�һƪ�������ݡ�</p>",
                    LastModifyDate = DateTime.Now.AddDays(-5)
                },
                new Article
                {
                    Id = 2,
                    Title = "������",
                    Date = DateTime.Now.AddDays(-8),
                    Content = "<p>����������ݡ�</p>",
                    LastModifyDate = DateTime.Now.AddDays(-3)
                }
            };
            db.Insertable(articles).ExecuteCommand();
        }

        // ��������-�����ϵ����
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

