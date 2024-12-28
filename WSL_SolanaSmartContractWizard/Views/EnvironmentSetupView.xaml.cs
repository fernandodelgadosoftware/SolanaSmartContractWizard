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
using WSL_SolanaSmartContractWizard.Services;
using WSL_SolanaSmartContractWizard.ViewModels;


namespace WSL_SolanaSmartContractWizard.Views
{
    /// <summary>
    /// Interaction logic for EnvironmentSetupView.xaml
    /// </summary>
    public partial class EnvironmentSetupView : UserControl
    {
        public EnvironmentSetupView()
        {
            InitializeComponent();
            DataContext = new EnvironmentSetupViewModel();
        }

        //private void CheckDependenciesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    if (DataContext is EnvironmentSetupViewModel viewModel)
        //    {
        //        // Run the CheckDependencies method
        //        viewModel.CheckDependencies();

        //        // Play sound for WSL if it is installed
        //        if (viewModel.IsWSLInstalled)
        //        {
        //            PlaySound();
        //        }

        //        // Play sound for Rust if it is installed
        //        if (viewModel.IsRustInstalled)
        //        {
        //            PlaySound();
        //        }

        //        // Play sound for Solana CLI if it is installed
        //        if (viewModel.IsSolanaCLIInstalled)
        //        {
        //            PlaySound();
        //        }
        //    }
        //}

        //private void PlaySound()
        //{
        //    try
        //    {
        //        // Ensure CoinSound has a valid Source
        //        if (CoinSound.Source != null)
        //        {
        //            string filePath = CoinSound.Source.AbsolutePath;

        //            // Ensure the file exists before trying to play it
        //            if (System.IO.File.Exists(filePath))
        //            {
        //                using (SoundPlayer player = new SoundPlayer(filePath))
        //                {
        //                    player.PlaySync();
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Sound file not found: {filePath}");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("CoinSound.Source is not set.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any errors while playing sound
        //        Console.WriteLine($"Error playing sound: {ex.Message}");
        //    }
        //}

        private async void CheckDependenciesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is EnvironmentSetupViewModel viewModel)
            {
                // Run the CheckDependencies method
                viewModel.CheckDependencies();

                // Play sound and update checkbox for WSL
                if (viewModel.IsWSLInstalled)
                {
                    PlaySoundAsync();
                    await Task.Delay(500); // Allow UI to update and slight delay for user experience
                    await UpdateUIAsync();
                }

                // Play sound and update checkbox for Rust
                if (viewModel.IsRustInstalled)
                {
                    PlaySoundAsync();
                    await Task.Delay(500);
                    await UpdateUIAsync();
                }

                // Play sound and update checkbox for Solana CLI
                if (viewModel.IsSolanaCLIInstalled)
                {
                    PlaySoundAsync();
                    await Task.Delay(500);
                    await UpdateUIAsync();
                }
            }
        }

        private void PlaySoundAsync()
        {
            try
            {
                // Ensure CoinSound has a valid Source
                if (CoinSound.Source != null)
                {
                    string filePath = CoinSound.Source.AbsolutePath;

                    // Ensure the file exists before trying to play it
                    if (System.IO.File.Exists(filePath))
                    {
                        using (SoundPlayer player = new SoundPlayer(filePath))
                        {
                            player.Play(); // Non-blocking sound play
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sound file not found: {filePath}");
                    }
                }
                else
                {
                    Console.WriteLine("CoinSound.Source is not set.");
                }
            }
            catch (Exception ex)
            {
                // Handle any errors while playing sound
                Console.WriteLine($"Error playing sound: {ex.Message}");
            }
        }

        //private async void CheckDependenciesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    if (DataContext is EnvironmentSetupViewModel viewModel)
        //    {
        //        // Run the CheckDependencies method
        //        viewModel.CheckDependencies();

        //        // Play sound and update checkbox for WSL
        //        if (viewModel.IsWSLInstalled)
        //        {
        //            await PlaySoundAsync();
        //            await Task.Delay(500); // Small delay for user experience
        //            await UpdateUIAsync(); // Force UI to update
        //        }

        //        // Play sound and update checkbox for Rust
        //        if (viewModel.IsRustInstalled)
        //        {
        //            await PlaySoundAsync();
        //            await Task.Delay(500);
        //            await UpdateUIAsync();
        //        }

        //        // Play sound and update checkbox for Solana CLI
        //        if (viewModel.IsSolanaCLIInstalled)
        //        {
        //            await PlaySoundAsync();
        //            await Task.Delay(500);
        //            await UpdateUIAsync();
        //        }
        //    }
        //}

        //private Task PlaySoundAsync()
        //{
        //    return Task.Run(() =>
        //    {
        //        try
        //        {
        //            // Ensure CoinSound has a valid Source
        //            if (CoinSound.Source != null)
        //            {
        //                string filePath = CoinSound.Source.AbsolutePath;

        //                // Ensure the file exists before trying to play it
        //                if (System.IO.File.Exists(filePath))
        //                {
        //                    using (SoundPlayer player = new SoundPlayer(filePath))
        //                    {
        //                        player.PlaySync(); // Block until sound finishes in this task
        //                    }
        //                }
        //                else
        //                {
        //                    Console.WriteLine($"Sound file not found: {filePath}");
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("CoinSound.Source is not set.");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle any errors while playing sound
        //            Console.WriteLine($"Error playing sound: {ex.Message}");
        //        }
        //    });
        //}

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
                        DependencyCheckService.DownloadAndInstallRust();
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

        // Helper methods for version comparison
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
