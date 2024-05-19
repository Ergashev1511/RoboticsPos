using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI.Components;

public partial class CategoriesForKassaControl : UserControl
{
    private KassaPage _kassaPage { get; set; }
    private ICategoryService _categoryService { get; set; }
    public List<Select> categorylist = new List<Select>();
    private long currentCategoryId = 0;
    public CategoriesForKassaControl()
    {
        InitializeComponent();
        
    }

    public void SetVariablies(KassaPage kassaPage,ICategoryService categoryService)
    {
        _kassaPage = kassaPage;
        _categoryService = categoryService;
    }


    public async void GetAllCategories()
    {
        main_wrap.Visibility = Visibility.Visible;
        main_wrap.Children.Clear();
        foreach (var category in categorylist)
        {
            CategoryItemControl itemControl = new CategoryItemControl();
            itemControl.Tag = category.Id;
            itemControl.SerVariablies( _categoryService,this);
            itemControl.SetCategoryName(category.Name);
            itemControl.Margin = new Thickness(5,5,5,5);
            main_wrap.Children.Add(itemControl);
        }
    }
    public async void GetChildCategories(long? parentId)
    {
        categorylist = await _categoryService.GetCategoriesForSelect(parentId);
        if(categorylist != null && categorylist.Any())
            GetAllCategories();
    }

    public async void GetCategoryProducts(long categoryId)
    {
        currentCategoryId = categoryId;
        products_panel.Visibility = Visibility.Visible;
        main_wrap.Visibility = Visibility.Collapsed;
        var category = await _categoryService.GetByIdCategory(categoryId);
        if (category != null && category.ProductDtos.Any())
        {
            products_datagrid.ItemsSource = category.ProductDtos;
            products_datagrid.Items.Refresh();
        }
    }

    private void Products_datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var selected = products_datagrid.SelectedItem as ProductForSelect;
       _kassaPage.AddProductToCash(selected.Id);
        _kassaPage.kassa_right_doc.Visibility = Visibility.Visible;
        _kassaPage.category_doc.Visibility = Visibility.Collapsed;
        
        products_panel.Visibility = Visibility.Collapsed;
        categorylist = new List<Select>();
    
    }

    private void Refresh_btn_OnClick(object sender, RoutedEventArgs e)
    {
        if (currentCategoryId > 0)
        {
            GetCategoryProducts(currentCategoryId);
        }
    }

    private void Exit_btn_OnClick(object sender, RoutedEventArgs e)
    {
        main_wrap.Visibility = Visibility.Collapsed;
        products_datagrid.ItemsSource = null;
        products_panel.Visibility = Visibility.Collapsed;
        _kassaPage.kassa_right_doc.Visibility = Visibility.Visible;
    }
}