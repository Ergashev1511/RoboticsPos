using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RoboticsPos.Common.DTOs;

namespace RoboticsPos.UI;

public partial class ChegirmaPage : Window
{
   
    public decimal result { get; set; }  // c
    public decimal summa { get; set; }//a
    public decimal percent { get; set; }//b
    private KassaPage _kassaPage { get; set; }
  
    public void SetValues(KassaPage kassaPage,string Summa)
    {
        // bu yerda projectni to'g'irlashim kerak githubga qarab!
        chegirmasum_txt.Text = Summa;
        summa = decimal.Parse(Summa);
    }
    
    public ChegirmaPage()
    {
        InitializeComponent();
    }

    public void Chegback_btn_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    public void SetSumma(KassaPage kassaPage,decimal totalsum)
    {
        _kassaPage = kassaPage;
        chegirmasum_txt.Text = totalsum.ToString();    // savdo qilingan umumiy summani chegirma page ga olish
    }

  
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if(result < summa)
        {
             _kassaPage.SetSum(result);
            chegirmafoiz_txt.Text = chegirmasum_txt.Text = chegnatijasum_txt.Text = string.Empty;
            this.Close();
        }
        else
        {
            MessageBox.Show("Ortiqcha summa kiritildi");
        }
    }

   

    private void ChegirmaPage_OnKeyUp(object sender, KeyEventArgs e)
    {
        if (chegirmafoiz_txt.Text.Length > 0 && chegirmafoiz_txt.IsFocused)
        {
            summa = decimal.Parse(chegirmasum_txt.Text);
            percent = decimal.Parse(chegirmafoiz_txt.Text);
            result = Math.Round(summa - (summa / 100) * percent,3);
            chegnatijasum_txt.Text = result.ToString(); 
        }
        if (chegnatijasum_txt.Text.Length > 0 && chegnatijasum_txt.IsFocused )
        {
            result = decimal.Parse(chegnatijasum_txt.Text);
            summa = decimal.Parse(chegirmasum_txt.Text);
            percent = Math.Round(((summa - result) / summa) * 100,3);
            chegirmafoiz_txt.Text=percent.ToString();
        }
    }
}