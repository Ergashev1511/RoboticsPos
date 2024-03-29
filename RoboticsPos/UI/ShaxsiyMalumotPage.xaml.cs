using System.Windows;
using System.Windows.Controls;

namespace RoboticsPos.UI;

public partial class ShaxsiyMalumotPage : UserControl
{
    private MainWindow _mainWindow { get; set; }

    public void SetMainWindow(MainWindow mainWindow)
    {
        this._mainWindow = mainWindow;
    }
    public ShaxsiyMalumotPage()
    {
        InitializeComponent();
    }

    private void Button_Back(object sender, RoutedEventArgs e)
    {
        _mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
        _mainWindow.Shaxsiy_kabinetViewBox.Visibility = Visibility.Hidden;
    }
}