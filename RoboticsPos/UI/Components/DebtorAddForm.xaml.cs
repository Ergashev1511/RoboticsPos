using System.Windows;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI.Components;

public partial class DebtorAddForm : Window
{
    private GbridPayment _gbridPayment { get; set; }
    private IClientService _clientService { get; set; }
    public DebtorAddForm()
    {
        InitializeComponent();
    }

    public void SetVariablies(GbridPayment gbridPayment, IClientService clientService)
    {
        _gbridPayment = gbridPayment;
        _clientService = clientService;
    }

    private void Close_btn_OnClick(object sender, RoutedEventArgs e)
    {
        firstname_txt.Text = lastname_txt.Text = phoneNumber_txt.Text = string.Empty;
        this.Close();
    }

    private async void DebtorAdd_btn_OnClick(object sender, RoutedEventArgs e)
    {
        if (firstname_txt.Text.Length == 0 || lastname_txt.Text.Length == 0 || phoneNumber_txt.Text.Length == 0)
        {
            MessageBox.Show("Iltimos maydonlarni to'ldiring!");
            return;
        }

        DebtorDTO debtorDto = new DebtorDTO()
        {
            FirstName = firstname_txt.Text,
            LastName = lastname_txt.Text,
            PhoneNumber = phoneNumber_txt.Text
        };
        await _clientService.CreateDebtor(debtorDto);
        _gbridPayment.RefreshClients();
        firstname_txt.Text = lastname_txt.Text = phoneNumber_txt.Text = string.Empty;
        this.Close();
    }
}