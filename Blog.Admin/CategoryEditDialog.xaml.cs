using Blog.Infrastructure.Models;
using Blog.Infrastructure.SqlSugar;
using System.Windows;

namespace Blog.Admin;

/// <summary>
/// 分类编辑对话框，用于新增或编辑分类信息。
/// </summary>
public partial class CategoryEditDialog : Window
{
    /// <summary>
    /// 当前编辑的分类对象。
    /// </summary>
    public Category Category { get; private set; }

    /// <summary>
    /// 构造函数，根据id加载分类信息，id为0时为新增。
    /// </summary>
    /// <param name="id">分类Id，0表示新增</param>
    public CategoryEditDialog(int id = 0)
    {
        InitializeComponent();
        if (id != 0)
        {
            // 编辑模式，加载已有分类信息
            Category = SqlSugarHelper.Db.Queryable<Category>().First(s => s.Id == id);
            NameTextBox.Text = Category.Name;
            ImageTextBox.Text = Category.Image;
        }
        else
        {
            // 新增模式，初始化空分类
            Category = new Category();
        }
    }

    /// <summary>
    /// 确定按钮点击事件，保存输入内容并关闭对话框。
    /// </summary>
    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        Category.Name = NameTextBox.Text.Trim();
        Category.Image = ImageTextBox.Text.Trim();
        DialogResult = true;
    }

    /// <summary>
    /// 取消按钮点击事件，关闭对话框且不保存。
    /// </summary>
    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}