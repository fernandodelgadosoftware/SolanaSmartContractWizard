using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Interaction logic for WalletConfigurationView.xaml
    /// </summary>
    public partial class WalletConfigurationView : UserControl
    {
        public WalletConfigurationView()
        {
            InitializeComponent();
        }

        private void CreateNewWalletButton_Click(object sender, RoutedEventArgs e)
        {
            // Play the "Asterisk" system sound
            SystemSounds.Asterisk.Play();
        }

        private void ConnectExistingWalletButton_Click(object sender, RoutedEventArgs e)
        {
            // Play the "Exclamation" system sound
            SystemSounds.Hand.Play();
        }
    }
}
