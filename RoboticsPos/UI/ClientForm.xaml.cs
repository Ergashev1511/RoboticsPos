using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class ClientForm : UserControl
{
    private MainWindow _mainWindow { get; set; }
    private SettingsPage _settings { get; set; }
    private ClientCreatePagae _clientCreatePagae { get; set; }
    private ClientDTO _clientDto { get; set; }
    private List<ClientDTO> _clientDtos { get; set; }
    private IClientService _clientService { get; set; }
    public async void SetValues(MainWindow mainWindow,SettingsPage settingsPage,ClientCreatePagae clientCreatePagae,IClientService clientService)
    {
        _mainWindow = mainWindow;
        _settings = settingsPage;
        _clientCreatePagae = clientCreatePagae;
        _clientService = clientService;
        await GetAllClients();
    }
    
    
    public ClientForm()
    {
        InitializeComponent();
    }

    public async Task GetAllClients()
    {
        _clientDtos = await _clientService.GetAll();
        if (_clientDtos != null && _clientDtos.Any())
        {
            client_datagrid.ItemsSource = _clientDtos;
        }
    }

    private void users_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _clientDto=client_datagrid.SelectedItem as ClientDTO;
    }

    private void Users_datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
              _settings.client_createPage.SetClientData(_clientDto.Id,true);
        if (_clientDto != null)
        {
            _settings.client_createPage.SetValues(_mainWindow,_settings,this,_clientService);
            _clientDto=client_datagrid.SelectedItem as ClientDTO;

            _settings.client_doc.Visibility = Visibility.Collapsed;
            _settings.client_creat.Visibility = Visibility.Visible;

            _settings.client_createPage.save_btn.Visibility = Visibility.Collapsed;
            _settings.client_createPage.cansel_btn.Visibility = Visibility.Visible;
        }
    }

    private async void Delete_btn_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_clientDto != null)
            {
                var res = MessageBox.Show("Do you want this Client", "WARNIG", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    await _clientService.Delete(_clientDto.Id);
                    await GetAllClients();
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
        if (_clientDto != null)
        {
            _settings.client_createPage.SetClientData(_clientDto.Id);
            _settings.client_createPage.SetValues(_mainWindow,_settings,this,_clientService);

            _settings.client_creat.Visibility = Visibility.Visible;
            _settings.client_doc.Visibility = Visibility.Collapsed;
        }
        else
        {
            MessageBox.Show("Select any Client");
        }
    }

    private void Button_Create(object sender, RoutedEventArgs e)
    {
         
        _settings.client_creat.Visibility = Visibility.Visible;
        _settings.client_doc.Visibility = Visibility.Collapsed;
    }
}