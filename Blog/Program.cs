// ��ڳ��򣬸������ע����м������
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
var connectionConfig = new ConnectionConfig()
{
    DbType = appsetting.SqlSugarOption.DbType,
    ConnectionString = appsetting.SqlSugarOption.ConnectionString,
    IsAutoCloseConnection = true,
};
builder.Services.AddSingleton<ISqlSugarClient>(s => new SqlSugarScope(connectionConfig));
builder.Services.AddScoped<IBlogServices, BlogServices>();

// �Զ�����ͽ���
SqlSugarHelper.InitDb(connectionConfig);
await SqlSugarHelper.InitDataBase();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // ����ʱ��ʾ��ϸ����ҳ��
    app.UseDeveloperExceptionPage();
}
else
{  // ȫ���쳣����
    app.UseExceptionHandler("/Error");
    // ��������ʹ�ô�����ҳ���״̬���ض���
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");

app.Run();
