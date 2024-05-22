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
    private List<ShopProductForTable> _shopProductForTables = new();
    public ShopHistoryControl()
    {
        InitializeComponent();
    }

    public void SetVariablies(ReportPage reportPage, IShopService shopService)
    {
        _reportPage = reportPage;
        _shopService = shopService;
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
        _shopProductForTables = await _shopService.GetAllShopProducts();
        if(_shopProductForTables.Any())
        {
            shopproducts_datagrid.ItemsSource = _shopProductForTables;
            shopproducts_datagrid.Items.Refresh();
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
    }
}