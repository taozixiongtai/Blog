using Blog.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

/// <summary>
/// ��ҳ��ؿ�����
/// </summary>
public class BlogController(IBlogServices _blogServices) : Controller
{
    /// <summary>
    /// ��ҳ��ͼ
    /// </summary>
    /// <returns>������ҳ��ͼ</returns>
    public async Task<IActionResult> Index()
    {
        var viewModel = await _blogServices.GetListAsync();
        return View(viewModel);
    }

    /// <summary>
    /// �����б���ͼ
    /// </summary>
    /// <returns>���ز����б���ͼ</returns>
    public async Task<IActionResult> BlogListAsync()
    {
        var viewModel = await _blogServices.GetListAsync();
        return View(viewModel);
    }

    /// <summary>
    /// ����������ͼ
    /// </summary>
    /// <param name="id">���� ID</param>
    /// <returns>���ز���������ͼ</returns>
    public async Task<IActionResult> DetailAsync(int id)
    {
        var viewModel = await _blogServices.GetByIdAsync(id);
        return View(viewModel);
    }
}
