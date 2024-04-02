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
        private EmployeeService _employeeService { get; set; }

        private List<UserDTO> users = new List<UserDTO>();
        private List<EmployeeDTO> _employeeDtos = new List<EmployeeDTO>();

        private IUserService _userService { get; set; }
         EmployeeDTO selectuserDto { get; set; }
       // private IUserService _userService { get; set; }
        public async void SetMainWinndow(MainWindow mainWindow, SettingsPage settingsPage, EmployeeService employeeService)
        {
            this.mainWindow = mainWindow;
            _settingsPage = settingsPage;
            _employeeService = employeeService;
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
           // _settingsPage.CreatePage.SetMainWinndow(mainWindow,_employeeService);
           
            _settingsPage.Create_doc.Visibility = Visibility.Visible;
            
        }

        private void Edit_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (selectuserDto != null)
            {
                _settingsPage.Edit_doc.Visibility = Visibility.Collapsed;
                _settingsPage.Create_doc.Visibility = Visibility.Visible;
                
                
                 XodimCreatePage xodimCreatePage=new XodimCreatePage();
               //  xodimCreatePage.SetMainWinndow(this,_employeeService);
                // xodimCreatePage.SetEmployeeData(selectuserDto.Id);
                 xodimCreatePage.Visibility=Visibility.Visible;
                  

            }
            else
            {
                MessageBox.Show("Select any  employee!");
            }
        }


        public async void SetEmployeeData(long Id)
        {
            var employee = await _employeeService.GetEmployeeById(Id);
            if (Id > 0)
            {
                /*
                 firstnametxt=employee.FirstName;
                 lasnametxt=employee.LastName;
                 fathernametxt=employee.FatherName;
                 borndatetxt=employee.BornDate;
                 
                 */
            }
        }
    }
}
