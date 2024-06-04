using System.Windows;
using System.Windows.Controls;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Repositories;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class LoginPage : UserControl
{
    private MainWindow _mainWindow { get; set; }
    private  IUserService userService { get; set; }
   // private  EmployeeRepository _repository { get; set; }
    
  
    public LoginPage()
    {
        InitializeComponent();
        
    }

    public void SetVariablies(IUserService userService,MainWindow mainWindow)
    {
        this.userService = userService;
        this._mainWindow = mainWindow;
       
    }

    private async void Button_Kirish(object sender, RoutedEventArgs e)
    {
        
        if(txtlogin.Text != "" || txtPassword.Password != "")
        {
            try
            {
                if (await userService.LoginByUsername(txtlogin.Text, txtPassword.Password))
                {
                    _mainWindow.menyupage.GetUserName(txtlogin.Text,txtPassword.Password);
                    _mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
                    _mainWindow.LoginViewbox.Visibility = Visibility.Hidden;
                    txtlogin.Text=string.Empty;
                    txtPassword.Password =string.Empty;
                    
                }
                else
                {
                    MessageBox.Show("Username yoki parol xato!");
                }
            }
            catch (NullReferenceException exception)
            {
                MessageBox.Show("Username yoki parol xato!");
                txtlogin.Text=string.Empty;
                txtPassword.Password =string.Empty;
            }
        }
        else 
        {
            MessageBox.Show("Username yoki parol kirtilmagan!");
        }
        
    }

    private void Button_Back(object sender, RoutedEventArgs e)
    {
        _mainWindow.LoginViewbox.Visibility = Visibility.Hidden;
        _mainWindow.KirishViewBox.Visibility = Visibility.Visible;
    }
}