using System.Windows;
using System.Windows.Controls;
using RoboticsPos.Services;

namespace RoboticsPos.UI.Reports;

public partial class ReportPage : UserControl
{
    private MainWindow _mainWindow { get; set; }
    private DebtorService _debtorService;
    private IShopService _shopService;
    private IDebtPaymentSevice _debtPaymentSevice;
    private IProductService _productService;
    private IPaymentService _paymentService;
    private KassaPage _kassaPage { get; set; }
    public ReportPage()
    {
        InitializeComponent();
    }

    public void SetVariablies(MainWindow mainWindow,DebtorService debtorService,IShopService shopService,IDebtPaymentSevice debtPaymentSevice,IProductService productService,IPaymentService paymentService,KassaPage kassaPage)
    {
        _mainWindow = mainWindow;
        _debtorService = debtorService;
        _shopService = shopService;
        _debtPaymentSevice = debtPaymentSevice;
        _productService = productService;
        _paymentService = paymentService;
        _kassaPage = kassaPage;


    }
    private void Back_btn_OnClick(object sender, RoutedEventArgs e)
    {
        _mainWindow.hisobot_doc.Visibility = Visibility.Collapsed;
        _mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
    }

    private void Debtor_btn_OnClick(object sender, RoutedEventArgs e)
    {
        debtor_doc.Visibility = Visibility.Visible;
        shopHistory_doc.Visibility = Visibility.Collapsed;
        debtor_control.SetVariablies(this,_debtorService,_debtPaymentSevice);
        debtor_control.GetAllDebtors();
    }

    private void Shophistory_btn_OnClick(object sender, RoutedEventArgs e)
    {
        debtor_doc.Visibility = Visibility.Collapsed;
        shopHistory_doc.Visibility = Visibility.Visible;
        shopHistory_control.SetVariablies(this,_shopService,_productService,_paymentService,_kassaPage);
        shopHistory_control.GetAllshops();
    }
}