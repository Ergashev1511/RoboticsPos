using System.Windows;
using System.Windows.Controls;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class ProductCreatePage : UserControl
{
    private StoreControl _storeControl { get; set; }
    private MainWindow _mainWindow { get; set; }
    private ProductListPage _productListPage { get; set; }
    private IProductService _productService { get; set; }
    private long ProductId = 0;
    public void SetVariablies(MainWindow mainWindow, StoreControl storeControl,ProductListPage productListPage,IProductService productService)
    {
        _mainWindow = mainWindow;
        _storeControl = storeControl;
        _productListPage = productListPage;
        _productService = productService;
        _productListPage.SetVariablies(mainWindow,storeControl,this,productService);
    }
    public ProductCreatePage()
    {
        InitializeComponent();
    }


    public async void SetProductData(long Id, bool IsView = false)
    {
        if (Id > 0)
        {
            DisableForm(IsView);
            ProductId = Id;
            var product = await _productService.GetProductById(Id);
            if (product != null)
            {
                nametxt.Text = product.Name;
                barcodetxt.Text = product.BarCode;
                amounttxt.Text = product.Amount.ToString();
                amountinpacakgetxt.Text = product.AmountInPackage.ToString();
                actualpricetxt.Text = product.ActualPrice.ToString();
                pricetxt.Text = product.Price.ToString();
                priceofpiesetxt.Text = product.PriceOfPiece.ToString();

            }
        }
    }
    
    
    
    private async void Saqlash_btn_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            ProductDTO productDto = new ProductDTO()
            {
               
                Name = nametxt.Text,
                BarCode = barcodetxt.Text,
                AmountInPackage = int.Parse(amountinpacakgetxt.Text),
                Amount = int.Parse(amounttxt.Text),
                ActualPrice = int.Parse(actualpricetxt.Text),
                Price = int.Parse(pricetxt.Text),
                PriceOfPiece = int.Parse(priceofpiesetxt.Text)
            };
            if (ProductId == 0)
            {
                await _productService.CreateProduct(productDto);
            }
            else
            {
                await _productService.UpdateProduct(ProductId,productDto);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        ClearForm();
       await _productListPage.GetAllProducts();
       _storeControl.ProCreate_doc.Visibility = Visibility.Collapsed;
       _storeControl.product_doc.Visibility = Visibility.Visible;
    }

    private void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
    {
        ClearForm();
        _storeControl.ProCreate_doc.Visibility = Visibility.Collapsed;
        _storeControl.product_doc.Visibility = Visibility.Visible;
    }


    public void ClearForm()
    {
        nametxt.Text = "";
        barcodetxt.Text = "";
        amounttxt.Text = "";
        amountinpacakgetxt.Text = "";
        actualpricetxt.Text = "";
        pricetxt.Text = "";
        priceofpiesetxt.Text = "";
    }

    public void DisableForm(bool isReadOnly)
    {
        nametxt.IsEnabled = !isReadOnly;
        barcodetxt.IsEnabled = !isReadOnly;
        amounttxt.IsEnabled = !isReadOnly;
        amountinpacakgetxt.IsEnabled = !isReadOnly;
        pricetxt.IsEnabled = !isReadOnly;
        actualpricetxt.IsEnabled = !isReadOnly;
        priceofpiesetxt.IsEnabled = !isReadOnly;
    }
}