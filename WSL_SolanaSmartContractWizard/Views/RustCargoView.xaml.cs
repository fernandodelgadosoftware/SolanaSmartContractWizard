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
using WSL_SolanaSmartContractWizard.Services;
using WSL_SolanaSmartContractWizard.ViewModels;

namespace WSL_SolanaSmartContractWizard.Views
{
    /// <summary>
    /// Interaction logic for RustCargoView.xaml
    /// </summary>
    public partial class RustCargoView : UserControl
    {
        public RustCargoView()
        {
            InitializeComponent();
            DataContext = new RustCargoViewModel();
        }

        private void CheckDependenciesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is RustCargoViewModel viewModel)
            {
                viewModel.CheckDependencies();

                if (viewModel.IsRustInstalled)
                {
                    PlayCoinDropSound();
                }

                if (viewModel.IsCargoInstalled)
                {
                    PlayCoinDropSound();
                }

                if (viewModel.IsRustupInstalled)
                {
                    PlayCoinDropSound();
                }
            }
        }

        private void PlayCoinDropSound()
        {
            try
            {
                var uri = new Uri("pack://application:,,,/WSL_SolanaSmartContractWizard;component/Sounds/coindrops.wav");
                var sri = Application.GetResourceStream(uri);

                if (sri != null)
                {
                    Console.WriteLine("Stream is not null.");

                    using (var s = sri.Stream)
                    {
                        Console.WriteLine($"Stream length: {s.Length} bytes");
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(s);
                        player.Load();
                        player.PlaySync();
                        Console.WriteLine("Sound should have played.");
                    }
                }
                else
                {
                    Console.WriteLine("Stream is null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing sound: {ex.Message}");
            }
        }

        private async void DownloadRustButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var (isRustInstalled, rustVersionOutput) = DependencyCheckService.CheckRust();

                if (!isRustInstalled)
                {
                    MessageBox.Show("Rust is not installed. Downloading and installing the latest version...");
                    await DependencyCheckService.DownloadAndInstallRust();
                }
                else
                {
                    var installedVersion = ParseVersion(rustVersionOutput);
                    var latestVersion = GetLatestRustVersion();

                    if (installedVersion != latestVersion)
                    {
                        MessageBox.Show($"An older version of Rust is installed (v{installedVersion}). Updating to v{latestVersion}...");
                        await DependencyCheckService.DownloadAndInstallRust();
                    }
                    else
                    {
                        MessageBox.Show("You already have the latest version of Rust installed.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private string ParseVersion(string versionOutput)
        {
            var parts = versionOutput.Split(' ');
            return parts.Length > 1 ? parts[1] : string.Empty;
        }

        private string GetLatestSolanaVersion()
        {
            return "1.11.0";  // Example: Replace with actual logic to fetch the latest version
        }

        private string GetLatestRustVersion()
        {
            return "1.60.0";  // Example: Replace with actual logic to fetch the latest version
        }
    }
}
