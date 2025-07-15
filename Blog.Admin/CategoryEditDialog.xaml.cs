using Blog.Infrastructure.Models;
using Blog.Infrastructure.SqlSugar;
using Microsoft.Win32;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Blog.Admin;

/// <summary>
/// 分类编辑对话框，用于新增或编辑分类信息。
/// </summary>
public partial class CategoryEditDialog : Window
{
    /// <summary>
    /// 当前编辑的分类对象。
    /// </summary>
    private Category _category { get; set; }

    /// <summary>
    /// 选中的图片文件完整路径
    /// </summary>
    private string? SelectedImageFilePath;

    protected override async void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
        var categories = await SqlSugarHelper.Db.Queryable<Category>().ToDictionaryAsync(s => s.Id, s => s.Name);
        // 添加空项用于选择无上级分类
        categories.Add("0", "-无-");
        ParentCategoriesComboBox.ItemsSource = categories;
        ParentCategoriesComboBox.SelectedValue = "0";
    }

    public CategoryEditDialog(int id = 0)
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        if (id != 0)
        {
            // 编辑模式，加载已有分类信息
            _category = SqlSugarHelper.Db.Queryable<Category>().First(s => s.Id == id);
            NameTextBox.Text = _category.Name;
            if (!string.IsNullOrEmpty(_category.Image))
            {
                ImageTextBox.Text = _category.Image;
                var imgPath = Path.Combine(ConfigurationManager.AppSettings["ImgUrl"], _category.Image);
                if (File.Exists(imgPath))
                {
                    PreviewImageBox.Source = new BitmapImage(new Uri(imgPath));
                }
            }
            ParentCategoriesComboBox.SelectedValue = _category.ParentId;
        }
        else
        {
            // 新增模式，初始化空分类
            _category = new Category();
        }
    }

    /// <summary>
    /// 选择图片按钮点击事件
    /// </summary>
    private void SelectImage_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            SelectedImageFilePath = openFileDialog.FileName;
            ImageTextBox.Text = Path.GetFileName(SelectedImageFilePath);
            PreviewImageBox.Source = new BitmapImage(new Uri(SelectedImageFilePath));
        }
    }

    /// <summary>
    /// 确定按钮点击事件，保存输入内容并关闭对话框。
    /// </summary>
    private async void Ok_Click(object sender, RoutedEventArgs e)
    {
        _category.Name = NameTextBox.Text.Trim();
        _category.ParentId = int.Parse(ParentCategoriesComboBox.SelectedValue?.ToString());

        // 处理图片文件
        if (!string.IsNullOrEmpty(SelectedImageFilePath))
        {
            var fileName = Path.GetFileName(SelectedImageFilePath);
            var destPath = Path.Combine(ConfigurationManager.AppSettings["ImgUrl"], fileName);
            File.Copy(SelectedImageFilePath, destPath, true);
            _category.Image = fileName;
        }
        else
        {
            _category.Image = ImageTextBox.Text.Trim();
        }

        if (_category.Id > 0)
        {
            await SqlSugarHelper.Db.Updateable(_category).ExecuteCommandAsync();
        }
        else
        {
            await SqlSugarHelper.Db.Insertable(_category).ExecuteCommandAsync();
        }

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