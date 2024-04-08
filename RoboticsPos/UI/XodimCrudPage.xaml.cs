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
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI
{
    /// <summary>
    /// Interaction logic for XodimCrudPage.xaml
    /// </summary>
    public partial class XodimCrudPage : UserControl
    {
        MainWindow mainWindow { get; set; }
        private SettingsPage _settingsPage { get; set; }
        private XodimCreatePage _xodimCreatePage { get; set; }
        private EmployeeService _employeeService { get; set; }

        private List<UserDTO> users = new List<UserDTO>();
        private List<EmployeeDTO> _employeeDtos = new List<EmployeeDTO>();

        private IUserService _userService { get; set; }
         EmployeeDTO selectuserDto { get; set; }
     
        public async void SetMainWinndow(MainWindow mainWindow, SettingsPage settingsPage, EmployeeService employeeService,XodimCreatePage xodimCreatePage)
        {
            this.mainWindow = mainWindow;
            _settingsPage = settingsPage;
            _employeeService = employeeService;
            _xodimCreatePage = xodimCreatePage;
            await GetAllUsers();
        }


        public XodimCrudPage()
        {
            InitializeComponent();
        }

        private void users_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectuserDto=users_datagrid.SelectedItem as EmployeeDTO;
        }
        
        
        public async Task GetAllUsers()
        {
            _employeeDtos = await _employeeService.GetAllEmployee();

            if (_employeeDtos != null && _employeeDtos.Any())
            {
                users_datagrid.ItemsSource = _employeeDtos;
            }
        }
        
        private void Button_Create(object sender, RoutedEventArgs e)
        {
            _settingsPage.Employee_doc.Visibility = Visibility.Collapsed;
            _settingsPage.Create_doc.Visibility = Visibility.Visible;
            
        }

        private void Edit_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (selectuserDto != null)
            {
                _settingsPage.createPage.SetMainWinndow(mainWindow,_employeeService,this,_settingsPage);
                _settingsPage.createPage.SetEmployeeData(selectuserDto.Id);
                 
                _settingsPage.Create_doc.Visibility = Visibility.Visible;
                _settingsPage.Employee_doc.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Select any  employee!");
            }
        }

        private async void Delete_btn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if(selectuserDto != null)
                {
                    
                    var result = MessageBox.Show("Do you want delete this employee", "WARNING", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        await _employeeService.DeleteEmployee(selectuserDto.Id);
                        await GetAllUsers();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Users_datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (selectuserDto != null)
            {
                
                _settingsPage.createPage.SetMainWinndow(mainWindow,_employeeService,this,_settingsPage);
                _settingsPage.createPage.SetEmployeeData(selectuserDto.Id,true);
                
                _settingsPage.Create_doc.Visibility = Visibility.Visible;
                _settingsPage.Employee_doc.Visibility = Visibility.Hidden;
                
                _settingsPage.createPage.saqlash_btn.Visibility = Visibility.Collapsed;
                _settingsPage.createPage.cansel_btn.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Select any employee!");
            }

        }
    }
}
