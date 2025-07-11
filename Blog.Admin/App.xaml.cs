﻿using Blog.Infrastructure.SqlSugar;
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
        SqlSugarHelper.InitDataBase(new SqlSugar.ConnectionConfig()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"],
            DbType = (SqlSugar.DbType)Enum.Parse(typeof(SqlSugar.DbType), ConfigurationManager.AppSettings["DbType"] ?? string.Empty),
        });
    }
}

