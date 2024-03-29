using System.Windows;
using System.Windows.Controls;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Repositories;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class LoginPage : UserControl
{
    private MainWindow _mainWindow { get; set; }
    private readonly UserService userService;
    private readonly EmployeeRepository _repository;
    
    public void SetMainWinndow(MainWindow mainWindow)
    {
        this._mainWindow = mainWindow;
    }
    public LoginPage()
    {
        InitializeComponent();
        
    }

    public LoginPage(UserService userService,EmployeeRepository repository)
    {
        this.userService = userService;
        this._repository = repository;
    }

    private async void Button_Kirish(object sender, RoutedEventArgs e)
    {
        // var data = await _repository.GetAllEmployee();
        //
        // foreach (var i in data)
        // {
        //     if (i.User.UserName == txtlogin.Text && i.User.Password == txtPassword.Password)
        //     {
        //         _mainWindow.LoginViewbox.Visibility = Visibility.Hidden;
        //         _mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
        //         
        //     }
        //     else
        //     {
        //         MessageBox.Show("Username not found!");
        //         txtlogin.Text = "";
        //         txtPassword.Password = "";
        //     }
        // }


        if (txtlogin.Text == "ergashev1511" && txtPassword.Password == "151103")
        {
            _mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
            _mainWindow.LoginViewbox.Visibility = Visibility.Hidden;

        }
        else
        {
            MessageBox.Show(" Username yoki parol xato!");
            txtlogin.Text = "";
            txtPassword.Password = "";
        }

        //  try
        //  {
        //  var data = userService.LoginByUsername(txtlogin.Text, txtPassword.Password);

        ////  if (data == null) throw new Exception("This Username not found!");
        //if (data == null)
        //{
        //    MessageBox.Show("Bazada malumot yoq");
        //}

        //  if (data.Result == true )
        //  {
        //      _mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
        //      _mainWindow.LoginViewbox.Visibility = Visibility.Hidden;
        //  }
        //  else
        //  {
        //      MessageBox.Show(" Username yoki parol xato!");
        //  }
        //  }
        //  catch (NullReferenceException exception)
        //  {
        //      MessageBox.Show("xdxskjdejfejcbdn jcvfndjkvfb");
        //  }

    }

    private void Button_Back(object sender, RoutedEventArgs e)
    {
        _mainWindow.LoginViewbox.Visibility = Visibility.Hidden;
        _mainWindow.KirishViewBox.Visibility = Visibility.Visible;
    }
}