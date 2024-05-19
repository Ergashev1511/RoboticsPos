using System.Windows;

namespace RoboticsPos.UI.Modalka;

public partial class PymentSucces : Window
{
    public PymentSucces()
    {
        InitializeComponent();
    }

    private void Ok_btn_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}