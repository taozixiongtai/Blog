using Blog.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Data
{
    public static class SeedData
    {
        public static void Init(ISqlSugarClient db)
        {
            // ��鲢�������
            if (db.Queryable<Category>().Count() == 0)
            {
                var categories = new List<Category>
                {
                    new Category { Id = 1, CategoryName = "����" },
                    new Category { Id = 2, CategoryName = "����" },
                    new Category { Id = 3, CategoryName = "���" }
                };
                db.Insertable(categories).ExecuteCommand();
            }

            // ��鲢��������
            if (db.Queryable<Article>().Count() == 0)
            {
                var articles = new List<Article>
                {
                    new Article
                    {
                        Id = 1,
                        Title = "��ӭʹ�ü��򲩿�",
                        Date = DateTime.Now.AddDays(-10),
                        Image = "https://i.pravatar.cc/120?img=3",
                        Content = "<p>���ǵ�һƪ�������ݡ�</p>",
                        LastModifyDate = DateTime.Now.AddDays(-5)
                    },
                    new Article
                    {
                        Id = 2,
                        Title = "������",
                        Date = DateTime.Now.AddDays(-8),
                        Image = "https://i.pravatar.cc/120?img=4",
                        Content = "<p>����������ݡ�</p>",
                        LastModifyDate = DateTime.Now.AddDays(-3)
                    }
                };
                db.Insertable(articles).ExecuteCommand();
            }

            // ��鲢��������-�����ϵ
            if (db.Queryable<ArticleAndCategoryRelation>().Count() == 0)
            {
                var relations = new List<ArticleAndCategoryRelation>
                {
                    new ArticleAndCategoryRelation { ArticleId = 1, CategoryId = 1 },
                    new ArticleAndCategoryRelation { ArticleId = 2, CategoryId = 2 },
                    // �����ж����
                    new ArticleAndCategoryRelation { ArticleId = 1, CategoryId = 3 }
                };
                db.Insertable(relations).ExecuteCommand();
            }
        }
    }
}

