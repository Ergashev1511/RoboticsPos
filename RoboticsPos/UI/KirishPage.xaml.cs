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

namespace RoboticsPos.UI
{
    /// <summary>
    /// Interaction logic for KirishPage.xaml
    /// </summary>
    public partial class KirishPage : UserControl
    {

        MainWindow mainWindow {  get; set; }


        public void SetMainWinndow( MainWindow mainWindow )
        { this.mainWindow = mainWindow; }
        public KirishPage()
        {
            InitializeComponent();
        }

        private void ButtonBase_PinKod(object sender, RoutedEventArgs e)
        {
            mainWindow.PinKodViewBox.Visibility = Visibility.Visible;
            mainWindow.KirishViewBox.Visibility = Visibility.Collapsed;
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            mainWindow.LoginViewbox.Visibility = Visibility.Visible;
            mainWindow.KirishViewBox.Visibility = Visibility.Collapsed;
        }
    }
}
