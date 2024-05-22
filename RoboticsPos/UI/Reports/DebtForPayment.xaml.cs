using System.Windows;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Enum;
using RoboticsPos.Data.Models;
using RoboticsPos.Services;

namespace RoboticsPos.UI.Reports;

public partial class DebtForPayment : Window
{
    private DebtorControl _debtorControl;
    private IDebtPaymentSevice _debtPaymentSevice;
    private long DebtId { get; set; }
    public DebtForPayment()
    {
        InitializeComponent();
    }

    public void SetVariablies(long debtId,DebtorControl debtorControl,IDebtPaymentSevice debtPaymentSevice)
    {
        _debtorControl = debtorControl;
        _debtPaymentSevice = debtPaymentSevice;
        DebtId = debtId;
    }
    private void Close_btn_OnClick(object sender, RoutedEventArgs e)
    {
        pay_sum.Text = string.Empty;
        this.Close();
    }

    private void Cash_btn_OnClick(object sender, RoutedEventArgs e)
    {
        DebtPaymentCreateModel(PaymentType.Cash);
        this.Close();
    }

    private void Cart_btn_OnClick(object sender, RoutedEventArgs e)
    {
        DebtPaymentCreateModel(PaymentType.Card);
        this.Close();
    }

    public void GetDebtSum(string debtsum)
    {
        debt_sum.Content = debtsum;
    }

    private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
    {
        
            pay_sum.Text = debt_sum.Content.ToString();
        
      
    }

    private void AllPay_chekbox_OnUnchecked(object sender, RoutedEventArgs e)
    {
            pay_sum.Text = string.Empty;
    }
    
    private async void DebtPaymentCreateModel(PaymentType  paymentType)
    {
        if (pay_sum.Text.Length == 0)
        {
            MessageBox.Show("Summa kiritilmadi!");
            return;
        }

        if (decimal.Parse(debt_sum.Content.ToString()) < (pay_sum.Text.Length > 0 ? decimal.Parse(pay_sum.Text) : 0))
        {
            MessageBox.Show("Ortiqcha Summa kiritilmadi!");
            return;
        }

        DebtPaymentDTO debtPaymentDto = new DebtPaymentDTO()
        {
            DebtId = DebtId,
            Payment = new PaymentDTO()
            {
                Sum = decimal.Parse(debt_sum.Content.ToString()),
                PayedSum = decimal.Parse(pay_sum.Text),
                RemainingSum = decimal.Parse(debt_sum.Content.ToString()) - decimal.Parse(pay_sum.Text),
                PaymentStatus = PaymentStatus.Payed,
                PaymentType = paymentType,
            }
        };
        await _debtPaymentSevice.CreateDebtPayment(debtPaymentDto);
    }
}