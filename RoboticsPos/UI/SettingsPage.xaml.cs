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
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : UserControl
    {
        MainWindow mainWindow { get; set; }


        public void SetMainWinndow(MainWindow mainWindow)
        { this.mainWindow = mainWindow; }

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Employee_doc.Visibility = Visibility.Visible;
        }
        

        private void Button_Back1(object sender, RoutedEventArgs e)
        {
            
            // Buttton ishlamadi 
            // mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
            // mainWindow.SettingsViewBox.Visibility = Visibility.Hidden;
        }
    }
}
