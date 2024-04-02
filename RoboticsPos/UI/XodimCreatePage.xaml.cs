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

       
        public void SetMainWinndow(MainWindow mainWindow, EmployeeService employeeService,XodimCrudPage xodimCrudPage)
        {
            this.mainWindow = mainWindow;
            Service = employeeService;
            _xodimCrudPage = xodimCrudPage;
        }

        public XodimCreatePage()
        {
            InitializeComponent();
        }


        private async void Saqlash_btn_OnClick(object sender, RoutedEventArgs e)
        {
           
                try
                {
                    EmployeeDTO newEmployee = new EmployeeDTO()
                    {
                        // employee details
                        JobTitle = jobtitletxt.SelectedItem.ToString(),
                        EnrollNumber = long.Parse(enrollnumbertxt.Text),
                        HireDate = hiredatetxt.SelectedDate.Value,
                        //EmployeeRole = employeerole_combo.SelectedValue == "Cashier" ? Data.Enum.EmployeeRole.Cashier : Data.Enum.EmployeeRole.Manager,
                        EmployeeRole = jobtitletxt.SelectedValue == "Manager" ? Data.Enum.EmployeeRole.Manager : Data.Enum.EmployeeRole.Cashier,
                        // user details
                        
                        Username = jobtitletxt.SelectedItem.ToString()=="Manager" ? usernametxt.Text : "",
                        password = jobtitletxt.SelectedItem.ToString()=="Manager" ? passwordtxt.Text: "",
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
                        await Service.CreateEmployee(newEmployee);
                    }
                    else
                    {
                        await Service.UpdateEmployee(employeeId, newEmployee);
                    }
                    ClearForm();
                    await _xodimCrudPage.GetAllUsers();
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
           // emplo.SelectedItem = employeerole_combo.Items[0];

            usernametxt.Text = "";
            passwordtxt.Text = "";
            pinkodtxt.Text = "";

            firstnametxt.Text = "";
            lastnametxt.Text = "";
            fathernametxt.Text = "";
            borndatetxt.SelectedDate = DateTime.Today;
            addresstxt.Text = "";
            phonenumbertxt.Text = "";
            borndatetxt.Text = "";
            addresstxt.Text = "";
            hiredatetxt.Text = "";

        }

        private void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
