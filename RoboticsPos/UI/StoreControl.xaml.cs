using System.Windows;
using System.Windows.Controls;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class StoreControl : UserControl
{
     MainWindow _mainWindow { get; set; }
     private ProductCreatePage _productCreatePage { get; set; }
     private ProductListPage _productListPage { get; set; }
     private IProductService _productService { get; set; }
     private ICategoryService _categoryService { get; set; }
     private IDiscountService _discountService { get; set; }
     private ICompanyService _companyService { get; set; }
     
    public void SetMainWindow(MainWindow mainWindow,ProductCreatePage productCreatePage,ProductListPage productListPage,IProductService productService,
        ICategoryService categoryService,IDiscountService discountService,ICompanyService companyService)
    {
        _mainWindow = mainWindow;
        _productCreatePage = productCreatePage;
        _productListPage = productListPage;
        _productService = productService;
        _categoryService = categoryService;
        _discountService = discountService;
        _companyService = companyService;
        ProCreatePage.SetVariablies(mainWindow,this,productListPage,productService,categoryService,discountService,companyService);
        product_control.SetVariablies(mainWindow,this,productCreatePage,_productService,categoryService,discountService,companyService);
    }
    public StoreControl()
    {
        InitializeComponent();
    }

    private void Prolist_btn_OnClick(object sender, RoutedEventArgs e)
    {
        _productCreatePage.ClearForm();
        product_doc.Visibility = Visibility.Visible;
        product_control.SetVariablies(_mainWindow,this,_productCreatePage,_productService,_categoryService,_discountService,_companyService);
        ProCreate_doc.Visibility = Visibility.Collapsed;
    }

   

    private void Bixos_btn_OnClick(object sender, RoutedEventArgs e)
    {
        product_doc.Visibility = Visibility.Collapsed;
        _mainWindow.Store_doc.Visibility = Visibility.Hidden;
        _mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
    }
}