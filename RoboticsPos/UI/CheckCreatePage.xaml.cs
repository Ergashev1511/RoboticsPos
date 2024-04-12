using System.Windows.Controls;

namespace RoboticsPos.UI;

public partial class CheckCreatePage : UserControl
{
    private MainWindow _mainWindow { get; set; }
    private SettingsPage _settingsPage { get; set; }
    private ClientForm _clientForm { get; set; }

    public void SetValues(MainWindow mainWindow, SettingsPage settingsPage, ClientForm clientForm)
    {
        _mainWindow = mainWindow;
        _settingsPage = settingsPage;
        _clientForm = clientForm;
    }
    public CheckCreatePage()
    {
        InitializeComponent();
    }
}