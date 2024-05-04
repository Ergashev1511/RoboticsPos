using System.Windows;
using System.Windows.Controls;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class CompanyForma : Window
{
    public CompanyForma()
    {
        InitializeComponent();
    }
    
    
    private CompanyControl _companyControl { get; set; }
    private ICompanyService _companyService { get; set; }
    private IProductService _productService { get; set; }
    
    private List<ProductForSelect> products { get; set; } = new List<ProductForSelect>();
    private CompanyDTO _company { get; set; }

    private long selectedcompanyId = 0;
    public void SetVariablies(CompanyControl companyControl, ICompanyService companyService,IProductService productService)
    {
        _companyControl = companyControl;
        _companyService = companyService;
        _productService = productService;
    }


    public async void GetAllProduct()
    {
        var products = await _productService.GetAllForSelect();
        if (products.Any())
        {
            prouduct_datagrid.ItemsSource = products;
            prouduct_datagrid.Items.Refresh();
        }
    }


    public async void FillForm(long Id,bool IsView=false)
    {
        List<ProductForSelect> products = new List<ProductForSelect>();
        _company = await _companyService.GetByIdCompany(Id);
        products = _company.Products;
        selectedcompanyId = Id;
        if (_company != null)
        {
            DisableForm(IsView);
            name_txt.Text = _company.Name;
            phoneNumber_txt.Text = _company.PhoneNumber;
            
            prouduct_datagrid.ItemsSource = products;
            prouduct_datagrid.Items.Refresh();
        }
    }

    private async void Reshresh_btn_OnClick(object sender, RoutedEventArgs e)
    {
        if (selectedcompanyId > 0)
        {
            products = await _productService.GetProductsByIdsForDiscount(_company.Products.Select(s => s.Id).ToList());
        }
        else
        {
            products = await _productService.GetAllForSelect();
        }

        if (products != null && products.Any())
        {
            prouduct_datagrid.ItemsSource = products;
            prouduct_datagrid .Items.Refresh();
        }
    }


    private  void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
    {
       ClearForm();
       this.Close();
    }

    private async void Save_btn_OnClick(object sender, RoutedEventArgs e)
    {
        CompanyDTO companyDto = new CompanyDTO();


        companyDto.Name = name_txt.Text;
        companyDto.PhoneNumber = phoneNumber_txt.Text;

        if (products.Any(b => b.Selected))
        {
            companyDto.Products = new List<ProductForSelect>();
            companyDto.Products.AddRange(products.Where(s=>s.Selected));
        }
        else
        {
            MessageBox.Show("Products doesn't select for discount creating!");
            return;
        }


        if (selectedcompanyId > 0)
        {
            await _companyService.UpdateCompany(selectedcompanyId, companyDto);
        }
        else
        {
            await _companyService.CreateCompant(companyDto);
        }
        _companyControl.GetAllCompany();
        ClearForm();
        this.Close();
    }
     

      public void ClearForm()
      {
          name_txt.Text = string.Empty;
          phoneNumber_txt.Text = string.Empty;
          prouduct_datagrid.ItemsSource = null;
      }

      private void CheckBox_OnChecked(object sender, RoutedEventArgs e)
      {
          CheckBox checkBox = sender as CheckBox;
          if (checkBox?.DataContext is ProductForSelect productForDiscountDto)
          {
              productForDiscountDto.Selected = true;
          }
      }

      private void CheckBox_OnUnchecked(object sender, RoutedEventArgs e)
      {
          CheckBox checkBox = sender as CheckBox;
          if (checkBox?.DataContext is ProductForSelect productForDiscountDto)
          {
              productForDiscountDto.Selected = false;
          }
      }

      public void DisableForm(bool isreadonly)
      {
          name_txt.IsReadOnly = isreadonly;
          phoneNumber_txt.IsReadOnly = isreadonly;
          prouduct_datagrid.IsEnabled = !isreadonly;
          // DataGrid ni tekshirib ko'rish kerak
      }
}