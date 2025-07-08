using Blog.Admin.ViewModels;
using Blog.Infrastructure.Models;
using Riok.Mapperly.Abstractions;

namespace Blog.Admin.Mapperly;

[Mapper]
public partial class CategoryMapper
{
    public partial CategoryViewModel CategoryToCategoryViewModel(Category category);

    public partial List<CategoryViewModel> CategoriesToCategoryViewModels(List<Category> categories);

}
