using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class CategoryCantrol : UserControl
{
    private SettingsPage _settingsPage { get; set; }
    private ICategoryService _categoryService { get; set; }
    private IProductService _productService { get; set; }
    private ProuctCategoryDTO _prouctCategoryDto { get; set; }
    public CategoryCantrol()
    {
        InitializeComponent();
    }

    public void SetVariablies(SettingsPage settingsPage, ICategoryService categoryService,
        IProductService productService)
    {
        _settingsPage = settingsPage;
        _categoryService = categoryService;
        _productService = productService;
        GetAllCategorys();
    }

    public async void GetAllCategorys()
    {
        var category = await _categoryService.GetAllCategory();
        if (category != null && category.Any())
        {
            categroy_datagrid.ItemsSource = category;
            categroy_datagrid.Items.Refresh();

        }
    }
    
    private void Create_btn_OnClick(object sender, RoutedEventArgs e)
    {
        CategoryForm categoryForm = new CategoryForm();
        categoryForm.SetVariablies(this,_categoryService,_productService);
        categoryForm.GetAllProduct();
        categoryForm.ShowDialog();
    }

    private async void Delete_btn_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedcategory = categroy_datagrid.SelectedItem as ProuctCategoryDTO;

        if (selectedcategory != null)
        {
            await _categoryService.Delete(selectedcategory.Id);
            GetAllCategorys();
        }
    }

    private void Edit_btn_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedCategory = categroy_datagrid.SelectedItem as ProuctCategoryDTO;
        if(selectedCategory != null)
        {
            CategoryForm categoryForm = new CategoryForm();
            categoryForm.SetVariablies(this, _categoryService, _productService);
            categoryForm.FillForm(selectedCategory.Id);
            categoryForm.ShowDialog();
        }
    }

    private void Categroy_datagrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _prouctCategoryDto=categroy_datagrid.SelectedItem as ProuctCategoryDTO;
        
    }

    private void Categroy_datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        CategoryForm categoryForm = new CategoryForm();
        categoryForm.SetVariablies(this,_categoryService,_productService);
        categoryForm.FillForm(_prouctCategoryDto.Id,true);
        if ( _prouctCategoryDto!= null)
        {
            _prouctCategoryDto=categroy_datagrid.SelectedItem as ProuctCategoryDTO;
          
            categoryForm.save_btn.Visibility = Visibility.Collapsed;
            categoryForm.cansel_btn.Visibility = Visibility.Visible;
            
            categoryForm.ShowDialog();
           
        }
        else
        {
            MessageBox.Show("Select any Category");
        }
    }
}