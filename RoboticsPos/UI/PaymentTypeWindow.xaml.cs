using System.Windows;

namespace RoboticsPos.UI;

public partial class PaymentTypeWindow : Window
{
    public PaymentTypeWindow()
    {
        InitializeComponent();
    }

    private void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}