using SqlSugar;

namespace Blog.Admin.ViewModels;

/// <summary>
/// ������ͼģ�ͣ�����ҳ��չʾ
/// </summary>
public class CategoryViewModel
{
    /// <summary>
    /// ����ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ��������
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ����ͼ��
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// �ϼ�����id
    /// </summary>
    public int ParentId { set; get; }

    /// <summary>
    /// �ϼ���������
    /// </summary>
    public string ParentName { set; get; }
}