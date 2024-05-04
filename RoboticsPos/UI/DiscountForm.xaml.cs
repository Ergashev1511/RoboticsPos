using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Enum;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class DiscountForm : Window
{
    private DiscountControl _discountControl { get; set; }
    private IDiscountService _discountService { get; set; }
    private IProductService _productService { get; set; }
    private List<ProductForSelect> products { get; set; } = new();
    private DiscountDTO _discount { get; set; }
    
        
    private long selectedDiscountId { get; set; } = 0;
    public DiscountForm()
    {
        InitializeComponent();
    }


    public void SetVariablies(DiscountControl discountControl, IDiscountService discountService,
        IProductService productService)
    {
        _discountControl = discountControl;
        _discountService = discountService;
        _productService = productService;
        GetAllProducts();
    }

    public async void GetAllProducts()
    {
        var products = await _productService.GetAllForSelect();
        if (products!=null && products.Any())
        {
            products_datagrid.ItemsSource = products;
            products_datagrid.Items.Refresh();
        }
    }


    public async void FillForm(long Id,bool IsView=false)
    {
        var products = new List<ProductForSelect>(); 
        _discount = await _discountService.GetByIdDiscount(Id);
        products = _discount.ProductDtos;
        selectedDiscountId = Id;                                                   
        if (_discount != null)
        {
            Disable(IsView);
         
            title_txt.Text = _discount.Title;
            describtion_txt.Text = _discount.Description;
            amount_txt.Text = _discount.Amount.ToString();
            amountType_combo.SelectedIndex = _discount.AmountType == DiscountType.Percent ? 0 : _discount.AmountType == DiscountType.Amount ? 1 : 2;
           
            start_date.SelectedDate = _discount.StartDate;
            end_date.SelectedDate = _discount.EndDate;
            active_chekbox.IsChecked = _discount.DiscountStatus == DiscountStatus.Active ? true : false;
            products_datagrid.ItemsSource = products;
            products_datagrid.Items.Refresh();
        }

    }
    private async void Refresh_btn_OnClick(object sender, RoutedEventArgs e)
    {
      
        if (selectedDiscountId > 0)
        {
            products = await _productService.GetProductsByIdsForDiscount(_discount.ProductDtos.Select(s=>s.Id).ToList());
        }
        else
        {
            products = await _productService.GetAllForSelect();
        }
        if (products != null && products.Any())
        {
            
            products_datagrid.ItemsSource = products;
            products_datagrid.Items.Refresh();
        }
    }

    private async void Save_btn_OnClick(object sender, RoutedEventArgs e)
    {
        DiscountDTO discountDTO = new DiscountDTO();

        discountDTO.Title = title_txt.Text;
        discountDTO.Description = describtion_txt.Text;
        discountDTO.Amount = decimal.Parse(amount_txt.Text);
        discountDTO.AmountType = amountType_combo.Text == "Percent" ?
            Data.Enum.DiscountType.Percent : amountType_combo.Text == "Amount" ?
                Data.Enum.DiscountType.Amount : Data.Enum.DiscountType.Count;
        discountDTO.StartDate = start_date.SelectedDate.Value;
        discountDTO.EndDate = end_date.SelectedDate.Value;
        discountDTO.DiscountStatus = active_chekbox.IsChecked == true ? Data.Enum.DiscountStatus.Active : Data.Enum.DiscountStatus.Inactive;

        if (products.Any(a => a.Selected))
        {
            discountDTO.ProductDtos = new List<ProductForSelect>();
            discountDTO.ProductDtos.AddRange(products.Where(s=>s.Selected));
        }
        else
        {
            MessageBox.Show("Products doesn't select for discount creating!");
            return;
        }
            
        if(selectedDiscountId > 0)
        {
            await _discountService.UpdateDiscount(selectedDiscountId, discountDTO);
        }
        else
        {
            await _discountService.CreateDiscount(discountDTO);
        }

        _discountControl.GetAllDiscount();
        ClearForm();
        this.Close();;
    }

    private void Back_btn_OnClick(object sender, RoutedEventArgs e)
    {
        ClearForm();
        this.Close();
        
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

    public void ClearForm()
    {
        title_txt.Text = describtion_txt.Text = amount_txt.Text = string.Empty;
        //amount_type_combo.Items = amount_type_combo.Items[amount_type_combo.SelectedIndex];
        
        // combo ni tozalash kerak shu yerda
        
        start_date.SelectedDate = end_date.SelectedDate = DateTime.Now;
        active_chekbox.IsChecked = false;
    }

    public void Disable(bool Isreadonly)
    {
        title_txt.IsReadOnly = Isreadonly;
        describtion_txt.IsReadOnly = Isreadonly;
        amount_txt.IsReadOnly = Isreadonly;
        title_txt.IsReadOnly = Isreadonly;
        amountType_combo.IsEnabled = !Isreadonly;
        start_date.IsEnabled = !Isreadonly;
        end_date.IsEnabled = !Isreadonly;
        active_chekbox.IsEnabled=!Isreadonly;
        products_datagrid.IsEnabled = !Isreadonly;
    }
}