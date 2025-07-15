using Blog.Models;
using Blog.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

    /// <summary>
    /// 错误视图
    /// </summary>
    /// <returns>返回错误视图</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
