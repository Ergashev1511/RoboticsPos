using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class Mijozlar : UserControl
{
    private KassaPage _kassaPage { get; set; }
    private IClientService _clientService { get; set; }
    private ClientDTO _clientDto { get; set; }
    private List<ClientDTO> _clientDtos = new List<ClientDTO>();
  
    public Mijozlar()
    {
        InitializeComponent();
    }
    
    public async void GetAllClients()
    {
        _clientDtos = await _clientService.GetAll();
        if (_clientDtos.Any())
        {
            client_datagrid.ItemsSource = _clientDtos;
            client_datagrid.Items.Refresh();
        }
    }
 
    public void SetVariablies(KassaPage kassaPage,IClientService clientService)
    {
        _kassaPage = kassaPage;
        _clientService = clientService;
        GetAllClients();
    }


    private void Client_datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (_clientDto != null)
        {
            _kassaPage.ClientGetName(_clientDto.FullName);
            _kassaPage.mijoz_doc.Visibility = Visibility.Collapsed;
            _kassaPage.kassa_right_doc.Visibility = Visibility.Visible;
        }
        else
        {
            MessageBox.Show("Client is not found!");
        }
        
    }

    private async void Search_textbox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        //var clients = await _clientService.GetAll();
        if (search_textbox.Text.Length >= 2)
        {
           client_datagrid.ItemsSource =_clientDtos.Where(a => a.FullName.ToLower().Contains(search_textbox.Text.ToLower())).ToList();
        }
        else
        {
            client_datagrid.ItemsSource = _clientDtos.Take(10);
            client_datagrid.Items.Refresh();
        }
    }

    private void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
    {
        _kassaPage.mijoz_doc.Visibility = Visibility.Collapsed;
        _kassaPage.kassa_right_doc.Visibility = Visibility.Visible;
    }

    private void Client_datagrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _clientDto=client_datagrid.SelectedItem as ClientDTO;
    }
}