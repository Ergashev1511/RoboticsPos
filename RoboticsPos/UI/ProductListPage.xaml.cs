using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class ProductListPage : UserControl
{
     MainWindow _mainWindow { get; set; }
    private StoreControl _storeControl { get; set; }
    private ProductCreatePage _productCreatePage { get; set; }
    private IProductService _productService { get; set; }
    private ICategoryService _categoryService { get; set; }
    private IDiscountService _discountService { get; set; }
    private ICompanyService _companyService { get; set; }
    
    private List<ProductDTO> productDtos = new List<ProductDTO>();
     ProductDTO _productDto { get; set; }
    public async void SetVariablies(MainWindow mainWindow, StoreControl storeControl,ProductCreatePage productCreatePage,
        IProductService productService,ICategoryService categoryService,IDiscountService discountService,ICompanyService companyService)
    {
        _mainWindow = mainWindow;
        _storeControl = storeControl;
        _productCreatePage = productCreatePage;
        _productService = productService;
        _categoryService = categoryService;
        _discountService = discountService;
        _companyService = companyService;
        await GetAllProducts();
    }
    public ProductListPage()
    {
        InitializeComponent();
    }

    
    
    public async Task GetAllProducts()
    {
        productDtos = await _productService.GetAllProducts();

        if (productDtos != null && productDtos.Any())
        {
            users_datagrid.ItemsSource = productDtos;
        }
    }
    private void Users_datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        _storeControl.ProCreatePage.SetProductData(_productDto.Id,true);
        if (_productDto != null)
        {
            _storeControl.ProCreatePage.SetVariablies(_mainWindow, _storeControl, this, _productService,_categoryService,_discountService,_companyService);
            _productDto=users_datagrid.SelectedItem as ProductDTO; 
            
            _storeControl.ProCreate_doc.Visibility = Visibility.Visible;
            _storeControl.product_doc.Visibility = Visibility.Collapsed;
            
            _storeControl.ProCreatePage.saqlash_btn.Visibility = Visibility.Collapsed;
            _storeControl.ProCreatePage.Visibility = Visibility.Visible;
        }
    }

    private  void users_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _productDto=users_datagrid.SelectedItem as ProductDTO;
    }

    private void Button_Create(object sender, RoutedEventArgs e)
    {
        _storeControl.ProCreate_doc.Visibility = Visibility.Visible;
        _storeControl.product_doc.Visibility = Visibility.Collapsed;
    }

    private void Edit_btn_OnClick(object sender, RoutedEventArgs e)
    { 
       
        if (_productDto != null)
        {
            _storeControl.ProCreatePage.SetProductData(_productDto.Id);
            _storeControl.ProCreatePage.SetVariablies(_mainWindow,_storeControl,this,_productService,_categoryService,_discountService,_companyService);
          
            
            _storeControl.ProCreate_doc.Visibility = Visibility.Visible;
            _storeControl.product_doc.Visibility = Visibility.Collapsed;
        }
        else
        {
            MessageBox.Show("Selact any product!");
        }
    }

    private async void Delete_btn_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_productDto != null)
            {
                var resalt = MessageBox.Show("Do you want this Product?", "WARNING", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (resalt == MessageBoxResult.Yes)
                {
                    await _productService.DeleteProduct(_productDto.Id);
                    await GetAllProducts();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}