// ��ڳ��򣬸������ע����м������
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

    // �Զ�����ͽ���
   sqlSugar.CodeFirst.InitTables(typeof(Article), typeof(Category), typeof(ArticleAndCategoryRelation));
    // ����и���ʵ�壬�������
    return sqlSugar;
}); 


var app = builder.Build();

// �Զ�����
//var db = app.Services.GetRequiredService<ISqlSugarClient>();
//db.CodeFirst.InitTables(typeof(Article), typeof(Category), typeof(ArticleAndCategoryRelation));
// ��ʼ����������
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
