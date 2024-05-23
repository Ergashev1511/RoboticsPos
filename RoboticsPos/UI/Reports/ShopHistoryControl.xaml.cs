using System.Windows;
using System.Windows.Controls;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI.Reports;

public partial class ShopHistoryControl : UserControl
{
    private ReportPage _reportPage;
    private IShopService _shopService;
    private List<ShopHistoryForTable> shopHistoryForTable = new();
    private IProductService _productService;
    private IPaymentService _paymentService;
    private List<ProductDTO> ProductDtos = new();
    private List<ShopProductForTable> ShopProductForTables = new();
    private KassaPage _kassaPage { get; set; }
    private ShopProductForTable shopProductForTable = new ShopProductForTable();
    public ShopHistoryControl()
    {
        InitializeComponent();
    }

    public void SetVariablies(ReportPage reportPage, IShopService shopService,IProductService productService,IPaymentService paymentService,KassaPage kassaPage)
    {
        _reportPage = reportPage;
        _shopService = shopService;
        _productService = productService;
        _paymentService = paymentService;
        _kassaPage = kassaPage;
        GetAllshops();
        
    }

    public async void GetAllshops()
    {
        shopHistoryForTable = await _shopService.GetAllShops();
        if (shopHistoryForTable.Any())
        {
            shophistory_datagrid.ItemsSource = shopHistoryForTable;
            shopproducts_datagrid.Items.Refresh();
        }
    }

    public async void GetAllShopProducts()
    {
      var productDto = shopHistoryForTable[shophistory_datagrid.SelectedIndex];
      if (productDto != null)
      {
          var newproducts = await _productService.GetAllShopProducts(productDto.Id);
          if (newproducts.Any())
          {
              shopproducts_datagrid.ItemsSource = newproducts;
              shopproducts_datagrid.Items.Refresh();
          }
      }
    }

  

    public async void GetAllPaymens()
    {
        var productDto = shopHistoryForTable[shophistory_datagrid.SelectedIndex];
        if (productDto != null)
        {
            var payments = await _paymentService.GetAllPayments(productDto.Id);
            if (payments.Any())
            {
                paymetns_datagrid.ItemsSource = payments;
                paymetns_datagrid.Items.Refresh();
            }
        }
    }

    private void Products_btn_OnClick(object sender, RoutedEventArgs e)
    {
        shopHistory_panel.Visibility = Visibility.Collapsed;
        payments_panel.Visibility = Visibility.Collapsed;
        products_panel.Visibility = Visibility.Visible;
        GetAllShopProducts();
    }

    private void Payments_btn_OnClick(object sender, RoutedEventArgs e)
    {
        payments_panel.Visibility = Visibility.Visible;
        shopHistory_panel.Visibility = Visibility.Collapsed;
        products_panel.Visibility = Visibility.Collapsed;
        GetAllPaymens();
    }

    private void Exit_btn_OnClick(object sender, RoutedEventArgs e)
    {
        products_panel.Visibility = Visibility.Collapsed;
        shopHistory_panel.Visibility = Visibility.Visible;
        payments_panel.Visibility = Visibility.Collapsed;
        shopproducts_datagrid.ItemsSource = null;
    }

    private void Back_btn_OnClick(object sender, RoutedEventArgs e)
    {
        products_panel.Visibility = Visibility.Collapsed;
        shopHistory_panel.Visibility = Visibility.Visible;
        payments_panel.Visibility = Visibility.Collapsed;
        paymetns_datagrid.ItemsSource = null;
    }
}