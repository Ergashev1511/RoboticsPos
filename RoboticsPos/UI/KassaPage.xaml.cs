using System.Windows.Controls;

namespace RoboticsPos.UI
{
    /// <summary>
    /// Interaction logic for KassaPage.xaml
    /// </summary>
    public partial class KassaPage : UserControl
    {

        MainWindow mainWindow { get; set; }


        public void SetMainWinndow(MainWindow mainWindow)
        { this.mainWindow = mainWindow; }
        public KassaPage()
        {
            InitializeComponent();
        }

    }
}
