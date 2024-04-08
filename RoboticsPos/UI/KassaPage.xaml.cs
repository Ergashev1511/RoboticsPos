using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Services;

namespace RoboticsPos.UI
{
    /// <summary>
    /// Interaction logic for KassaPage.xaml
    /// </summary>
    public partial class KassaPage : UserControl
    {

        MainWindow mainWindow { get; set; }
        private IProductService _productService { get; set; }
        private ProductSearchDTO selectedProduct { get; set; }
        private List<ProductSearchDTO> Products = new List<ProductSearchDTO>();
        private List<ProductForKassaDTO> productsCash = new List<ProductForKassaDTO>();
        private ProductForKassaDTO selectedkassaForKassaDto { get; set; }

        public void SetMainWinndow(MainWindow mainWindow, IProductService productService)
        {
            this.mainWindow = mainWindow;
            _productService = productService;
        }
        public KassaPage()
        {
            InitializeComponent();
        }

        public void ChangeQuantityProuct(decimal quantity)
        {
            productsCash.Where(a=>a.Id==selectedkassaForKassaDto.Id).ToList().ForEach(a =>
            {
                a.Quantity = quantity;
                a.TotalPrice = a.Price * a.Quantity;
            });
            product_datagrid.ItemsSource = productsCash;
            product_datagrid.Items.Refresh();
            summatxt.Text = productsCash.Sum(a => a.TotalPrice).ToString();
        }

        // private async void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        // {
        //     if (SearchTextBox.Text.Length > 2)
        //     {
        //         var data = await _productService.GetAllSearchFOrProducts(SearchTextBox.Text);
        //         product_datagrid.ItemsSource = data;
        //     }
        //     else
        //     {
        //         product_datagrid.ItemsSource = null;
        //     }
        // }

        private async void Searchcombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchcombo.SelectedIndex >= 0)
            {
                selectedProduct = Products[searchcombo.SelectedIndex];

                if (selectedProduct != null)
                {
                   AddProduct(selectedProduct.Id);
                }
            }
        }

        private async void AddProduct(long productId)
        {
            var product = await _productService.GetProductById(selectedProduct.Id);
                   
            if (product.Amount > 1)
            {
                if (productsCash.Any(a => a.Id == product.Id))
                {
                    productsCash.Where(a=>a.Id==product.Id).ToList().ForEach(a =>
                    {
                        a.Quantity++;
                        a.TotalPrice = a.Quantity * a.Price;
                    });
                }
                else
                {
                    productsCash.Add(new ProductForKassaDTO()
                    {
                        Index = productsCash.Count+1,
                        Id = product.Id,
                        Name = product.Name,
                        BarCode = product.BarCode,
                        Price = product.Price,
                        Quantity = 1,
                        TotalPrice = product.Price
                    });
                }
                        
                product_datagrid.ItemsSource = productsCash;
                product_datagrid.Items.Refresh();
                summatxt.Text = productsCash.Sum(a => a.TotalPrice).ToString();
            }
            else
            {
                MessageBox.Show("Mahsulot soni yetarli emas!");
            }
        }

        private async void Searchcombo_OnKeyUp(object sender, KeyEventArgs e)
        {
                if (searchcombo.Text.Length > 2)
                {
                    Products = await _productService.GetAllSearchFOrProducts(searchcombo.Text);
                    searchcombo.ItemsSource = Products.Select(a => a.Name);
                }
                else
                {
                    searchcombo.ItemsSource = null;
                }
            
           
        }

        private void exit_btn1_OnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.KassaViewBox.Visibility = Visibility.Collapsed;
            mainWindow.KirishViewBox.Visibility = Visibility.Visible;
            ClearCash();

        }       

        private void Add_btn_OnClick(object sender, RoutedEventArgs e)
        {
            var product = product_datagrid.SelectedItem as ProductForKassaDTO;
            if (product != null)
            {
                AddProduct(product.Id);
            }
        }

        private void Remove_btn_OnClick(object sender, RoutedEventArgs e)
        {
            var product = product_datagrid.SelectedItem as ProductForKassaDTO;
            if (product != null && product.Quantity > 1)
            {
                productsCash.Where(a => a.Id == product.Id).ToList().ForEach(a =>
                {
                    a.Quantity--;
                    a.TotalPrice = a.Price * a.Quantity;
                });
            }
            else if(product != null && product.Quantity == 1)
            {
                productsCash.Remove(product);
            }
            product_datagrid.ItemsSource = productsCash;
                product_datagrid.Items.Refresh();
                summatxt.Text = productsCash.Sum(a => a.TotalPrice).ToString();

        }

        private async void Tahrir_btn_OnClick(object sender, RoutedEventArgs e)
        {
             selectedkassaForKassaDto = product_datagrid.SelectedItem as ProductForKassaDTO;
            if (selectedkassaForKassaDto != null)
            {
                var prod = await _productService.GetProductById(selectedkassaForKassaDto.Id);
                EditCountForm editCountForm = new EditCountForm();
                editCountForm.SetValues(this,prod.Amount,selectedkassaForKassaDto.Quantity);
                editCountForm.ShowDialog();
            }
        }
        
        
       
        private void ClearCash()
        {
            Products.Clear();
            productsCash.Clear();
            product_datagrid.ItemsSource = null;
            searchcombo.ItemsSource= null;
            summatxt.Text = string.Empty;
         //   final_sum_lbl.Content =string.Empty;
        } 
        
    }
}
