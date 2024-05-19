using System.Windows;

namespace RoboticsPos.UI.Modalka;

public partial class PaymentFailed : Window
{
    public PaymentFailed()
    {
        InitializeComponent();
    }

    private void Ok_btn_OnClick(object sender, RoutedEventArgs e)
    {
      this.Close();
    }
}