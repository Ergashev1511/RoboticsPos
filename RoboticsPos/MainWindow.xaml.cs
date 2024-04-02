using System.Text;
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
using RoboticsPos.Data.Repositories;
using RoboticsPos.Services;
using RoboticsPos.UI;

namespace RoboticsPos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserService userService;
        private IRepository<User> userRepository;
        private AppDbContext _context;
        private EmployeeService _employeeService;

        private XodimCrudPage _xodimCrudPage { get; set; }
     //  private EmployeeRepository _repository;
        public MainWindow(IUserService userService,IRepository<User> userRepository,AppDbContext context,EmployeeService employeeService,XodimCrudPage xodimCrudPage )
        {
            this.userService = userService;
            this.userRepository = userRepository;
            this._context = context;
            this._employeeService = employeeService;
            _xodimCrudPage = xodimCrudPage;
            
            InitializeComponent();
            
            
            Kirishpage.SetMainWinndow(this);
            
            pinkodpage.SetMainWinndow(this,userService);
            menyupage.SetMainWinndow(this);
            kassapage.SetMainWinndow(this);
            shaxsiy_malPage.SetMainWindow(this);
            settingspage.SetMainWinndow(this,_employeeService,_xodimCrudPage);
            
            loginpage.SetVariablies(userService,this);
        }
    }
}