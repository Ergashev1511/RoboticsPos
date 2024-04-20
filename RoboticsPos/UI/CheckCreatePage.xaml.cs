using System.Windows;
using System.Windows.Controls;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class CheckCreatePage : UserControl
{
    private MainWindow _mainWindow { get; set; }
    private SettingsPage _settingsPage { get; set; }
    private CheckForm _checkForm { get; set; }
    private ICheckPrinterService _checkPrinterService { get; set; }
    private long checkprintId = 0;

    public void SetValues(MainWindow mainWindow, SettingsPage settingsPage,CheckForm checkForm,ICheckPrinterService checkPrinterService)
    {
        _mainWindow = mainWindow;
        _settingsPage = settingsPage;
        _checkForm = checkForm;
        _checkPrinterService = checkPrinterService;
        _checkForm.SetValues(mainWindow,settingsPage,this,checkPrinterService);
    }
    public CheckCreatePage()
    {
        InitializeComponent();
    }



    public async void SetChecksData(long Id, bool IsView = false)
    {
        if (Id > 0)
        {
            Disable(IsView);
            checkprintId = Id;
            var checks = await _checkPrinterService.GetById(Id);
            if (checks != null)
            {
                headertxt.Text = checks.Header;
                footertxt.Text = checks.Footer;
                taratxt.Text = checks.Tara;
                tintxt.Text = checks.TIN;
                printertxt.Text = checks.Printer;
            }
        }
    }

    private async void Save_btn_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            CheckPrinterDataDTO checkPrinterDataDto = new CheckPrinterDataDTO()
            {
                Header = headertxt.Text,
                Footer = footertxt.Text,
                Tara = taratxt.Text,
                TIN = tintxt.Text,
                Printer = printertxt.Text
            };
            if (checkprintId == 0)
            {
                await _checkPrinterService.Create(checkPrinterDataDto);
                ClearForm();
            }
            else
            {
                await _checkPrinterService.Update(checkprintId,checkPrinterDataDto);
                ClearForm();
            }
            ClearForm();
            await _checkForm.GetAllChecks();
            _settingsPage.check_doc.Visibility = Visibility.Visible;
            _settingsPage.check_creat.Visibility = Visibility.Hidden;

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
    {
        ClearForm();
        Disable(false);
        _settingsPage.check_creat.Visibility = Visibility.Collapsed;
        _settingsPage.check_doc.Visibility = Visibility.Visible;
    }

    public void Disable(bool IsView)
    {
        headertxt.IsReadOnly = IsView;
        footertxt.IsReadOnly = IsView;
        taratxt.IsReadOnly =IsView;
        tintxt.IsReadOnly = IsView;
        printertxt.IsReadOnly = IsView;
    }

    public void ClearForm()
    {
        headertxt.Text = "";
        footertxt.Text = "";
        taratxt.Text = "";
        tintxt.Text = "";
        printertxt.Text = "";
    }
}
