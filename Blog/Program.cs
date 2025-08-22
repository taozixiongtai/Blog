// 入口程序，负责服务注册和中间件配置
using Blog.Infrastructure.SqlSugar;
using Blog.Options;
using Blog.Services;
using Blog.Services.Interface;
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
});
var appConfiguration = builder.Configuration.GetSection(nameof(App));
builder.Services.Configure<App>(appConfiguration);
var appsetting = appConfiguration.Get<App>();

builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    var connectionConfig = new ConnectionConfig()
    {
        DbType = appsetting.SqlSugarOption.DbType,
        ConnectionString = string.Format(appsetting.SqlSugarOption.ConnectionString, AppDomain.CurrentDomain.BaseDirectory + "Blog.db"),
        IsAutoCloseConnection = true,
    };

#if DEBUG
    // 自动建库和建表
    //SqlSugarHelper.InitDb(connectionConfig);
    //SqlSugarHelper.InitDataBase().GetAwaiter().GetResult();
#endif

    return new SqlSugarScope(connectionConfig);
});

builder.Services.AddScoped<IBlogServices, BlogServices>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // 开发时显示详细错误页面
    app.UseDeveloperExceptionPage();
}
else
{  // 全局异常处理
    app.UseExceptionHandler("/Error");
    // 生产环境使用错误处理页面和状态码重定向
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");

app.Run();
