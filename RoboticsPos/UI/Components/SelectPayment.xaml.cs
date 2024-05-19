using System.Windows;
using RoboticsPos.Data.Enum;
using RoboticsPos.UI.Modalka;

namespace RoboticsPos.UI.Components;

public partial class SelectPayment : Window
{
    private KassaPage _kassaPage { get; set; }
    public SelectPayment()
    {
        InitializeComponent();
    }

    public void SetVariablies(KassaPage kassaPage)
    {
        _kassaPage = kassaPage;
    }

    private void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
    {
        payment_sum.Text = string.Empty;
        this.Close();
    }

    public void GetPaymentSum(string summa)
    {
        payment_sum.Text = summa;
    }

    private void Naqd_btn_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _kassaPage.SelectPaymentResult(PaymentType.Cash);
            payment_sum.Text = string.Empty;
            this.Close();
            PymentSucces pymentSucces = new PymentSucces();
            pymentSucces.ShowDialog();
        }
        catch (Exception exception)
        {
            PaymentFailed paymentFailed = new PaymentFailed();
            paymentFailed.ShowDialog();
        }
        
    }

    private void Cart_btn_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
           _kassaPage.SelectPaymentResult(PaymentType.Card);
            PymentSucces pymentSucces = new PymentSucces();
            payment_sum.Text = string.Empty; 
            this.Close();
            pymentSucces.ShowDialog();
        }
        catch (Exception exception)
        {
            PaymentFailed paymentFailed = new PaymentFailed();
            paymentFailed.ShowDialog();
        }
      
    }

    private void Gbrid_btn_OnClick(object sender, RoutedEventArgs e)
    {
        _kassaPage.ShowGibridPayment();
        payment_sum.Text = string.Empty;
        this.Close();
    }
}