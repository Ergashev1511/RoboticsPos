using System.Windows;
using System.Windows.Controls;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class ClientCreatePagae : UserControl
{
    private MainWindow _mainWindow { get; set; }
    private IClientService _clientService { get; set; }
    private SettingsPage _settings { get; set; }
    private ClientForm _clientForm { get; set; }
    
    private long ClientId = 0;
    public void SetValues(MainWindow mainWindow,  SettingsPage settingsPage,ClientForm clientForm,IClientService clientService)
    {
        _mainWindow = mainWindow;
        _settings = settingsPage;
        _clientForm = clientForm;
        _clientService = clientService;
        _clientForm.SetValues(mainWindow,settingsPage,this,clientService);
    }
    public ClientCreatePagae()
    {
        InitializeComponent();
    }


    public async void SetClientData(long Id, bool IsView=false)
    {
        if (Id > 0)
        {
            Disable(IsView);
            ClientId = Id;
            var client = await _clientService.GetById(Id);
            if (client != null)
            {
                firstnametxt.Text = client.FirstName;
                lastnametxt.Text = client.LastName;
                fathernametxt.Text = client.FathersName;
                phoneNumbertxt.Text = client.PhoneNumber;
                addresstxt.Text = client.Address;
                borndatepicer.SelectedDate = client.BornDate;
            }
        }
        
    }
    
    private async void Save_btn_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            ClientDTO clientDto = new ClientDTO()
            {
                FirstName = firstnametxt.Text,
                LastName = lastnametxt.Text,
                FathersName = fathernametxt.Text,
                PhoneNumber = phoneNumbertxt.Text,
                Address = addresstxt.Text,
                BornDate = borndatepicer.SelectedDate.Value
            };
            if (ClientId == 0)
            {
                await _clientService.Create(clientDto);
                ClearForm();
            }
            else
            {
                await _clientService.Update(ClientId, clientDto);
                ClearForm();
            }
            ClearForm();
            await _clientService.GetAll();

            _settings.client_doc.Visibility = Visibility.Visible;
            _settings.client_creat.Visibility = Visibility.Collapsed;
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
        _settings.client_creat.Visibility = Visibility.Collapsed;
        _settings.client_doc.Visibility = Visibility.Visible;
    }

    public void ClearForm()
    {
        firstnametxt.Text = "";
        lastnametxt.Text = "";
        fathernametxt.Text = "";
        phoneNumbertxt.Text = "";
        addresstxt.Text = "";
        borndatepicer.Text = "";  // buni tekshirib ko'rish kk
        
    }

    
    // Disable() funksiya ishlashida muammo chiqayapdi
    public void Disable( bool IsView)
    {
        firstnametxt.IsReadOnly = IsView;  // Bu yerda kamchiliklarni to'g'irlashim kerak
        lastnametxt.IsReadOnly = IsView;
        fathernametxt.IsReadOnly = IsView;
        phoneNumbertxt.IsReadOnly = IsView;
        addresstxt.IsReadOnly = IsView;
        borndatepicer.IsEnabled = !IsView;
    }
}