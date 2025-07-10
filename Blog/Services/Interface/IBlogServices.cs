using Blog.ViewModels;

namespace Blog.Services.Interface;

/// <summary>
/// blog服务接口
/// </summary>
public interface IBlogServices
{
    Task<ArticleViewModel> GetByIdAsync(int id);
    Task<List<ArticleViewModel>> GetListAsync();
}
