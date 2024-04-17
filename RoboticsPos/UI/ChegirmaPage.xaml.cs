using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RoboticsPos.Common.DTOs;

namespace RoboticsPos.UI;

public partial class ChegirmaPage : Window
{
    private decimal lastsumma;
    private KassaPage _kassaPage { get; set; }
  
    public void SetValue(KassaPage kassaPage)
    {
        _kassaPage = kassaPage;
        
    }
    
    public ChegirmaPage()
    {
        InitializeComponent();
    }

    public void Chegback_btn_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    public void SetSumma(decimal totalsum)
    {
        chegirmasum_txt.Text = totalsum.ToString();
    }

    private void Chegirmafoiz_txt_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        decimal a = decimal.Parse(chegirmasum_txt.Text);
        decimal b = decimal.Parse(chegirmafoiz_txt.Text);
        lastsumma = a - (a / 100) * b;
        chegnatijasum_txt.Text = lastsumma.ToString(); 
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
        _kassaPage.SetSum(lastsumma);
    }
}