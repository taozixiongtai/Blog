using Blog.Infrastructure.Models;
using System.Windows;

namespace Blog.Admin
{
    public partial class CategoryEditDialog : Window
    {
        public Category Category { get; private set; }

        public CategoryEditDialog(Category category = null)
        {
            InitializeComponent();
            if (category != null)
            {
                Category = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Image = category.Image
                };
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