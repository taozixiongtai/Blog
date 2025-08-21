using Blog.Infrastructure.Models;
using SqlSugar;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Blog.Infrastructure.SqlSugar;

/// <summary>
/// sqlsugar数据库操作帮助类
/// </summary>
public static class SqlSugarHelper
{

    public static SqlSugarScope? Db
    {
        set; get;
    }


    public static void InitDb(ConnectionConfig connectionConfig)
    {
        // 创建 SqlSugarScope 实例
        Db = new SqlSugarScope(connectionConfig);
    }

    /// <summary>
    /// 初始化数据库
    /// </summary>
    public static async Task InitDataBase()
    {

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

        await InitDataAsync();
    }


    public static async Task InitDataAsync()
    {
        if (Db == null)
            throw new InvalidOperationException("数据库未初始化");

        // 获取所有带有SugarTable特性的实体类型
        var entityTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetCustomAttribute<SugarTable>() != null)
            .ToList();

        // 构建种子文件夹路径
        var blogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Seeds");

        if (!Directory.Exists(blogDir))
            throw new Exception($"种子文件目录不存在: {blogDir}");

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        foreach (var file in Directory.GetFiles(blogDir, "*.json"))
        {
            var fileName = Path.GetFileNameWithoutExtension(file);

            // 查找与文件名匹配的实体类型（表名优先，否则类型名）
            var entityType = entityTypes.FirstOrDefault(t =>
            {
                var sugarTable = t.GetCustomAttribute<SugarTable>();
                return (sugarTable?.TableName ?? t.Name).Equals(fileName, StringComparison.OrdinalIgnoreCase);
            });

            if (entityType == null)
                continue; // 未找到匹配实体，跳过

            var json = await File.ReadAllTextAsync(file);
            var listType = typeof(List<>).MakeGenericType(entityType);
            dynamic data = JsonSerializer.Deserialize(json, listType, jsonOptions);

            await Db.Insertable(data).ExecuteCommandAsync();
        }
    }



    /// <summary>
    /// 生成种子文件
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task GenerateSeedFilesAsync()
    {
        if (Db == null)
        {
            throw new InvalidOperationException("数据库未初始化");
        }

        // 获取所有带有SugarTable特性的实体类型
        var entityTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetCustomAttribute<SugarTable>() != null)
            .ToList();

        // 获取解决方案根目录
        // 假设 Blog 是 ASP.NET Core 项目文件夹名
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var blogDir = Path.Combine(baseDir, "..", "..", "..", "..", "Blog", "Seeds");
        var seedDir = Path.GetFullPath(blogDir);
        // 确保seed文件夹存在
        if (!Directory.Exists(seedDir))
        {
            throw new Exception($"种子文件目录不存在: {seedDir}");
        }

        var encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

        foreach (var type in entityTypes)
        {
            // 获取表名
            var sugarTableAttr = type.GetCustomAttribute<SugarTable>();
            var tableName = sugarTableAttr?.TableName ?? type.Name;

            // 查询表数据
            var data = Db.QueryableByObject(type).ToList();

            // 序列化为JSON
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true, Encoder = encoder });

            // 写入文件
            var filePath = Path.Combine(seedDir, $"{tableName}.json");
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
