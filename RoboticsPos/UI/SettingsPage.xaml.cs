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
        MainWindow _mainWindow { get; set; }
        private EmployeeService _employeeService { get; set; }
        private XodimCrudPage _xodimCrudPage { get; set; }
        private XodimCreatePage _xodimCreatePage { get; set; }
        private CheckCreatePage _checkCreate { get; set; }
        private CheckForm _checkForm { get; set; }
        private ClientForm _clientForm { get; set; }
        private ClientCreatePagae _clientCreatePagae { get; set; }
        private ICheckPrinterService _service { get; set; }
        private IClientService _clientService { get; set; }
        public void SetMainWinndow(MainWindow mainWindow, EmployeeService employeeService, XodimCrudPage xodimCrudPage,XodimCreatePage xodimCreatePage,
            CheckCreatePage checkCreatePage,CheckForm checkForm,ICheckPrinterService service,ClientForm clientForm,ClientCreatePagae clientCreatePagae,
            IClientService clientService
        )
        {
            _mainWindow = mainWindow;
            _employeeService = employeeService;
            _service = service;
            _xodimCrudPage = xodimCrudPage;
            _xodimCreatePage = xodimCreatePage;
            _checkCreate = checkCreatePage;
            _checkForm = checkForm;
            _clientForm = clientForm;
            _clientCreatePagae = clientCreatePagae;
            _clientService = clientService;
            employeeControl.SetMainWinndow(mainWindow,this,  _employeeService,xodimCreatePage);
            createPage.SetMainWinndow(mainWindow,employeeService,_xodimCrudPage,this);
            checks_createPage.SetValues(mainWindow,this,checkForm,_service);
            chekspage.SetValues(mainWindow,this,checkCreatePage,_service);
            clientpage.SetValues(mainWindow,this,clientCreatePagae,clientService);
            client_createPage.SetValues(mainWindow,this,clientForm,clientService);
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
            _mainWindow.SettingsViewBox.Visibility = Visibility.Hidden;
            _mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
        }



        private void Xodim_btn_OnClick(object sender, RoutedEventArgs e)
        {
            Employee_doc.Visibility = Visibility.Visible;
            employeeControl.SetMainWinndow(_mainWindow,this,  _employeeService,_xodimCreatePage);
            Create_doc.Visibility = Visibility.Hidden;
            check_doc.Visibility = Visibility.Collapsed;
            check_creat.Visibility = Visibility.Collapsed;
            client_doc.Visibility = Visibility.Hidden;
            client_creat.Visibility = Visibility.Hidden;
        }

        private void Back_btn_OnClick(object sender, RoutedEventArgs e)
        {
           _mainWindow.SettingsViewBox.Visibility = Visibility.Hidden;
            _mainWindow.MenyuViewBox.Visibility = Visibility.Visible;
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

        private void Discount_btn_OnClick(object sender, RoutedEventArgs e)
        {
            Employee_doc.Visibility = Visibility.Collapsed;
            chekspage.SetValues(_mainWindow,this,_checkCreate,_service);
            check_doc.Visibility = Visibility.Visible;
            client_doc.Visibility = Visibility.Hidden;
            client_creat.Visibility = Visibility.Hidden;
            check_creat.Visibility = Visibility.Collapsed;
            Create_doc.Visibility = Visibility.Hidden;
        }

        private void Client_btn_OnClick(object sender, RoutedEventArgs e)
        {
            client_doc.Visibility = Visibility.Visible;
            client_creat.Visibility = Visibility.Hidden;
            clientpage.SetValues(_mainWindow,this,_clientCreatePagae,_clientService);
            
            Employee_doc.Visibility = Visibility.Hidden;
            Create_doc.Visibility = Visibility.Hidden;
            check_doc.Visibility = Visibility.Collapsed;
            check_creat.Visibility = Visibility.Collapsed;
        }
    }
}
