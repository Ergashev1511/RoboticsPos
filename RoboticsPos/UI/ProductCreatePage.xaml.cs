using System.Windows;
using System.Windows.Controls;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Enum;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class ProductCreatePage : UserControl
{
    private StoreControl _storeControl { get; set; }
    private MainWindow _mainWindow { get; set; }
    private ProductListPage _productListPage { get; set; }
    private IProductService _productService { get; set; }
    
    private ICategoryService _categoryService { get; set; }
    private List<Select> category { get; set; } = new();

    private IDiscountService _discountService { get; set; }
    private List<Select> dicount { get; set; } = new();

    private ICompanyService _companyService { get; set; }
    private List<Select> company { get; set; } = new();
    
    private long ProductId = 0;
    public void SetVariablies(MainWindow mainWindow, StoreControl storeControl,ProductListPage productListPage,
        IProductService productService,ICategoryService categoryService,IDiscountService discountService,ICompanyService companyService)
    {
        _mainWindow = mainWindow;
        _storeControl = storeControl;
        _productListPage = productListPage;
        _productService = productService;
        _categoryService = categoryService;
        _discountService = discountService;
        _companyService = companyService;
        _productListPage.SetVariablies(mainWindow,storeControl,this,productService,categoryService,discountService,companyService);
        GetCategroy();
        GetCompany();
        GetDiscount();
    }
    public ProductCreatePage()
    {
        InitializeComponent();
    }


    public async void GetCategroy()
    {
        category = await _categoryService.GetCategoriesForSelect();
        if (category != null && category.Any())
        {
            category_combo.ItemsSource = category.Select(a=>a.Name);
            category_combo.Items.Refresh();
        }
    }

    public async void GetCompany()
    {
         company = await _companyService.GetSelectCompany();
        if (company != null && company.Any())
        {
            company_combo.ItemsSource = company.Select(a=>a.Name);
            company_combo.Items.Refresh();
        }
    }

    public async void GetDiscount()
    {
        dicount = await _discountService.GetSelectDiscount();
        if (dicount != null && dicount.Any())
        {
            discount_combo.ItemsSource = dicount.Select(a=>a.Name);
            discount_combo.Items.Refresh();
        }
    }

    public async void SetProductData(long Id, bool IsView = false)
    {
        if (Id > 0)
        {
            DisableForm(IsView);
            ProductId = Id;
            var product = await _productService.GetProductById(Id);
            if (product != null)
            {
                nametxt.Text = product.Name;
                barcodetxt.Text = product.BarCode;
                amounttxt.Text = product.Amount.ToString();
                amountinpacakgetxt.Text = product.AmountInPackage.ToString();
                actualpricetxt.Text = product.ActualPrice.ToString();
                pricetxt.Text = product.Price.ToString();
                priceofpiesetxt.Text = product.PriceOfPiece.ToString();
                
                // Discoutn  company va category qo'shish kerak 

            }
        }
    }
    
    
    
    private async void Saqlash_btn_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            ProductDTO productDto = new ProductDTO()
            {
               
                Name = nametxt.Text,
                BarCode = barcodetxt.Text,
                AmountInPackage = int.Parse(amountinpacakgetxt.Text),
                Amount = int.Parse(amounttxt.Text),
                ActualPrice = int.Parse(actualpricetxt.Text),
                Price = int.Parse(pricetxt.Text),
                PriceOfPiece = int.Parse(priceofpiesetxt.Text),
                
                DiscountId = dicount.Any()? dicount[discount_combo.SelectedIndex].Id: null, // null qilmay otasiga o'zini berib ketish kerak
                CompanyId = company.Any()? company[company_combo.SelectedIndex].Id : null,
                CategoryId = category.Any() ? category[category_combo.SelectedIndex].Id : null
            };
            if (ProductId == 0)
            {
                await _productService.CreateProduct(productDto);
            }
            else
            {
                await _productService.UpdateProduct(ProductId,productDto);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        ClearForm();
       await _productListPage.GetAllProducts();
       _storeControl.ProCreate_doc.Visibility = Visibility.Collapsed;
       _storeControl.product_doc.Visibility = Visibility.Visible;
    }

    private void Cansel_btn_OnClick(object sender, RoutedEventArgs e)
    {
        ClearForm();
        _storeControl.ProCreate_doc.Visibility = Visibility.Collapsed;
        _storeControl.product_doc.Visibility = Visibility.Visible;
    }


    public void ClearForm()
    {
        nametxt.Text = "";
        barcodetxt.Text = "";
        amounttxt.Text = "";
        amountinpacakgetxt.Text = "";
        actualpricetxt.Text = "";
        pricetxt.Text = "";
        priceofpiesetxt.Text = "";
        discount_combo.SelectedItem = "";
    }

    public void DisableForm(bool isReadOnly)
    {
        nametxt.IsEnabled = !isReadOnly;
        barcodetxt.IsEnabled = !isReadOnly;
        amounttxt.IsEnabled = !isReadOnly;
        amountinpacakgetxt.IsEnabled = !isReadOnly;
        pricetxt.IsEnabled = !isReadOnly;
        actualpricetxt.IsEnabled = !isReadOnly;
        priceofpiesetxt.IsEnabled = !isReadOnly;
        discount_combo.IsReadOnly = !isReadOnly;
        category_combo.IsReadOnly = !isReadOnly;
        company_combo.IsReadOnly = !isReadOnly;
    }
}