using System.Windows;

namespace RoboticsPos.UI;

public partial class PaymentWindow : Window
{
    public PaymentWindow()
    {
        InitializeComponent();
    }

    private void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}