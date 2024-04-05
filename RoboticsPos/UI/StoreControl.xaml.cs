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
    public void SetMainWindow(MainWindow mainWindow,ProductCreatePage productCreatePage,ProductListPage productListPage,IProductService productService)
    {
        _mainWindow = mainWindow;
        _productCreatePage = productCreatePage;
        _productListPage = productListPage;
        _productService = productService;
        ProCreatePage.SetVariablies(mainWindow,this,productListPage,productService);
        product_control.SetVariablies(mainWindow,this,productCreatePage,_productService);
    }
    public StoreControl()
    {
        InitializeComponent();
    }

    private void Prolist_btn_OnClick(object sender, RoutedEventArgs e)
    {
        product_doc.Visibility = Visibility.Visible;
        product_control.SetVariablies(_mainWindow,this,_productCreatePage,_productService);
        ProCreate_doc.Visibility = Visibility.Collapsed;
    }

   

    private void Bixos_btn_OnClick(object sender, RoutedEventArgs e)
    {
        product_doc.Visibility = Visibility.Collapsed;
        _mainWindow.Store_doc.Visibility = Visibility.Hidden;
        _mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
    }
}