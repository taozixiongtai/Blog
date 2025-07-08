using Blog.Infrastructure.Models;
using Blog.Infrastructure.SqlSugar;
using System.Security.Cryptography;
using System.Windows;

namespace Blog.Admin
{
    public partial class CategoryEditDialog : Window
    {
        public Category Category { get; private set; }

        public CategoryEditDialog(int id = 0)
        {
            InitializeComponent();
            if (id != 0)
            {
                Category=SqlSugarHelper.Db.Queryable<Category>().First(s => s.Id == id);
                NameTextBox.Text = Category.Name;
                ImageTextBox.Text = Category.Image;
            }
            else
            {
                Category = new Category();
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Category.Name = NameTextBox.Text.Trim();
            Category.Image = ImageTextBox.Text.Trim();
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}