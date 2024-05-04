using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class CompanyControl : UserControl
{
    private ICompanyService _companyService { get; set; }
    private SettingsPage _settings { get; set; }
    private IProductService _productService { get; set; }
    private CompanyDTO _companyDto { get; set; }

    public void SetVariablies(SettingsPage settingsPage,ICompanyService companyService,IProductService productService)
    {
        _settings = settingsPage;
        _companyService = companyService;
        _productService = productService;
        GetAllCompany();
    }
    public CompanyControl()
    {
        InitializeComponent();
    }

    public async void GetAllCompany()
    {
        var companys = await _companyService.GetAllCompany();
        if (companys.Any())
        {
            company_datagrid.ItemsSource = companys;
            company_datagrid.Items.Refresh();
        }
    }

    private void products_datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        CompanyForma companyForma = new CompanyForma();  
        companyForma.SetVariablies(this,_companyService,_productService);
        companyForma.FillForm(_companyDto.Id,true);
        if (_companyDto != null)
        {
            _companyDto=company_datagrid.SelectedItem as CompanyDTO;
            
            companyForma.save_btn.Visibility = Visibility.Collapsed;
            companyForma.cansel_btn.Visibility = Visibility.Visible;
            
            companyForma.ShowDialog();
          

           
        }
    }

   

    private void Edit_btn_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedcompany = company_datagrid.SelectedItem as CompanyDTO;
        if (selectedcompany != null)
        {
            CompanyForma companyForma = new CompanyForma();
            companyForma.SetVariablies(this,_companyService,_productService);
            companyForma.FillForm(selectedcompany.Id);
            companyForma.ShowDialog();
        }
        
    } 

    private async void Delete_btn_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedcompany = company_datagrid.SelectedItem as CompanyDTO;

        if (selectedcompany != null)
        {
            await _companyService.DeleteCompany(selectedcompany.Id);
            GetAllCompany();
        }
    }

    private void Create_btn_OnClick(object sender, RoutedEventArgs e)
    {
        CompanyForma companyForma = new CompanyForma();
        companyForma.SetVariablies(this,_companyService,_productService);
        companyForma.GetAllProduct();
        companyForma.ShowDialog();
    }

    private void Company_datagrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _companyDto=company_datagrid.SelectedItem as CompanyDTO;
        
    }
}