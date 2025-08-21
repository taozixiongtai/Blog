using System.Configuration;

namespace Blog.Admin;

public sealed class AppConfigManager
{
    private static readonly Lazy<AppConfigManager> _instance = new(() => new AppConfigManager());

    public static AppConfigManager Instance => _instance.Value;

    // 示例：读取 ConnectionString
    public string ConnectionString { get; }
    public string DbType { get; }

    private AppConfigManager()
    {
        ConnectionString = ConfigurationManager.AppSettings["ConnectionString"] ?? string.Empty;
        DbType = ConfigurationManager.AppSettings["DbType"] ?? string.Empty;
    }

    // 可扩展更多配置项
    public string? Get(string key) => ConfigurationManager.AppSettings[key];
   
}