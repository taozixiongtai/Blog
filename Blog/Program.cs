// 入口程序，负责服务注册和中间件配置
using Blog.Infrastructure.SqlSugar;
using Blog.Options;
using Blog.Services;
using Blog.Services.Interface;
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var appsetting = builder.Configuration.GetSection(nameof(App)).Get<App>() ?? throw new Exception("读取配置文件失败");
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    var sqlSugar = new SqlSugarScope(new ConnectionConfig()
    {
        DbType = appsetting.SqlSugarOption.DbType,
        ConnectionString = appsetting.SqlSugarOption.ConnectionString,
        IsAutoCloseConnection = true,
    });
    // 自动建库和建表
    SqlSugarHelper.InitDataBase(sqlSugar);
    return sqlSugar;
});
builder.Services.AddScoped<IBlogServices, BlogServices>();

var app = builder.Build();

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
