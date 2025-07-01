using SqlSugar;
using Blog.Models; // �������ʵ�������ռ�

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
    sqlSugar.CodeFirst.InitTables(typeof(Article), typeof(Category));
    // ����и���ʵ�壬�������

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
