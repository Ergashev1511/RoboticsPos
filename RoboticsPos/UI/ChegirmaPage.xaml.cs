using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RoboticsPos.Common.DTOs;

namespace RoboticsPos.UI;

public partial class ChegirmaPage : Window
{
    private KassaPage _kassaPage { get; set; }
  
    public void SetValue(KassaPage kassaPage)
    {
        _kassaPage = kassaPage;
        
    }
    
    public ChegirmaPage()
    {
        InitializeComponent();
    }

    private void Chegback_btn_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    // private void Chegnatijasum_txt_OnTextChanged(object sender, TextChangedEventArgs e)
    // {
    //     decimal a = decimal.Parse(chegirmasum_txt.Text);
    //     decimal b = decimal.Parse(chegirmafoiz_txt.Text);
    //     chegnatijasum_txt.Text = (a + (a / 100) * b).ToString();
    // }




    private void Chegnatijasum_txt_OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        decimal a = decimal.Parse(chegirmasum_txt.Text);
        decimal b = decimal.Parse(chegirmafoiz_txt.Text);
        chegnatijasum_txt.Text = (a + (a / 100) * b).ToString();
    }


   
}