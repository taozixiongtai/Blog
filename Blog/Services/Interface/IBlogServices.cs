using Blog.ViewModels;

namespace Blog.Services.Interface;

/// <summary>
/// blog服务接口
/// </summary>
public interface IBlogServices
{
    /// <summary>
    /// 获取博客详情
    /// </summary>
    /// <param name="id">文章的主键</param>
    /// <returns>文章视图模型</returns>
    Task<ArticleViewModel> GetByIdAsync(int id);

    /// <summary>
    /// 获取博客列表
    /// </summary>
    /// <returns>文章视图模型集合</returns>
    Task<List<ArticleViewModel>> GetListAsync();
}
