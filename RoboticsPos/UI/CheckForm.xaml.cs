using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RoboticsPos.UI;

public partial class CheckForm : UserControl
{
    private MainWindow _mainWindow { get; set; }
    private CheckCreatePage _checkCreatePage { get; set; }
    private SettingsPage _settingsPage { get; set; }
    public void SetValues(MainWindow mainWindow,SettingsPage settingsPage, CheckCreatePage checkCreatePage)
    {
        _mainWindow = mainWindow;
        _settingsPage = settingsPage;
        _checkCreatePage = checkCreatePage;
    }
    public CheckForm()
    {
        InitializeComponent();
    }

    private void Users_datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void users_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Delete_btn_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Edit_btn_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Button_Create(object sender, RoutedEventArgs e)
    {
        _settingsPage.check_doc.Visibility = Visibility.Collapsed;
        _settingsPage.check_creat.Visibility = Visibility.Visible;
    }
}