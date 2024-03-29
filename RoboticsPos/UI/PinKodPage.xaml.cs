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
        private readonly UserService userService;

        public void SetMainWinndow(MainWindow mainWindow)
        { this.mainWindow = mainWindow; }


        public PinKodPage()
        {
            InitializeComponent();
          
            
        }

        public PinKodPage(UserService userService)
        {
             this.userService = userService;
        }

        private void Button_number(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            passwordbox.Password = passwordbox.Password + button.Content;
        }

        private void Passwordbox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {

            if (passwordbox.Password.Length == 4)
            {
                if(passwordbox.Password=="1511")
                {
                    mainWindow.KassaViewBox.Visibility = Visibility.Visible;
                    mainWindow.PinKodViewBox.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Parol xato!");
                    passwordbox.Password = "";
                }
            }
           /* var data = userService.LoginByPini(passwordbox.Password);
            if (passwordbox.Password.Length == 4)
            {
                if (data.Result == true)
                {
                    mainWindow.KassaViewBox.Visibility = Visibility.Visible;
                    mainWindow.PinKodViewBox.Visibility = Visibility.Hidden;
                }
                else
                {

                    MessageBox.Show("Incorrcet password");
                }
            }*/
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
