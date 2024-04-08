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
using RoboticsPos.Data.Enum;
using RoboticsPos.Services;

namespace RoboticsPos.UI
{
    /// <summary>
    /// Interaction logic for XodimCreatePage.xaml
    /// </summary>
    public partial class XodimCreatePage : UserControl
    {
        MainWindow mainWindow { get; set; }
        
        private List<UserDTO> users = new List<UserDTO>();
        
        private List<EmployeeDTO> _employeeDtos = new List<EmployeeDTO>();
        public EmployeeService Service { get; set; }
        long employeeId { get; set; } = 0;

        private XodimCrudPage _xodimCrudPage { get; set; }
        private SettingsPage _settingsPage { get; set; }

       
        public void SetMainWinndow(MainWindow mainWindow, EmployeeService employeeService,XodimCrudPage xodimCrudPage,SettingsPage settingsPage)
        {
            this.mainWindow = mainWindow;
            Service = employeeService;
            _xodimCrudPage = xodimCrudPage;
            _settingsPage = settingsPage;
        }

        public XodimCreatePage()
        {
            InitializeComponent();
        }


        public async void SetEmployeeData(long Id,bool isView=false)
        {
            
            if (Id > 0)
            {
                DisableForm(isView);
                employeeId = Id;
                var employee = await Service.GetEmployeeById(Id);
                if (employee != null)
                {
                    // employee details
                    jobtitletxt.Text = employee.JobTitle;
                    enrollnumbertxt.Text = employee.EnrollNumber.ToString();
                    employeeroletxt.SelectedItem=employee.EmployeeRole == Data.Enum.EmployeeRole.Cashier ? employeeroletxt.Items[0] : employeeroletxt.Items[1]; 
                    
                    usernametxt.Text = employee.Username;
                    passwordtxt.Text = employee.password;
                    pinkodtxt.Text = employee.PIN;
                    
                    firstnametxt.Text = employee.Firstname;
                    lastnametxt.Text = employee.Lastname;
                    fathernametxt.Text = employee.Fathername;
                    borndatetxt.SelectedDate = employee.BornDate;
                    hiredatetxt.SelectedDate = employee.HireDate;
                    addresstxt.Text = employee.Address;
                    phonenumbertxt.Text = employee.PhoneNumber;
                }
            }
        }
        
        
        
        
        private async void Saqlash_btn_OnClick(object sender, RoutedEventArgs e)
        {
           
                try
                {
                    EmployeeDTO newEmployee = new EmployeeDTO()
                    {
                        // employee details
                        JobTitle = jobtitletxt.Text,
                        EnrollNumber = long.Parse(enrollnumbertxt.Text),
                        HireDate = hiredatetxt.SelectedDate.Value,
                        EmployeeRole = employeeroletxt.SelectedValue == employeeroletxt.Items[1] ? Data.Enum.EmployeeRole.Manager : Data.Enum.EmployeeRole.Cashier,
                        // user details
                        
                        Username = employeeroletxt.SelectedItem==employeeroletxt.Items[1] ? usernametxt.Text : "",  
                        password = employeeroletxt.SelectedItem==employeeroletxt.Items[1] ? passwordtxt.Text: "",
                        PIN = pinkodtxt.Text,

                        // person details
                        Firstname = firstnametxt.Text,
                        Lastname = lastnametxt.Text,
                        Fathername = fathernametxt.Text,
                        BornDate = borndatetxt.SelectedDate.Value,
                        Address = addresstxt.Text,
                        PhoneNumber = phonenumbertxt.Text,
                        Fullname = $"{lastnametxt.Text} {firstnametxt.Text} {fathernametxt.Text}" 
                    };
                    if(employeeId == 0)
                    {
                        ClearForm();
                        await Service.CreateEmployee(newEmployee);
                    }
                    else
                    {
                        await Service.UpdateEmployee(employeeId, newEmployee);
                    }
                    ClearForm();
                    await _xodimCrudPage.GetAllUsers();
                    _settingsPage.Employee_doc.Visibility = Visibility.Visible;
                    _settingsPage.Create_doc.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
        
        public void ClearForm()
        {
            jobtitletxt.Text = "";
           enrollnumbertxt.Text = "";
            hiredatetxt.SelectedDate = DateTime.Today;

            usernametxt.Text = "";
            passwordtxt.Text = "";
            pinkodtxt.Text = "";

            firstnametxt.Text = "";
            lastnametxt.Text = "";
            fathernametxt.Text = "";
            borndatetxt.SelectedDate = DateTime.Today;
         
            phonenumbertxt.Text = "";
            borndatetxt.Text = "";
            addresstxt.Text = "";
            hiredatetxt.Text = "";

        }
        public void DisableForm(bool isReadOnly)
        {
            jobtitletxt.IsReadOnly = isReadOnly;
            enrollnumbertxt.IsReadOnly = isReadOnly;
            employeeroletxt.IsEnabled = !isReadOnly;
            
           firstnametxt.IsEnabled = !isReadOnly; 
           lastnametxt.IsEnabled = !isReadOnly;
           fathernametxt.IsEnabled = !isReadOnly;
           addresstxt.IsEnabled = !isReadOnly;
           phonenumbertxt.IsEnabled =!isReadOnly;
           
            hiredatetxt.IsEnabled = !isReadOnly;
            borndatetxt.IsEnabled = !isReadOnly;
            usernametxt.IsReadOnly = isReadOnly;
            passwordtxt.IsEnabled = !isReadOnly;
            pinkodtxt.IsEnabled = !isReadOnly;
            saqlash_btn.Visibility = isReadOnly ? Visibility.Hidden : Visibility.Visible;

        }


        private void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
        {
            ClearForm();
            _settingsPage.Create_doc.Visibility = Visibility.Collapsed;
            _settingsPage.Employee_doc.Visibility = Visibility.Visible;
        }
      
    }
}
