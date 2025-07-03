using Blog.Admin.Mapperly;
using Blog.Infrastructure.Models;
using Blog.Infrastructure.SqlSugar;
using Markdig;
using Markdig.Wpf;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Blog.Admin;

/// <summary>
/// MarkDownEditer.xaml 的交互逻辑
/// </summary>
public partial class MarkDownEditer : Window
{
    private MarkdownPipeline pipeline;
    private Article _article;
    private bool IsModify = false;

    public MarkDownEditer(int? id = null)
    {
        InitializeComponent();
        // 初始化Markdown管道
        pipeline = new MarkdownPipelineBuilder()
            .UseSupportedExtensions()
            .Build();

        if (id.HasValue)
        {
            var mapper = new ArticleMapper();
            _article = SqlSugarHelper.Db.Queryable<Article>().First(s => s.Id == id);
            MarkdownViewer.Markdown = _article.Content;
            IsModify = true;
        }

        var category = SqlSugarHelper.Db.Queryable<Category>().ToDictionary(s => s.Id, s => s.Name);
        // Mock 下拉框数据
        CategoryComboBox.ItemsSource = category;
        CategoryComboBox.SelectedIndex = 0;
        MarkdownViewer.Pipeline = pipeline;
    }

    private void MarkdownTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        MarkdownViewer.Markdown = MarkdownTextBox.Text;
    }

    private async void SubmitButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedCategory = CategoryComboBox.SelectedItem as KeyValuePair<int, string>?;
        if (selectedCategory == null)
        {
            MessageBox.Show("请选择分类");
            return;
        }
        if (IsModify)
        {
            _article.Content = MarkdownTextBox.Text;
            await SqlSugarHelper.Db.Updateable(_article).ExecuteCommandAsync();
            SqlSugarHelper.Db.Deleteable<ArticleAndCategoryRelation>().Where(s => s.ArticleId == _article.Id);
        }

    }
}