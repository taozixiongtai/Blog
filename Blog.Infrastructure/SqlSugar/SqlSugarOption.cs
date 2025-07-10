namespace Blog.Infrastructure.SqlSugar;

/// <summary>
///  sqlSugar 配置选项类
/// </summary>
public class SqlSugarOption
{

    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public string ConnectionString { get; set; } = "Data Source=blog.db;Mode=ReadWriteCreate";

    /// <summary>
    ///  数据库类型
    /// </summary>
    public string DbType { get; set; } = "Sqlite";
}
