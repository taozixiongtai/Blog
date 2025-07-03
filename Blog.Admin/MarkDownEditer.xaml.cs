using Markdig;
using Markdig.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Blog.Admin
{
    /// <summary>
    /// MarkDownEditer.xaml 的交互逻辑
    /// </summary>
    public partial class MarkDownEditer : Window
    {               
        private MarkdownPipeline pipeline;

        public MarkDownEditer()
        {
            InitializeComponent();
            // 初始化Markdown管道
            pipeline = new MarkdownPipelineBuilder()
                .UseSupportedExtensions()
                .Build();

            // Mock 下拉框数据
            CategoryComboBox.ItemsSource = new List<string> { "技术", "生活", "随笔", "教程" };
            CategoryComboBox.SelectedIndex = 0;
            MarkdownViewer.Pipeline = pipeline;
        }

        private void MarkdownTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MarkdownViewer.Markdown = MarkdownTextBox.Text;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedCategory = CategoryComboBox.SelectedItem as string;
            string markdownContent = MarkdownTextBox.Text;
            MessageBox.Show($"分类: {selectedCategory}\n内容:\n{markdownContent.Substring(0, Math.Min(100, markdownContent.Length))}...", "提交内容预览");
        }
    }
}