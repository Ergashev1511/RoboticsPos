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

namespace RoboticsPos.UI
{
    /// <summary>
    /// Interaction logic for XodimCrudPage.xaml
    /// </summary>
    public partial class XodimCrudPage : UserControl
    {
        MainWindow mainWindow { get; set; }


        public void SetMainWinndow(MainWindow mainWindow)
        { this.mainWindow = mainWindow; }


        public XodimCrudPage()
        {
            InitializeComponent();
        }

        private void users_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_Create(object sender, RoutedEventArgs e)
        {
            // //mainWindow.XodimCrudViewBox.Visibility = Visibility.Hidden;
            // create_doc.Visibility = Visibility.Visible;
        }
    }
}
