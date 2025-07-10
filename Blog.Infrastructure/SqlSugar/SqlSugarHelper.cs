using SqlSugar;
using System.Reflection;

namespace Blog.Infrastructure.SqlSugar;

/// <summary>
/// sqlsugar数据库操作帮助类
/// </summary>
public static class SqlSugarHelper
{

    public static SqlSugarScope Db = new(new ConnectionConfig()
    {
        IsAutoCloseConnection = true
    });

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="db"></param>
    public static void InitDataBase(ConnectionConfig connectionConfig)
    {
        Db.CurrentConnectionConfig = connectionConfig;

        var tableInfo = Db.DbMaintenance.GetTableInfoList();
        // 表信息列表不为空，说明数据库已存在且表已创建，无需再次创建
        if (tableInfo.Count != 0)
        {
            return;
        }

        // 自动创建数据库 
        Db.DbMaintenance.CreateDatabase();

        // 扫描所有带有SugarTable特性的类
        var entityTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetCustomAttribute<SugarTable>() != null)
            .ToList();

        Db.CodeFirst.InitTables(entityTypes.ToArray());

        // 初始化种子数据
        SeedData.Init(Db);
    }

}
