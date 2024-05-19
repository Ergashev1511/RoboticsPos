using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Repositories;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class CategoryForm : Window
{
    private CategoryCantrol _categoryCantrol { get; set; }
    private ICategoryService _categoryService { get; set; }
    private IProductService _productService { get; set; }
    private List<ProductForSelect> products { get; set; } = new();
    private ProuctCategoryDTO _CategoryDto { get; set; }
    private List<ProductForSelect> _productForSelects { get; set; } = new();
    private List<Select> parentCategories { get; set; } = new();
    private long selectedCategoryId { get; set; } = 0;
    
    public CategoryForm()
    {
        InitializeComponent();
    }



    public void SetVariablies(CategoryCantrol categoryCantrol, ICategoryService categoryService,
        IProductService productService)
    {
        _categoryCantrol = categoryCantrol;
        _categoryService = categoryService;
        _productService = productService;
        GetAllProduct();
        GetCategoryParents();
    }

// endi ProsuctDtos null b'lib qolyapdi
    public async void GetAllProduct()
    {
        if (!_productForSelects.Any())
        {
            _productForSelects = await _productService.GetAllForSelect();
        }
        products_datagrid.ItemsSource = _productForSelects;
        products_datagrid.Items.Refresh();
    }

    public async void GetCategoryParents()
    {
        parentCategories = await _categoryService.GetCategoriesForSelect(null);
        if (parentCategories.Any())
        {
            parent_combo.ItemsSource = parentCategories.Select(a => a.Name);
            parent_combo.Items.Refresh();
        }
    }
        
    private void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
    {
        ClearForm();
        this.Close();
    }

    private async void Save_btn_OnClick(object sender, RoutedEventArgs e)
    {
        ProuctCategoryDTO categoryDto = new ProuctCategoryDTO();

      
        categoryDto.Name = name_txt.Text; 
        categoryDto.Discription = describtion_txt.Text;
        categoryDto.ParentId = parentCategories.Any() ? (parent_combo.SelectedIndex > 0) ? parentCategories[parent_combo.SelectedIndex].Id : null : null;
        categoryDto.ProductDtos = _productForSelects.Any() ? _productForSelects.Where(s => s.Selected).ToList() : new ();

        if (products.Any(a => a.Selected))
        {
            categoryDto.ProductDtos = new List<ProductForSelect>();     
            categoryDto.ProductDtos.AddRange(products.Where(a=>a.Selected));
        }
        else
        {
            MessageBox.Show("Products doesn't select for discount creating!");
            return;
        }
        
        if (selectedCategoryId > 0)
        {
            await _categoryService.UpdateCategory(selectedCategoryId, categoryDto);
        }
        else
        {
            await _categoryService.CreateCategory(categoryDto);
        }
        _categoryCantrol.GetAllCategorys();
        ClearForm();
        this.Close();;
    }

    public void ClearForm()
    {
        name_txt.Text = "";
        describtion_txt.Text = "";
    }


    public async void FillForm(long Id,bool IsView=false)
    {
        var products = new List<ProductForSelect>(); 
        _CategoryDto = await _categoryService.GetByIdCategory(Id);
        products = _CategoryDto.ProductDtos;
        if (_CategoryDto != null)
        {
            DisableForm(IsView);
            selectedCategoryId = _CategoryDto.Id;
            name_txt.Text = _CategoryDto.Name;
            describtion_txt.Text = _CategoryDto.Discription;
            parent_combo.SelectedValue = _CategoryDto.ParentName;
            
            products_datagrid.ItemsSource = products;
            products_datagrid.Items.Refresh();
        }
    }
    private async void Refresh_btn_OnClick(object sender, RoutedEventArgs e)
    {
        if (selectedCategoryId > 0)
        {
            products = await _productService.GetProductsByIdsForDiscount(_CategoryDto.ProductDtos.Select(s=>s.Id).ToList());
        }
        else
        {
            products = await _productService.GetAllForSelect();
        }
        if (products != null && products.Any())
        {
            products_datagrid.ItemsSource = products;
            products_datagrid.Items.Refresh();
        }
    }

    private void CheckBox_OnChecked(object sender, RoutedEventArgs e)
    {
        CheckBox checkBox = sender as CheckBox;
        if (checkBox?.DataContext is ProductForSelect productForSelect)
        {
            productForSelect.Selected = true;
        }
    }

    private void CheckBox_OnUnchecked(object sender, RoutedEventArgs e)
    {
        CheckBox checkBox = sender as CheckBox;
        if (checkBox?.DataContext is ProductForSelect productForSelect)
        {
            productForSelect.Selected = false;
        }
    }

    private void DisableForm(bool isreadonly)
    {
        name_txt.IsReadOnly = isreadonly;
        describtion_txt.IsReadOnly = isreadonly;
        parent_combo.IsEnabled = !isreadonly;
        products_datagrid.IsEnabled = !isreadonly;
       
    }
    
}