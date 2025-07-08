using Blog.Admin.Mapperly;
using Blog.Admin.ViewModels;
using Blog.Infrastructure.Models;
using Blog.Infrastructure.SqlSugar;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace Blog.Admin;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<ArticleViewModel> Articles { get; set; }
    public ObservableCollection<Category> Categories { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        Loaded += async (s, e) =>
        {
            await LoadDataAsync();
            await LoadCategoryDataAsync();
        };
        SqlSugarHelper.InitDataBase();
    }

    // 文章管理相关
    private void AddArticle_Click(object sender, RoutedEventArgs e)
    {
        var editor = new MarkDownEditer();
        editor.ShowDialog();
    }

    private async Task LoadDataAsync()
    {
        var mapper = new ArticleMapper();
        var articles = await SqlSugarHelper.Db.Queryable<Article>().ToListAsync();
        Articles = new ObservableCollection<ArticleViewModel>(mapper.ArticlesToArticleViewModels(articles));
        ArticleDataGrid.ItemsSource = Articles;
    }

    private void EditArticle_Click(object sender, RoutedEventArgs e)
    {
        if (ArticleDataGrid.SelectedItem is ArticleViewModel article)
        {
            var editor = new MarkDownEditer(article.Id);
            editor.Closed += async (s, e) => await LoadDataAsync();
            editor.Show();
        }
    }

    private async void DeleteArticle_Click(object sender, RoutedEventArgs e)
    {
        if (ArticleDataGrid.SelectedItem is ArticleViewModel article)
        {
            if (MessageBox.Show($"确定要删除“{article.Title}”吗？", "确认删除", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await SqlSugarHelper.Db.Deleteable<Article>().Where(s => s.Id == article.Id).ExecuteCommandAsync();
                await LoadDataAsync();
            }
        }
    }

    // 分类管理相关
    private async Task LoadCategoryDataAsync()
    {
        var categories = await SqlSugarHelper.Db.Queryable<Category>().ToListAsync();
        Categories = new ObservableCollection<Category>(categories);
        CategoryDataGrid.ItemsSource = Categories;
    }

    private async void AddCategory_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new CategoryEditDialog();
        if (dialog.ShowDialog() == true)
        {
            var category = dialog.Category;
            await SqlSugarHelper.Db.Insertable(category).ExecuteCommandAsync();
            await LoadCategoryDataAsync();
        }
    }

    private async void EditCategory_Click(object sender, RoutedEventArgs e)
    {
        if (CategoryDataGrid.SelectedItem is Category category)
        {
            var dialog = new CategoryEditDialog(category);
            if (dialog.ShowDialog() == true)
            {
                var updated = dialog.Category;
                await SqlSugarHelper.Db.Updateable(updated).ExecuteCommandAsync();
                await LoadCategoryDataAsync();
            }
        }
    }

    private async void DeleteCategory_Click(object sender, RoutedEventArgs e)
    {
        if (CategoryDataGrid.SelectedItem is Category category)
        {
            if (MessageBox.Show($"确定要删除“{category.Name}”吗？", "确认删除", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await SqlSugarHelper.Db.Deleteable<Category>().Where(s => s.Id == category.Id).ExecuteCommandAsync();
                await LoadCategoryDataAsync();
            }
        }
    }
}

