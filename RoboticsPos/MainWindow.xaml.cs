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
        private IProductService _productService;
        private XodimCrudPage _xodimCrudPage { get; set; }
        private XodimCreatePage _xodimCreatePage { get; set; }
        
        private ProductCreatePage _productCreatePage { get; set; }
        private ProductListPage _productListPage{get; set; }
       
     
        public MainWindow(IUserService userService,IRepository<User> userRepository,AppDbContext context,
            EmployeeService employeeService,XodimCrudPage xodimCrudPage,XodimCreatePage xodimCreatePage,
            ProductCreatePage productCreatePage,ProductListPage productListPage,IProductService productService)
        {
            this.userService = userService;
            this.userRepository = userRepository;
            this._context = context;
            this._employeeService = employeeService;
            _productService = productService;
            _xodimCrudPage = xodimCrudPage;
            _xodimCreatePage = xodimCreatePage;
            _productCreatePage = productCreatePage;
            _productListPage = productListPage;
            
            
            InitializeComponent();
            
            
            Kirishpage.SetMainWinndow(this);
            pinkodpage.SetMainWinndow(this,userService);
            menyupage.SetMainWinndow(this);
            kassapage.SetMainWinndow(this,productService);
            shaxsiy_malPage.SetMainWindow(this);
            loginpage.SetVariablies(userService,this);
            store_control.SetMainWindow(this,_productCreatePage,_productListPage,_productService);
            
            
            
            
            settingspage.SetMainWinndow(this,_employeeService,_xodimCrudPage,xodimCreatePage);
            _xodimCreatePage.SetMainWinndow(this,_employeeService,_xodimCrudPage,settingspage);
            _xodimCrudPage.SetMainWinndow(this,settingspage,_employeeService,_xodimCreatePage);
            
            
            
            store_control.SetMainWindow(this,productCreatePage,productListPage,productService);
            productCreatePage.SetVariablies(this,store_control,productListPage,productService);
            productListPage.SetVariablies(this,store_control,productCreatePage,productService);
        }
    }
}