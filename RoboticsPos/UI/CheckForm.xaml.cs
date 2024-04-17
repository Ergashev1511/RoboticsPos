using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class CheckForm : UserControl
{
    private MainWindow _mainWindow { get; set; }
    private CheckCreatePage _checkCreatePage { get; set; }
    private SettingsPage _settingsPage { get; set; }
    private ICheckPrinterService _checkPrinterServiceservice { get; set; }
    private List<CheckPrinterDataDTO> checkPrinterDataDtos { get; set; }
    private CheckPrinterDataDTO _checkPrinterDataDto { get; set; }
    public async void SetValues(MainWindow mainWindow,SettingsPage settingsPage, CheckCreatePage checkCreatePage,ICheckPrinterService checkPrinterServiceservice)
    {
        _mainWindow = mainWindow;
        _settingsPage = settingsPage;
        _checkCreatePage = checkCreatePage;
        _checkPrinterServiceservice = checkPrinterServiceservice;
        await GetAllChecks();
    }
    public CheckForm()
    {
        InitializeComponent();
    }
    
    public async Task GetAllChecks()
    {
        checkPrinterDataDtos = await _checkPrinterServiceservice.GetAll();
        if (checkPrinterDataDtos != null && checkPrinterDataDtos.Any())
        {
            checks_datagrid.ItemsSource = checkPrinterDataDtos;
        }
        
    }
    private void Users_datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        _settingsPage.checks_createPage.SetChecksData(_checkPrinterDataDto.Id,true);
        if (_checkPrinterDataDto != null)
        {
            _settingsPage.checks_createPage.SetValues(_mainWindow,_settingsPage,this,_checkPrinterServiceservice);
            _checkPrinterDataDto=checks_datagrid.SelectedItem as CheckPrinterDataDTO;
            
            _settingsPage.check_creat.Visibility = Visibility.Visible;
            _settingsPage.check_doc.Visibility = Visibility.Collapsed;

            _settingsPage.checks_createPage.save_btn.Visibility = Visibility.Collapsed;
            _settingsPage.checks_createPage.cansel_btn.Visibility = Visibility.Visible;
        }
        else
        {
            MessageBox.Show("Select any CheckPrinter");
        }
       
    }

    private void users_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _checkPrinterDataDto=checks_datagrid.SelectedItem as CheckPrinterDataDTO;
    }

    private async void Delete_btn_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_checkPrinterDataDto != null)
            {
                var res = MessageBox.Show("Do you want delete this ChekPrinters", "WARNING", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (res == MessageBoxResult.Yes)
                {
                    await _checkPrinterServiceservice.Delete(_checkPrinterDataDto.Id);
                    await GetAllChecks();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Edit_btn_OnClick(object sender, RoutedEventArgs e)
    {

        if (_checkPrinterDataDto != null)
        {
            _settingsPage.checks_createPage.SetChecksData(_checkPrinterDataDto.Id);
            _settingsPage.checks_createPage.SetValues(_mainWindow,_settingsPage,this,_checkPrinterServiceservice);

            _settingsPage.check_creat.Visibility = Visibility.Visible;
            _settingsPage.check_doc.Visibility = Visibility.Collapsed;
        }
        else
        {
            MessageBox.Show("Select any CheckPrinter");
        }
    }

    private void Button_Create(object sender, RoutedEventArgs e)
    {
        _settingsPage.check_doc.Visibility = Visibility.Collapsed;
        _settingsPage.check_creat.Visibility = Visibility.Visible;
    }
}