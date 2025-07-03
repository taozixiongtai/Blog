using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blog.Admin;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<Article> Articles { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        // Mock 文章数据
        Articles = new ObservableCollection<Article>
        {
            new Article { Id = 1, Title = "WPF 入门", Category = "教程", CreatedAt = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd HH:mm") },
            new Article { Id = 2, Title = "Markdown 编辑器实现", Category = "技术", CreatedAt = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm") },
            new Article { Id = 3, Title = "生活随笔", Category = "生活", CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm") }
        };
        ArticleDataGrid.ItemsSource = Articles;
    }

    private void AddArticle_Click(object sender, RoutedEventArgs e)
    {
        var editor = new MarkDownEditer();
        editor.ShowDialog();
    }

    private void EditArticle_Click(object sender, RoutedEventArgs e)
    {
        if (ArticleDataGrid.SelectedItem is Article article)
        {
            MessageBox.Show($"模拟修改：{article.Title}", "提示");
            // 实际可打开编辑器并传递数据
        }
    }

    private void DeleteArticle_Click(object sender, RoutedEventArgs e)
    {
        if (ArticleDataGrid.SelectedItem is Article article)
        {
            if (MessageBox.Show($"确定要删除“{article.Title}”吗？", "确认删除", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Articles.Remove(article);
            }
        }
    }
}

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string CreatedAt { get; set; }
}