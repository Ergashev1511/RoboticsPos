using System.Windows;
using System.Windows.Controls;

namespace RoboticsPos.UI;

public partial class EditCountForm : Window
{
    private decimal _totalAmount { get; set; }
    private KassaPage _kassaPage { get; set; }
    public EditCountForm()
    {
        InitializeComponent();
    }

    public void SetValues(KassaPage kassaPage,decimal totalAmount,decimal amount)
    {
        _kassaPage = kassaPage;
        _totalAmount = totalAmount;
        label_umumiysoni.Content = totalAmount.ToString();
       
        amount_txt.Text = amount.ToString();
    }

    private void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
    {
       this.Close();
    }

    private void Tahrirlash_btn_OnClick(object sender, RoutedEventArgs e)
    {
        if (amount_txt.Text.Length > 0)
        {
            if (decimal.Parse(amount_txt.Text) <= _totalAmount)
            {
                _kassaPage.ChangeQuantityProuct(decimal.Parse(amount_txt.Text));
                this.Close();
            }
        }
        
    }

    private void Amount_txt_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (amount_txt.Text.Length > 0 )
        {
            if(decimal.Parse(amount_txt.Text) >= _totalAmount)
            {
                amount_txt.Text = amount_txt.Text.Substring(0, amount_txt.Text.Length - 1);
            }
        }
        else
        {
             
        }
    }
}