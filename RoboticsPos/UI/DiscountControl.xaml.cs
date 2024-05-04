using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI;

public partial class DiscountControl : UserControl
{
    private SettingsPage _settingsPage { get; set; }
    private IDiscountService _discountService { get; set; }
    private IProductService _productService { get; set; }
    private DiscountDTO _discountDto { get; set; }
    public void SetVariablies(SettingsPage settingsPage,IDiscountService discountService,IProductService productService)
    {
        _settingsPage = settingsPage;
        _discountService = discountService;
        _productService = productService;
        GetAllDiscount();
    }

    public async void GetAllDiscount()
    {
        var discounts = await _discountService.GetAllDiscount();
        if (discounts.Any())
        {
            discount_datagrid.ItemsSource = discounts;
            discount_datagrid.Items.Refresh();
        }
    }
    public DiscountControl()
    {
        InitializeComponent();
    }

    private void Create_btn_OnClick(object sender, RoutedEventArgs e)
    {
        DiscountForm discountForm = new DiscountForm();
       discountForm.SetVariablies(this,_discountService,_productService);
        discountForm.GetAllProducts();
        discountForm.ShowDialog();
    }

    private void Edit_btn_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedDiscount = discount_datagrid.SelectedItem as DiscountDTO;
        if(selectedDiscount != null)
        {
            DiscountForm discountForm = new DiscountForm();
            discountForm.SetVariablies(this, _discountService, _productService);
           discountForm.FillForm(selectedDiscount.Id);
            discountForm.ShowDialog();
        }
    }

    private async void Delete_btn_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedDiscount = discount_datagrid.SelectedItem as DiscountDTO;
        if(selectedDiscount != null)
        {
            await _discountService.DeleteDiscount(selectedDiscount.Id);
            GetAllDiscount();
        } 
    }
    private void Discount_datagrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _discountDto=discount_datagrid.SelectedItem as DiscountDTO;
    }
    
    
    private void Discount_datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        DiscountForm discountForm = new DiscountForm(); 
        discountForm.SetVariablies(this,_discountService,_productService);
        discountForm.FillForm(_discountDto.Id,true);
        if (_discountDto != null)
        {
          _discountDto=discount_datagrid.SelectedItem as DiscountDTO;
          
             discountForm.save_btn.Visibility = Visibility.Collapsed;
            discountForm.back_btn.Visibility = Visibility.Visible;
            
            discountForm.ShowDialog();
           
        }
        else
        {
            MessageBox.Show("Select any CheckPrinter");
        }

    }

    
}