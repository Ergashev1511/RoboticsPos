using System.Windows;
using System.Windows.Controls;
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

        public void SetMainWinndow(MainWindow mainWindow, IProductService productService)
        {
            this.mainWindow = mainWindow;
            _productService = productService;
        }



        public KassaPage()
        {
            InitializeComponent();
        }

        private async void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text.Length > 2)
            {
                var data = await _productService.GetAllSearchFOrProducts(SearchTextBox.Text);
                product_datagrid.ItemsSource = data;
            }
            else
            {
                product_datagrid.ItemsSource = null;
            }
        }
       
    }
}
