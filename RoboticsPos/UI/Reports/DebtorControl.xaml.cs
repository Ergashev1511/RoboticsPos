using System.Windows;
using System.Windows.Controls;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Repositories;
using RoboticsPos.Services;

namespace RoboticsPos.UI.Reports;

public partial class DebtorControl : UserControl
{
    private ReportPage _reportPage { get; set; }
    private DebtorService _debtorService { get; set; }
    private IDebtPaymentSevice _debtPaymentSevice;
    private List<DebtorForTable> DebtorListDtos { get; set; }
    public DebtorControl()
    {
        InitializeComponent();
    }

    public  void SetVariablies(ReportPage reportPage,DebtorService debtorService,IDebtPaymentSevice debtPaymentSevice)
    {
        _reportPage = reportPage;
        _debtorService = debtorService;
        _debtPaymentSevice = debtPaymentSevice;
         GetAllDebtors();
    }


    public async void GetAllDebtors()
    {
          DebtorListDtos = await _debtorService.GetAllDebtors();
          if (DebtorListDtos.Any())
          {
              debtors_datagrid.ItemsSource = DebtorListDtos;
          }
    }
    private void ShopHistory_btn_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void DebtPay_btn_OnClick(object sender, RoutedEventArgs e)
    {
        var debtpay = debtors_datagrid.SelectedItem as DebtorForTable;
        if (debtpay != null)
        {
             DebtForPayment debtForPayment = new DebtForPayment();
             debtForPayment.SetVariablies(debtpay.Id,this,_debtPaymentSevice);
                    debtForPayment.GetDebtSum(debtpay.DebtSum.ToString());
                    debtForPayment.ShowDialog();
        }
       
    }
}