using Blog.Infrastructure.Models;
using Blog.Infrastructure.SqlSugar;
using Microsoft.Win32;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Blog.Admin;

/// <summary>
/// ����༭�Ի�������������༭������Ϣ��
/// </summary>
public partial class CategoryEditDialog : Window
{
    /// <summary>
    /// ��ǰ�༭�ķ������
    /// </summary>
    private Category _category { get; set; }

    /// <summary>
    /// ѡ�е�ͼƬ�ļ�����·��
    /// </summary>
    private string? SelectedImageFilePath;

    protected override async void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
        var categories = await SqlSugarHelper.Db.Queryable<Category>().ToDictionaryAsync(s => s.Id, s => s.Name);
        // ��ӿ�������ѡ�����ϼ�����
        categories.Add("0", "-��-");
        ParentCategoriesComboBox.ItemsSource = categories;
        ParentCategoriesComboBox.SelectedValue = "0";
    }

    public CategoryEditDialog(int id = 0)
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        if (id != 0)
        {
            // �༭ģʽ���������з�����Ϣ
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
            // ����ģʽ����ʼ���շ���
            _category = new Category();
        }
    }

    /// <summary>
    /// ѡ��ͼƬ��ť����¼�
    /// </summary>
    private void SelectImage_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "ͼƬ�ļ�|*.jpg;*.jpeg;*.png;*.bmp"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            SelectedImageFilePath = openFileDialog.FileName;
            ImageTextBox.Text = Path.GetFileName(SelectedImageFilePath);
            PreviewImageBox.Source = new BitmapImage(new Uri(SelectedImageFilePath));
        }
    }

    /// <summary>
    /// ȷ����ť����¼��������������ݲ��رնԻ���
    /// </summary>
    private async void Ok_Click(object sender, RoutedEventArgs e)
    {
        _category.Name = NameTextBox.Text.Trim();
        _category.ParentId = int.Parse(ParentCategoriesComboBox.SelectedValue?.ToString());

        // ����ͼƬ�ļ�
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
    /// ȡ����ť����¼����رնԻ����Ҳ����档
    /// </summary>
    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}