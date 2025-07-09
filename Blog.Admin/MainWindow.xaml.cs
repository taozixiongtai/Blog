using Blog.Admin.Mapperly;
using Blog.Admin.ViewModels;
using Blog.Infrastructure.Models;
using Blog.Infrastructure.SqlSugar;
using System.Collections.ObjectModel;
using System.Windows;

namespace Blog.Admin;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<ArticleViewModel> Articles { get; set; }
    public ObservableCollection<CategoryViewModel> Categories { get; set; }

    /// <summary>
    /// 构造函数，初始化窗口并加载数据
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 窗口激活时加载数据
    /// </summary>
    /// <param name="e"></param>
    protected override async void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
        await LoadDataAsync();
        await LoadCategoryDataAsync();
    }

    #region  文章相关    
    /// <summary>
    /// 新增文章按钮点击事件
    /// </summary>
    private void AddArticle_Click(object sender, RoutedEventArgs e)
    {
        var editor = new MarkDownEditer();
        editor.ShowDialog();
    }

    /// <summary>
    /// 加载文章数据
    /// </summary>
    private async Task LoadDataAsync()
    {
        var mapper = new ArticleMapper();
        var articles = await SqlSugarHelper.Db.Queryable<Article>().ToListAsync();
        Articles = new ObservableCollection<ArticleViewModel>(mapper.ArticlesToArticleViewModels(articles));
        ArticleDataGrid.ItemsSource = Articles;
    }

    /// <summary>
    /// 编辑文章按钮点击事件
    /// </summary>
    private void EditArticle_Click(object sender, RoutedEventArgs e)
    {
        if (ArticleDataGrid.SelectedItem is ArticleViewModel article)
        {
            var editor = new MarkDownEditer(article.Id);
            editor.Closed += async (s, e) => await LoadDataAsync();
            editor.Show();
        }
    }

    /// <summary>
    /// 删除文章按钮点击事件
    /// </summary>
    private async void DeleteArticle_Click(object sender, RoutedEventArgs e)
    {
        if (ArticleDataGrid.SelectedItem is ArticleViewModel article && MessageBox.Show($"确定要删除“{article.Title}”吗？", "确认删除", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            await SqlSugarHelper.Db.Deleteable<Article>().Where(s => s.Id == article.Id).ExecuteCommandAsync();
            await LoadDataAsync();
        }
    }
    #endregion

    #region 分类相关
    /// <summary>
    /// 加载分类数据
    /// </summary>
    private async Task LoadCategoryDataAsync()
    {
        var mapper = new CategoryMapper();
        var categories = await SqlSugarHelper.Db.Queryable<Category>().ToListAsync();
        Categories = new ObservableCollection<CategoryViewModel>(mapper.MapCategoriesToCategoryViewModels(categories));
        CategoryDataGrid.ItemsSource = Categories;
    }

    /// <summary>
    /// 新增分类按钮点击事件
    /// </summary>
    private async void AddCategory_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new CategoryEditDialog();
        if (dialog.ShowDialog() == true)
        {
            await LoadCategoryDataAsync();
        }
    }

    /// <summary>
    /// 编辑分类按钮点击事件
    /// </summary>
    private async void EditCategory_Click(object sender, RoutedEventArgs e)
    {
        if (CategoryDataGrid.SelectedItem is CategoryViewModel categoryViewModel)
        {
            var dialog = new CategoryEditDialog(categoryViewModel.Id);
            if (dialog.ShowDialog() == true)
            {
                await LoadCategoryDataAsync();
            }
        }
    }

    /// <summary>
    /// 删除分类按钮点击事件
    /// </summary>
    private async void DeleteCategory_Click(object sender, RoutedEventArgs e)
    {
        if (CategoryDataGrid.SelectedItem is CategoryViewModel categoryViewModel)
        {
            if (MessageBox.Show($"确定要删除“{categoryViewModel.Name}”吗？", "确认删除", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await SqlSugarHelper.Db.Deleteable<Category>().Where(s => s.Id == categoryViewModel.Id).ExecuteCommandAsync();
                await LoadCategoryDataAsync();
            }
        }
    }
    #endregion
}
