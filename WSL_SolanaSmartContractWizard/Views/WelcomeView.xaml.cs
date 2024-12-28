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

namespace WSL_SolanaSmartContractWizard.Views
{
    /// <summary>
    /// Interaction logic for WelcomeView.xaml
    /// </summary>
    public partial class WelcomeView : UserControl
    {
        public WelcomeView()
        {
            InitializeComponent();
        }

        public void NextTabButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic to switch tabs
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainTabControl.SelectedIndex = 1; // Switch to the second tab (index 1)
        }

    }
}
