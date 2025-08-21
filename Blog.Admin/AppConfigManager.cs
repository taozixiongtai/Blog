using System.Configuration;

namespace Blog.Admin;

public sealed class AppConfigManager
{
    private static readonly Lazy<AppConfigManager> _instance = new(() => new AppConfigManager());

    public static AppConfigManager Instance => _instance.Value;

    // ʾ������ȡ ConnectionString
    public string ConnectionString { get; }
    public string DbType { get; }

    private AppConfigManager()
    {
        ConnectionString = ConfigurationManager.AppSettings["ConnectionString"] ?? string.Empty;
        DbType = ConfigurationManager.AppSettings["DbType"] ?? string.Empty;
    }

    // ����չ����������
    public string? Get(string key) => ConfigurationManager.AppSettings[key];
   
}