namespace Blog.ViewModels;

/// <summary>
///  ������ͼģ��
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// ����id
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// �Ƿ���ʾ����id
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
