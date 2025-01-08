using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Numerics;
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
using System.Windows.Controls;
using WSL_SolanaSmartContractWizard.Services;
using WSL_SolanaSmartContractWizard.ViewModels;


namespace WSL_SolanaSmartContractWizard.Views
{
    public partial class EnvironmentSetupView : UserControl
    {
        public EnvironmentSetupView()
        {
            InitializeComponent();
            DataContext = new EnvironmentSetupViewModel();

        }

        private void CheckDependenciesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is EnvironmentSetupViewModel viewModel)
            {
                // Run the CheckDependencies method
                viewModel.CheckDependencies();

                // Play sound for WSL if it is installed
                if (viewModel.IsWSLInstalled)
                {
                    PlayCoinDropSound();
                }

                // Play sound for Rust if it is installed
                if (viewModel.IsRustInstalled)
                {
                    PlayCoinDropSound();
                }

                // Play sound for Solana CLI if it is installed
                if (viewModel.IsSolanaCLIInstalled)
                {
                    PlayCoinDropSound();
                }
            }
        }

        //private async void CheckDependenciesButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DataContext is EnvironmentSetupViewModel viewModel)
        //    {
        //        OutputTextBox.Clear(); // Clear previous output

        //        await viewModel.CheckDependenciesAsync(output =>
        //        {
        //            OutputTextBox.AppendText(output + Environment.NewLine);
        //            OutputTextBox.ScrollToEnd();
        //        });

        //        // Play sounds for installed dependencies
        //        if (viewModel.IsWSLInstalled) PlayCoinDropSound();
        //        if (viewModel.IsRustInstalled) PlayCoinDropSound();
        //        if (viewModel.IsSolanaCLIInstalled) PlayCoinDropSound();
        //    }
        //}

        //private async void CheckDependenciesButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DataContext is EnvironmentSetupViewModel viewModel)
        //    {
        //        // Clear all previous outputs
        //        WSLOutputTextBox.Clear();
        //        RustOutputTextBox.Clear();
        //        SolanaOutputTextBox.Clear();

        //        // Check dependencies asynchronously
        //        await viewModel.CheckDependenciesAsync(output =>
        //        {
        //            // This callback updates the appropriate TextBox based on the output
        //            if (output.Contains("WSL"))
        //            {
        //                WSLOutputTextBox.AppendText(output + Environment.NewLine);
        //                WSLOutputTextBox.ScrollToEnd();
        //            }
        //            else if (output.Contains("rustc"))
        //            {
        //                RustOutputTextBox.AppendText(output + Environment.NewLine);
        //                RustOutputTextBox.ScrollToEnd();
        //            }
        //            else if (output.Contains("solana-cli"))
        //            {
        //                SolanaOutputTextBox.AppendText(output + Environment.NewLine);
        //                SolanaOutputTextBox.ScrollToEnd();
        //            }
        //        });

        //        // Play sounds for installed dependencies if they are installed
        //        if (viewModel.IsWSLInstalled) PlayCoinDropSound();
        //        if (viewModel.IsRustInstalled) PlayCoinDropSound();
        //        if (viewModel.IsSolanaCLIInstalled) PlayCoinDropSound();
        //    }
        //}


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

        private Task UpdateUIAsync()
        {
            return Application.Current.Dispatcher.InvokeAsync(() => { }).Task;
        }

        private async void DownloadWSLButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var (isWSLInstalled, wslOutput) = DependencyCheckService.CheckWSL();

                if (!isWSLInstalled)
                {
                    MessageBox.Show("WSL is not installed. Downloading and installing the latest version...");
                    await DependencyCheckService.DownloadAndInstallWSL();
                }
                else
                {
                    MessageBox.Show("WSL is already installed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
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

        private async void DownloadSolanaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var (isSolanaInstalled, solanaVersionOutput) = DependencyCheckService.CheckSolanaCLI();

                if (!isSolanaInstalled)
                {
                    MessageBox.Show("Solana CLI is not installed. Downloading and installing the latest version...");
                    await DependencyCheckService.DownloadAndInstallSolanaCLI();
                }
                else
                {
                    var installedVersion = ParseVersion(solanaVersionOutput);
                    var latestVersion = GetLatestSolanaVersion();

                    if (installedVersion != latestVersion)
                    {
                        MessageBox.Show($"An older version of Solana CLI is installed (v{installedVersion}). Updating to v{latestVersion}...");
                        await DependencyCheckService.DownloadAndInstallSolanaCLI();
                    }
                    else
                    {
                        MessageBox.Show("You already have the latest version of Solana CLI installed.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void DownloadNodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var (isNodeInstalled, nodeVersionOutput) = DependencyCheckService.CheckNode();

                if (!isNodeInstalled)
                {
                    MessageBox.Show("Node is not installed in WSL. Downloading and installing the latest version...");
                    await DependencyCheckService.DownloadAndInstallNode();
                }
                else
                {
                    var installedVersion = ParseVersion(nodeVersionOutput);
                    var latestVersion = GetLatestNodeVersion();

                    if (installedVersion != latestVersion)
                    {
                        MessageBox.Show($"An older version of Node is installed (v{installedVersion}). Updating to v{latestVersion}...");
                        await DependencyCheckService.DownloadAndInstallNode();
                    }
                    else
                    {
                        MessageBox.Show("You already have the latest version of Node installed.");
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

        private string GetLatestNodeVersion()
        {
            return "1.11.0";  // Example: Replace with actual logic to fetch the latest version
        }

        private string GetLatestRustVersion()
        {
            return "1.60.0";  // Example: Replace with actual logic to fetch the latest version
        }
    }
}
