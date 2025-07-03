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
        var mapper = new ArticleMapper();
        ArticleDataGrid.ItemsSource = SqlSugarHelper.Db.Queryable<Article>().ToList();
    }

    private void AddArticle_Click(object sender, RoutedEventArgs e)
    {
        var editor = new MarkDownEditer();
        editor.ShowDialog();
    }

    private void EditArticle_Click(object sender, RoutedEventArgs e)
    {
        if (ArticleDataGrid.SelectedItem is ArticleViewModel article)
        {
            var editor = new MarkDownEditer(article.Id);
        }
    }

    private async void DeleteArticle_Click(object sender, RoutedEventArgs e)
    {
        if (ArticleDataGrid.SelectedItem is ArticleViewModel article)
        {
            if (MessageBox.Show($"确定要删除“{article.Title}”吗？", "确认删除", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await SqlSugarHelper.Db.Deleteable<Article>().Where(s => s.Id == article.Id).ExecuteCommandAsync();
            }
        }
    }
}

