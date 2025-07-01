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
            // 检查并插入分类
            if (db.Queryable<Category>().Count() == 0)
            {
                var categories = new List<Category>
                {
                    new Category { Id = 1, CategoryName = "技术" },
                    new Category { Id = 2, CategoryName = "生活" },
                    new Category { Id = 3, CategoryName = "随笔" }
                };
                db.Insertable(categories).ExecuteCommand();
            }

            // 检查并插入文章
            if (db.Queryable<Article>().Count() == 0)
            {
                var articles = new List<Article>
                {
                    new Article
                    {
                        Id = 1,
                        Title = "欢迎使用极简博客",
                        Date = DateTime.Now.AddDays(-10),
                        Image = "https://i.pravatar.cc/120?img=3",
                        Content = "<p>这是第一篇文章内容。</p>",
                        LastModifyDate = DateTime.Now.AddDays(-5)
                    },
                    new Article
                    {
                        Id = 2,
                        Title = "生活点滴",
                        Date = DateTime.Now.AddDays(-8),
                        Image = "https://i.pravatar.cc/120?img=4",
                        Content = "<p>生活随笔内容。</p>",
                        LastModifyDate = DateTime.Now.AddDays(-3)
                    }
                };
                db.Insertable(articles).ExecuteCommand();
            }

            // 检查并插入文章-分类关系
            if (db.Queryable<ArticleAndCategoryRelation>().Count() == 0)
            {
                var relations = new List<ArticleAndCategoryRelation>
                {
                    new ArticleAndCategoryRelation { ArticleId = 1, CategoryId = 1 },
                    new ArticleAndCategoryRelation { ArticleId = 2, CategoryId = 2 },
                    // 假如有多分类
                    new ArticleAndCategoryRelation { ArticleId = 1, CategoryId = 3 }
                };
                db.Insertable(relations).ExecuteCommand();
            }
        }
    }
}

