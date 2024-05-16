using System.Windows;
using System.Windows.Controls;
using RoboticsPos.Services;

namespace RoboticsPos.UI.Components;

public partial class CategoryItemControl : UserControl
{
    private ICategoryService _categoryService { get; set; }
    private CategoriesForKassaControl _categoriesForKassaControl { get; set; }
    
    public CategoryItemControl()
    {
        InitializeComponent();
    }


    public void SerVariablies(ICategoryService categoryService, CategoriesForKassaControl categoriesForKassaControl)
    {
        _categoryService = categoryService;
        _categoriesForKassaControl = categoriesForKassaControl;
    }

    public void SetCategoryName(string name)
    {
        name_txt.Text = name;
    }
    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var category =
                _categoriesForKassaControl.categorylist.FirstOrDefault(a => a.Id == long.Parse(this.Tag.ToString()));
            var haschild = await _categoryService.HasChildCategory(category.Id);
            if (haschild)
            {
                _categoriesForKassaControl.GetChildCategories(category.Id);
            }
            else
            {
                _categoriesForKassaControl.GetCategoryProducts(category.Id);
            }
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}