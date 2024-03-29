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
using RoboticsPos.UI;

namespace RoboticsPos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Kirishpage.SetMainWinndow(this);
            loginpage.SetMainWinndow(this);
            pinkodpage.SetMainWinndow(this);
            menyupage.SetMainWinndow(this);
            kassapage.SetMainWinndow(this);
            shaxsiy_malPage.SetMainWindow(this);
        }
    }
}