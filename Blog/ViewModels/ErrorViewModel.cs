namespace Blog.ViewModels;

/// <summary>
///  错误视图模型
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// 请求id
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// 是否显示请求id
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
