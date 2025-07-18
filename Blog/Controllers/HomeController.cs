using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class HomeController : Controller
{
    /// <summary>
    /// 有状态码的错误处理
    /// 用于处理被捕获的异常
    /// </summary>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        ViewBag.ErrorMessage = statusCode switch
        {
            404 => "抱歉，您访问的页面不存在",
            500 => "服务器内部错误",
            _ => "发生未知错误",
        };
        return View("Error");
    }

    /// <summary>
    /// 错误页面
    /// </summary>
    /// <returns></returns>
    [Route("Error")]
    public IActionResult Error() => View();
}
