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


    public async void GetAllDebtPaymentds()
    {
        var debtpay = debtors_datagrid.SelectedItem as DebtorForTable;
        if (debtpay != null)
        {
            var debtpaymetns = await _debtPaymentSevice.GetAllDebtPayments(debtpay.Id);
            if (debtpaymetns.Any())
            {
                
            }
        }
    }

    public async void GetAllDebtors()
    {
          DebtorListDtos = await _debtorService.GetAllDebtors();
          if (DebtorListDtos.Any())
          {
              debtors_datagrid.ItemsSource = DebtorListDtos;
          }
    }

       public async void GetAllDebtPayments()
        {
           var debtpay = debtors_datagrid.SelectedItem as DebtorForTable;
           if(debtpay!=null)
           {
              var debtpayments = await _debtPaymentSevice.GetAllDebtPayments(debtpay.Id);
               if(debtpayments.Any())
             {
                 debtPayments_datagrid.ItemsSource = debtpayments;
                 debtPayments_datagrid.Items.Refresh();
              }
               else
               {
                   debtPayments_datagrid.ItemsSource = null;
                   debtPayments_datagrid.Items.Refresh();
               }
           }
         }
    private void ShopHistory_btn_OnClick(object sender, RoutedEventArgs e)
    {
        debtors_panel.Visibility = Visibility.Collapsed;
        debtPayments_panel.Visibility = Visibility.Visible;
        GetAllDebtPayments();
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

    private void Close_btn_OnClick(object sender, RoutedEventArgs e)
    {
        debtors_panel.Visibility = Visibility.Visible;
        debtPayments_panel.Visibility = Visibility.Collapsed;
        debtPayments_datagrid.ItemsSource = null;
    }
}