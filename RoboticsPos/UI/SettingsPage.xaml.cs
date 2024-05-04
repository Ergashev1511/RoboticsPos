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
        private IDiscountService _discountService { get; set; }
        private IProductService _productService { get; set; }
        private ICompanyService _companyService { get; set; }
        private ICategoryService _categoryService { get; set; }
        public void SetMainWinndow(MainWindow mainWindow, EmployeeService employeeService, XodimCrudPage xodimCrudPage,XodimCreatePage xodimCreatePage,
            CheckCreatePage checkCreatePage,CheckForm checkForm,ICheckPrinterService service,ClientForm clientForm,ClientCreatePagae clientCreatePagae,
            IClientService clientService,IDiscountService discountService,IProductService productService,ICompanyService companyService,ICategoryService categoryService
            
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
            _discountService = discountService;
            _productService = productService;
            _companyService = companyService;
            _categoryService = categoryService;
            
            employeeControl.SetMainWinndow(mainWindow,this,  _employeeService,xodimCreatePage);
            createPage.SetMainWinndow(mainWindow,employeeService,_xodimCrudPage,this);
            checks_createPage.SetValues(mainWindow,this,checkForm,_service);
            chekspage.SetValues(mainWindow,this,checkCreatePage,_service);
            clientpage.SetValues(mainWindow,this,clientCreatePagae,clientService);
            client_createPage.SetValues(mainWindow,this,clientForm,clientService);
            discountControl.SetVariablies(this,discountService, productService);
            companycontrol.SetVariablies(this,companyService,productService);
            categorycontrol.SetVariablies(this,categoryService,productService);
        }

        public SettingsPage()
        {
            InitializeComponent();
        }



        private void Xodim_btn_OnClick(object sender, RoutedEventArgs e)
        {
            Employee_doc.Visibility = Visibility.Visible;
            employeeControl.SetMainWinndow(_mainWindow,this,  _employeeService,_xodimCreatePage);
            Create_doc.Visibility = Visibility.Hidden;
            
            check_doc.Visibility = Visibility.Collapsed;
            check_creat.Visibility = Visibility.Collapsed;
            
            client_doc.Visibility = Visibility.Collapsed;
            client_creat.Visibility = Visibility.Collapsed;
            
            discount_oc.Visibility = Visibility.Collapsed;
            company_doc.Visibility = Visibility.Collapsed;
            category_doc.Visibility = Visibility.Collapsed;
           
        }

        private void Back_btn_OnClick(object sender, RoutedEventArgs e)
        {
           _mainWindow.SettingsViewBox.Visibility = Visibility.Collapsed;
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

        private void Check_btn_OnClick(object sender, RoutedEventArgs e)
        {
            check_doc.Visibility = Visibility.Visible; 
            check_creat.Visibility = Visibility.Collapsed;
            
            chekspage.SetValues(_mainWindow,this,_checkCreate,_service);
            
            client_doc.Visibility = Visibility.Collapsed;
            client_creat.Visibility = Visibility.Collapsed;
           
            
            Employee_doc.Visibility = Visibility.Collapsed;
            Create_doc.Visibility = Visibility.Collapsed;
            
            discount_oc.Visibility = Visibility.Collapsed;
            company_doc.Visibility = Visibility.Collapsed;
            category_doc.Visibility = Visibility.Collapsed;
            
        }

        private void Client_btn_OnClick(object sender, RoutedEventArgs e)
        {
            client_doc.Visibility = Visibility.Visible;
            client_creat.Visibility = Visibility.Collapsed;
            clientpage.SetValues(_mainWindow,this,_clientCreatePagae,_clientService);
            
            Employee_doc.Visibility = Visibility.Collapsed;
            Create_doc.Visibility = Visibility.Collapsed;
            
            check_doc.Visibility = Visibility.Collapsed;
            check_creat.Visibility = Visibility.Collapsed;
            
            discount_oc.Visibility = Visibility.Collapsed;
            
            company_doc.Visibility = Visibility.Collapsed;
            category_doc.Visibility = Visibility.Collapsed;
          
        }


        private void Discount_btn_OnClick(object sender, RoutedEventArgs e)
        {
            discount_oc.Visibility = Visibility.Visible;
            discountControl.SetVariablies(this,_discountService,_productService);
            
            
            client_doc.Visibility = Visibility.Collapsed;
            client_creat.Visibility = Visibility.Collapsed;
            
            Employee_doc.Visibility = Visibility.Collapsed;
            Create_doc.Visibility = Visibility.Collapsed;
            
            check_doc.Visibility = Visibility.Collapsed;
            check_creat.Visibility = Visibility.Collapsed;
            
            company_doc.Visibility = Visibility.Collapsed;
            category_doc.Visibility = Visibility.Collapsed;
        }

        private void Company_btn_OnClick(object sender, RoutedEventArgs e)
        {
            company_doc.Visibility = Visibility.Visible;
            companycontrol.SetVariablies(this,_companyService,_productService);
            
            discount_oc.Visibility = Visibility.Collapsed;
            client_doc.Visibility = Visibility.Collapsed;
            client_creat.Visibility = Visibility.Collapsed;
            
            Employee_doc.Visibility = Visibility.Collapsed;
            Create_doc.Visibility = Visibility.Collapsed;
            
            check_doc.Visibility = Visibility.Collapsed;
            check_creat.Visibility = Visibility.Collapsed;
            category_doc.Visibility = Visibility.Collapsed;
        }

        private void Categroy_btn_OnClick(object sender, RoutedEventArgs e)
        {
            category_doc.Visibility = Visibility.Visible;
            categorycontrol.SetVariablies(this, _categoryService, _productService);
            company_doc.Visibility = Visibility.Collapsed;
            discount_oc.Visibility = Visibility.Collapsed;
            client_doc.Visibility = Visibility.Collapsed;
            client_creat.Visibility = Visibility.Collapsed;
            
            Employee_doc.Visibility = Visibility.Collapsed;
            Create_doc.Visibility = Visibility.Collapsed;
            
            check_doc.Visibility = Visibility.Collapsed;
            check_creat.Visibility = Visibility.Collapsed;
        }
    }
}
