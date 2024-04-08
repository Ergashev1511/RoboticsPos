using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RoboticsPos.Data.Models;
using RoboticsPos.Services;

namespace RoboticsPos.UI
{
    /// <summary>
    /// Interaction logic for PinKodPage.xaml
    /// </summary>
    public partial class PinKodPage : UserControl
    {

        MainWindow mainWindow { get; set; }
        private  IUserService userService { get; set; }

        public void SetMainWinndow(MainWindow mainWindow, IUserService userService)
        {
            this.mainWindow = mainWindow;
            this.userService = userService;
        }


        public PinKodPage()
        {
            InitializeComponent();
        }
        

        private void Button_number(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            passwordbox.Password = passwordbox.Password + button.Content;
        }

        private async void Passwordbox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {

            if (passwordbox.Password.Length == 4)
            {
                try
                {
                    if (await userService.LoginByPini(passwordbox.Password))
                    {
                        mainWindow.PinKodViewBox.Visibility = Visibility.Collapsed;
                        mainWindow.KassaViewBox.Visibility = Visibility.Visible;
                        passwordbox.Password = "";
                    }
                    else
                    {
                        MessageBox.Show("Pin kod xato!");
                        passwordbox.Password = "";
                    }
                }
                catch (NullReferenceException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
           
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            passwordbox.Password = passwordbox.Password.Substring(0, passwordbox.Password.Length - 1);
        }

        private void Button_Cansel(object sender, RoutedEventArgs e)
        {
            mainWindow.PinKodViewBox.Visibility = Visibility.Hidden;
            mainWindow.KirishViewBox.Visibility = Visibility.Visible;
        }
    }
}
