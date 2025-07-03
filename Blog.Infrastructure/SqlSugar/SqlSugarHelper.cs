using SqlSugar;

namespace Blog.Infrastructure.SqlSugar;
public class SqlSugarHelper //不能是泛型类
{
    public static SqlSugarScope Db = new(new ConnectionConfig()
    {
        DbType = DbType.Sqlite,
        ConnectionString = "Data Source=blog.db;Mode=ReadWriteCreate",
        IsAutoCloseConnection = true
    },
  db =>
  {
      //调试SQL事件，可以删掉
      db.Aop.OnLogExecuting = (sql, pars) =>
      {
          Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));

      };
  });
}
