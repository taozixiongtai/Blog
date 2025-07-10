// ��ڳ��򣬸������ע����м������
using Blog.Infrastructure.SqlSugar;
using Blog.Options;
using Blog.Services;
using Blog.Services.Interface;
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var appsetting = builder.Configuration.GetSection(nameof(App)).Get<App>() ?? throw new Exception("��ȡ�����ļ�ʧ��");
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    var  connectionConfig = new ConnectionConfig()
    {
        DbType = appsetting.SqlSugarOption.DbType,
        ConnectionString = appsetting.SqlSugarOption.ConnectionString,
        IsAutoCloseConnection = true,
    };
    var sqlSugar = new SqlSugarScope(connectionConfig);

    // �Զ�����ͽ���
    SeedData.InitDataBase(connectionConfig);
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
