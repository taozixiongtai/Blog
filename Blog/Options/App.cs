using SqlSugar;

namespace Blog.Options;

/// <summary>
/// 应用的选项配置类
/// </summary>
public class App
{
    /// <summary>
    /// sqlSugar的配置选项
    /// </summary>
    public ConnectionConfig SqlSugarOption { set; get; }
}
