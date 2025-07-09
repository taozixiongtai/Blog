using Blog.Admin.ViewModels;
using Blog.Infrastructure.Models;
using Riok.Mapperly.Abstractions;

namespace Blog.Admin.Mapperly;

/// <summary>
/// ∑÷¿‡”≥…‰
/// </summary>
[Mapper]
public partial class CategoryMapper
{
    // public partial CategoryViewModel CategoryToCategoryViewModel(Category category);

    private partial List<CategoryViewModel> CategoriesToCategoryViewModels(List<Category> categories);

    [UserMapping(Default = true)]
    public List<CategoryViewModel> MapCategoriesToCategoryViewModels(List<Category> categories)
    {
        var dto = CategoriesToCategoryViewModels(categories);
        dto.ForEach(c => c.ParentName = categories.FirstOrDefault(p => p.Id == c.ParentId)?.Name ?? "-Œﬁ-");
        return dto;
    }
}
