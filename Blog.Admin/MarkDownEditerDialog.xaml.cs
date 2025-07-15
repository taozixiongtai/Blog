using Blog.Admin.Mapperly;
using Blog.Infrastructure.Models;
using Blog.Infrastructure.SqlSugar;
using Markdig;
using Markdig.Wpf;
using Microsoft.IdentityModel.Tokens;
using System.Windows;
using System.Windows.Controls;

namespace Blog.Admin;

/// <summary>
/// MarkDownEditerDialog.xaml 的交互逻辑，实现文章的新增和编辑功能。
/// </summary>
public partial class MarkDownEditer : Window
{
    private readonly MarkdownPipeline pipeline;
    private Article _article;

    protected override async void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
        CategoryComboBox.ItemsSource = await SqlSugarHelper.Db.Queryable<Category>().ToDictionaryAsync(s => s.Id, s => s.Name);
        CategoryComboBox.SelectedIndex = 0;
    }

    /// <summary>
    /// 构造函数，根据传入的文章ID决定是新增还是编辑模式。
    /// </summary>
    /// <param name="id">文章ID，null表示新增，非null表示编辑</param>
    public MarkDownEditer(int? id = null)
    {
        InitializeComponent();

        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        // 初始化Markdown管道
        pipeline = new MarkdownPipelineBuilder()
            .UseSupportedExtensions()
            .Build();

        if (id.HasValue)
        {
            var mapper = new ArticleMapper();
            _article = SqlSugarHelper.Db.Queryable<Article>().First(s => s.Id == id);
            MarkdownTextBox.Text = _article.Content;
            TitleTextBox.Text = _article.Title;
        }
        else
        {
            _article = new();
        }
        MarkdownViewer.Pipeline = pipeline;
    }

    /// <summary>
    /// Markdown内容变更时，实时渲染到右侧预览区。
    /// </summary>
    private void MarkdownTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        MarkdownViewer.Markdown = MarkdownTextBox.Text;
    }

    /// <summary>
    /// 提交按钮点击事件，处理文章的新增或修改逻辑。
    /// </summary>
    private async void SubmitButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedCategory = CategoryComboBox.SelectedItem as KeyValuePair<string, object>?;
        if (selectedCategory == null)
        {
            MessageBox.Show("请选择分类");
            return;
        }
        if (TitleTextBox.Text.IsNullOrEmpty())
        {
            MessageBox.Show("请输入标题");
            return;
        }

        var categoryId = Convert.ToInt32(selectedCategory.Value.Key);
        if (_article.Id > 0)
        {
            await AddArticleAsync(categoryId);
        }
        else
        {
            await ModifyArticleAsync(categoryId);
        }

        MessageBox.Show($"{(_article.Id > 0 ? "修改" : "新增")}成功");
        this.Close();
    }

    /// <summary>
    /// 修改文章逻辑
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    private async Task ModifyArticleAsync(int categoryId)
    {
        _article = new();
        _article.Title = TitleTextBox.Text;
        _article.Content = MarkdownTextBox.Text;
        _article.ContentHtml = Markdig.Markdown.ToHtml(_article.Content);
        _article.Date = DateTime.Now;
        _article.LastModifyDate = DateTime.Now;
        var aiticleId = await SqlSugarHelper.Db.Insertable(_article).ExecuteReturnIdentityAsync();
        var category = new ArticleAndCategoryRelation
        {
            ArticleId = aiticleId,
            CategoryId = categoryId
        };

        await SqlSugarHelper.Db.Insertable(category).ExecuteCommandAsync();
    }

    /// <summary>
    /// 新增文章逻辑
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    private async Task AddArticleAsync(int categoryId)
    {
        _article.Title = TitleTextBox.Text;
        _article.Content = MarkdownTextBox.Text;
        _article.ContentHtml = Markdig.Markdown.ToHtml( _article.Content );
        await SqlSugarHelper.Db.Updateable(_article).ExecuteCommandAsync();
        var modifyCategory = new ArticleAndCategoryRelation
        {
            ArticleId = _article.Id,
            CategoryId = categoryId
        };
        await SqlSugarHelper.Db.Insertable(modifyCategory).ExecuteCommandAsync();
    }
}