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
        public MarkDownEditer()
        {
            InitializeComponent();
            // 初始化Markdown管道（可以添加各种扩展）
            var pipeline = new MarkdownPipelineBuilder()
                .UseSupportedExtensions()
                .Build();

            // 设置Markdown内容
            string markdownText = @"
# Markdig.Wpf 示例

这是一个使用 **Markdig.Wpf** 的示例。

## 功能列表

- Markdown 渲染
- 语法高亮
- 表格支持
- 任务列表
- 等等...

```csharp
// 代码块示例
public class Program
{
    public static void Main()
    {
        Console.WriteLine(""Hello Markdig!"");
    }
}";
            MarkdownViewer.Markdown = markdownText;
            MarkdownViewer.Pipeline = pipeline;
        }
    }
}
 