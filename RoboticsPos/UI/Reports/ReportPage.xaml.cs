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
    public ReportPage()
    {
        InitializeComponent();
    }

    public void SetVariablies(MainWindow mainWindow,DebtorService debtorService,IShopService shopService,IDebtPaymentSevice debtPaymentSevice)
    {
        _mainWindow = mainWindow;
        _debtorService = debtorService;
        _shopService = shopService;
        _debtPaymentSevice = debtPaymentSevice;
        debtor_control.SetVariablies(this,debtorService,debtPaymentSevice);
        shopHistory_control.SetVariablies(this,shopService);
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
        debtor_control.GetAllDebtors();
    }

    private void Shophistory_btn_OnClick(object sender, RoutedEventArgs e)
    {
        debtor_doc.Visibility = Visibility.Collapsed;
        shopHistory_doc.Visibility = Visibility.Visible;
        shopHistory_control.GetAllshops();
    }
}