using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RoboticsPos.Data.Enum;
using RoboticsPos.Services;

namespace RoboticsPos.UI.Components;

public partial class GbridPayment : Window
{
    private KassaPage _kassaPage { get; set; }
    private IClientService _clientService { get; set; }
    public GbridPayment()
    {
        InitializeComponent();
    }

    public void SetVariablies(KassaPage kassaPage, IClientService clientService)
    {
        _kassaPage = kassaPage;
        _clientService = clientService;
        paysum_txt.Text = _kassaPage.chegirmabn_txt.Text.Length > 0 
            ? _kassaPage.chegirmabn_txt.Text
            : _kassaPage.summatxt.Text;
        
        naqd_txt.IsEnabled = plastic_txt.IsEnabled = false;
        if (client_combo.Items.Count == 0)
        {
            RefreshClients();
        }
    }

    private void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
    {
        CloseForm(); 
    }

    private void Naqd_checkbox_OnChecked(object sender, RoutedEventArgs e)
    {
        naqd_txt.IsEnabled = naqd_checkbox.IsChecked.Value;
        if (client_combo.Items.Count == 0)
        {
            RefreshClients();
        }
    }

    private void Card_checkbox_OnChecked(object sender, RoutedEventArgs e)
    {
        plastic_txt.IsEnabled = plastic_checkbox.IsChecked.Value;
        if (client_combo.Items.Count == 0)
        {
            RefreshClients();
        }

    }

    private void Naqd_txt_OnKeyUp(object sender, KeyEventArgs e)
    {
        if (naqd_txt.Text.Length > 0 && (decimal.Parse(naqd_txt.Text) - decimal.Parse(paysum_txt.Text) > 0))
        {
            qaytim_txt.Text = (decimal.Parse(naqd_txt.Text) - decimal.Parse(paysum_txt.Text)).ToString();
        }
        else
        {
            qaytim_txt.Text = "0.00";
        }
    }

    private void Plastic_txt_OnKeyUp(object sender, KeyEventArgs e)
    {
        if (plastic_txt.Text.Length > 0 && (decimal.Parse(plastic_txt.Text) - decimal.Parse(paysum_txt.Text) > 0))
        {
            qaytim_txt.Text = (decimal.Parse(plastic_txt.Text) - decimal.Parse(paysum_txt.Text)).ToString();
        }
        else
        {
            qaytim_txt.Text = "0.00";
        }
    }


    public void CloseForm()
    {
        paysum_txt.Text = naqd_txt.Text = qaytim_txt.Text = plastic_txt.Text = string.Empty;
        debt_doc.Visibility = Visibility.Collapsed;
        this.Close();
    }
    public async void RefreshClients()
    {
        _kassaPage.clients = await _clientService.GetClientForSelect();
        client_combo.ItemsSource = _kassaPage.clients.Select(a => a.Name);
        client_combo.Items.Refresh();
    }

    private void Payment_btn_OnClick(object sender, RoutedEventArgs e)
    {
        if (client_combo.Items.Count == 0)
        {
            RefreshClients();
        }
        
        decimal cash_sum =naqd_txt.Text.Length > 0 ? decimal.Parse(naqd_txt.Text) : 0;
        decimal card_sum = plastic_txt.Text.Length > 0 ? decimal.Parse(plastic_txt.Text) : 0;

        if (cash_sum >= decimal.Parse(paysum_txt.Text))
        {
            _kassaPage.SelectPaymentResult(PaymentType.Cash, cash_sum);
            CloseForm();
        }
        else if (card_sum >= decimal.Parse(paysum_txt.Text))
        {
            _kassaPage.SelectPaymentResult(PaymentType.Card, card_sum);
            CloseForm();
        }
        else if ((cash_sum + card_sum) >= decimal.Parse(paysum_txt.Text))
        {
            _kassaPage.SelectPaymentResult(PaymentType.Cash, cash_sum);
            _kassaPage.SelectPaymentResult(PaymentType.Card, card_sum);
            CloseForm();
        }
        else
        {
            decimal debtSum = decimal.Parse(paysum_txt.Text) - (cash_sum + card_sum);
            var messageBoxResult = MessageBox.Show("To'lov summasi yetarli emas, qarzga yozishni istaysizmi","Ogohlantirish!", MessageBoxButton.OKCancel);
           
            if (messageBoxResult == MessageBoxResult.OK)
            {
                if (_kassaPage.ClientId == 0)
                {
                    MessageBox.Show("Client tanlanmadi, Klientni tanlang!");
                    debt_doc.Visibility = Visibility.Visible;
                    payment_btn.IsEnabled = false;
                    return;
                }

                if (cash_sum > 0)
                {
                    _kassaPage.SelectPaymentResult( PaymentType.Cash, cash_sum);
                }

                if (card_sum > 0)
                {
                    _kassaPage.SelectPaymentResult( PaymentType.Card, card_sum);
                }
                
                _kassaPage.SelectPaymentResult( PaymentType.Debt, debtSum);

            }
        }
        CloseForm();
    }

    private void Client_combo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var debtor = _kassaPage.clients[client_combo.SelectedIndex];
        if (debtor != null)
        {
            _kassaPage.ClientId = debtor.Id;
            payment_btn.IsEnabled = true;
        }
    }

    private void Newdebtor_btn_OnClick(object sender, RoutedEventArgs e)
    {
        DebtorAddForm debtorAddForm = new DebtorAddForm();
        debtorAddForm.SetVariablies(this, _clientService);
        debtorAddForm.ShowDialog();
    }

  
}