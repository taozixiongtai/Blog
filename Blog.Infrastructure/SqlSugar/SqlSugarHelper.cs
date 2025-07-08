using SqlSugar;
using System.Reflection;

namespace Blog.Infrastructure.SqlSugar;

public class SqlSugarHelper //不能是泛型类
{
    public static SqlSugarScope Db = new(new ConnectionConfig()
    {
        DbType = DbType.Sqlite,
        ConnectionString = "Data Source=blog.db;Mode=ReadWriteCreate",
        IsAutoCloseConnection = true
    });

    public static void InitDataBase(SqlSugarScope db = null)
    {
        if (db == null)
        {
            db = Db;
        }
        var tableInfo = db.DbMaintenance.GetTableInfoList();
        // 表信息列表不为空，说明数据库已存在且表已创建，无需再次创建
        if (tableInfo.Count != 0)
        {
            return;
        }

        // 自动创建数据库 
        db.DbMaintenance.CreateDatabase();

        // 扫描所有带有SugarTable特性的类
        var entityTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetCustomAttribute<SugarTable>() != null)
            .ToList();

        db.CodeFirst.InitTables(entityTypes.ToArray());

        // 初始化种子数据
        SeedData.Init(db);
    }

}
