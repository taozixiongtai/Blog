// 入口程序，负责服务注册和中间件配置
using Blog.Data;
using Blog.Infrastructure.Models;
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    var sqlSugar = new SqlSugarScope(new ConnectionConfig()
    {
        DbType = DbType.Sqlite,
        ConnectionString = "Data Source=blog.db;Mode=ReadWriteCreate",
        IsAutoCloseConnection = true,
    });

    // 自动建库和建表
   sqlSugar.CodeFirst.InitTables(typeof(Article), typeof(Category), typeof(ArticleAndCategoryRelation));
    // 如果有更多实体，继续添加
    return sqlSugar;
}); 


var app = builder.Build();

// 自动建表
//var db = app.Services.GetRequiredService<ISqlSugarClient>();
//db.CodeFirst.InitTables(typeof(Article), typeof(Category), typeof(ArticleAndCategoryRelation));
// 初始化种子数据
//SeedData.Init(db);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
