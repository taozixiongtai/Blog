using Blog.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

/// <summary>
/// 主页相关控制器
/// </summary>
public class BlogController(IBlogServices _blogServices) : Controller
{
    /// <summary>
    /// 主页视图
    /// </summary>
    /// <returns>返回主页视图</returns>
    public async Task<IActionResult> Index()
    {
        var viewModel = await _blogServices.GetListAsync();
        return View(viewModel);
    }

    /// <summary>
    /// 博客列表视图
    /// </summary>
    /// <returns>返回博客列表视图</returns>
    public async Task<IActionResult> BlogListAsync()
    {
        var viewModel = await _blogServices.GetListAsync();
        return View(viewModel);
    }

    /// <summary>
    /// 博客详情视图
    /// </summary>
    /// <param name="id">博客 ID</param>
    /// <returns>返回博客详情视图</returns>
    public async Task<IActionResult> DetailAsync(int id)
    {
        var viewModel = await _blogServices.GetByIdAsync(id);
        return View(viewModel);
    }
}
