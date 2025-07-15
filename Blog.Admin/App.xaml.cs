using Blog.Infrastructure.SqlSugar;
using SqlSugar;
using System.Configuration;
using System.Windows;

namespace Blog.Admin;
/// <summary>
/// App.xaml 的交互逻辑，应用程序入口类。
/// </summary>
public partial class App : Application
{

    /// <summary>
    /// Raises the System.Windows.Application.Startup event.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // 自动建库和建表
        SqlSugarHelper.InitDb(new ConnectionConfig()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"],
            DbType = (DbType)Enum.Parse(typeof(DbType), ConfigurationManager.AppSettings["DbType"] ?? string.Empty),
        });
        SqlSugarHelper.InitDataBase();
    }
}

