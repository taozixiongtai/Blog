using SqlSugar;
using Blog.Models; // 引入你的实体命名空间

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
    sqlSugar.CodeFirst.InitTables(typeof(Article), typeof(Category));
    // 如果有更多实体，继续添加

    return sqlSugar;
});

var app = builder.Build();

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
