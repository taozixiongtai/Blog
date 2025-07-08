using Blog.Infrastructure.Models;
using Blog.Infrastructure.SqlSugar;
using System.Windows;

namespace Blog.Admin;

/// <summary>
/// ����༭�Ի�������������༭������Ϣ��
/// </summary>
public partial class CategoryEditDialog : Window
{
    /// <summary>
    /// ��ǰ�༭�ķ������
    /// </summary>
    public Category Category { get; private set; }

    /// <summary>
    /// ���캯��������id���ط�����Ϣ��idΪ0ʱΪ������
    /// </summary>
    /// <param name="id">����Id��0��ʾ����</param>
    public CategoryEditDialog(int id = 0)
    {
        InitializeComponent();
        if (id != 0)
        {
            // �༭ģʽ���������з�����Ϣ
            Category = SqlSugarHelper.Db.Queryable<Category>().First(s => s.Id == id);
            NameTextBox.Text = Category.Name;
            ImageTextBox.Text = Category.Image;
        }
        else
        {
            // ����ģʽ����ʼ���շ���
            Category = new Category();
        }
    }

    /// <summary>
    /// ȷ����ť����¼��������������ݲ��رնԻ���
    /// </summary>
    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        Category.Name = NameTextBox.Text.Trim();
        Category.Image = ImageTextBox.Text.Trim();
        DialogResult = true;
    }

    /// <summary>
    /// ȡ����ť����¼����رնԻ����Ҳ����档
    /// </summary>
    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}