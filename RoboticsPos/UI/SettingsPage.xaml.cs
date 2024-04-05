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
using RoboticsPos.Services;

namespace RoboticsPos.UI
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : UserControl
    {
        MainWindow mainWindow { get; set; }
        private EmployeeService _employeeService { get; set; }
        private XodimCrudPage _xodimCrudPage { get; set; }
        private XodimCreatePage _xodimCreatePage { get; set; }
        public void SetMainWinndow(MainWindow mainWindow, EmployeeService employeeService, XodimCrudPage xodimCrudPage,XodimCreatePage xodimCreatePage
        )
        {
            this.mainWindow = mainWindow;
            _employeeService = employeeService;
            _xodimCrudPage = xodimCrudPage;
            _xodimCreatePage = xodimCreatePage;  
            employeeControl.SetMainWinndow(mainWindow,this,  _employeeService,xodimCreatePage);
            createPage.SetMainWinndow(mainWindow,employeeService,_xodimCrudPage,this);

        }

        public SettingsPage()
        {
            InitializeComponent();
        }




        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Employee_doc.Visibility = Visibility.Visible;
        }
        


        private void ButtonBase_OnClick1(object sender, RoutedEventArgs e)
        {
            mainWindow.SettingsViewBox.Visibility = Visibility.Hidden;
            mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
        }



        private void Xodim_btn_OnClick(object sender, RoutedEventArgs e)
        {
            Employee_doc.Visibility = Visibility.Visible;
            employeeControl.SetMainWinndow(mainWindow,this,  _employeeService,_xodimCreatePage);
            Create_doc.Visibility = Visibility.Hidden;
        }

        private void Back_btn_OnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.SettingsViewBox.Visibility = Visibility.Hidden;
            mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
        }

        private void Set_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (grid_Settings.ColumnDefinitions.First().MaxWidth == 200)
            {
                grid_Settings.ColumnDefinitions.First().MaxWidth = 50;
            }
            else if(grid_Settings.ColumnDefinitions.First().MaxWidth==50)
            {
                grid_Settings.ColumnDefinitions.First().MaxWidth = 200;
            }
        }
    }
}
