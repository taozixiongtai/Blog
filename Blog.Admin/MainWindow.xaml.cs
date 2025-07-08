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

    public MainWindow()
    {
        InitializeComponent();
        Loaded += async (s, e) => await LoadDataAsync();
        SqlSugarHelper.InitDataBase();
    }

    private void AddArticle_Click(object sender, RoutedEventArgs e)
    {
        var editor = new MarkDownEditer();
        editor.Closed += async (s, e) => await LoadDataAsync();
        editor.Show();
    }

    private async Task LoadDataAsync()
    {
        var mapper = new ArticleMapper();
        var articles = await SqlSugarHelper.Db.Queryable<Article>().Includes(s => s.Categories).ToListAsync();
        Articles = new ObservableCollection<ArticleViewModel>(mapper.ArticlesToArticleViewModels(articles));
        ArticleDataGrid.ItemsSource = Articles;
    }

    private void EditArticle_Click(object sender, RoutedEventArgs e)
    {
        if (ArticleDataGrid.SelectedItem is ArticleViewModel article)
        {
            var editor = new MarkDownEditer(article.Id);
            editor.Closed += async (s, e) => MessageBox.Show("123");
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
}

