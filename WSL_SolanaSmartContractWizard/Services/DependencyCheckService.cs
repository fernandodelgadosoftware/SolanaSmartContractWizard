using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WSL_SolanaSmartContractWizard.Services
{
    public static class DependencyCheckService
    {
        public static (bool IsInstalled, string Output) CheckWSL()
        {
            return RunCommand("wsl", "--version");
        }

        public static (bool IsInstalled, string Output) CheckRust()
        {
            return RunWSLCommand("rustc", "--version");
        }

        public static (bool IsInstalled, string Output) CheckCargo()
        {
            return RunWSLCommand("cargo", "--version");
        }

        public static (bool IsInstalled, string Output) CheckRustup()
        {
            return RunWSLCommand("rustup", "--version");
        }

        public static (bool IsInstalled, string Output) CheckSolanaCLI()
        {
            return RunWSLCommand("solana", "--version");
        }

        public static (bool IsInstalled, string Output) CheckNode()
        {
            return RunWSLCommand("npm", "--version");
        }

        private static (bool IsInstalled, string Output) RunWSLCommand(string command, string arguments)
        {
            try
            {
                // Modify the command to run two commands separated by '&&'
                string wslCommand = $"bash -ic \"{command} {arguments} && echo 'second command'\"";

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "wsl.exe", // Directly use 'wsl' to invoke the WSL environment
                        Arguments = wslCommand, // Pass bash -c with the command as argument
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        StandardOutputEncoding = System.Text.Encoding.UTF8,
                        StandardErrorEncoding = System.Text.Encoding.UTF8
                    }
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // Clean up null characters in the output
                output = output.Replace("\0", string.Empty);

                return (process.ExitCode == 0, output);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }




        private static (bool IsInstalled, string Output) RunCommand(string command, string arguments)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = command,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        StandardOutputEncoding = System.Text.Encoding.UTF8
                    }
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // Deletes null characters in the WSL readout, maybe revisit why it does that, maybe Encoding characters bc Windows != Linux
                output = output.Replace("\0", string.Empty);

                return (process.ExitCode == 0, output);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }


        public static async Task CheckWSLAsync(Action<string> onOutputReceived)
        {
            await RunCommandAsync("wsl --version", onOutputReceived);
        }

        public static async Task CheckRustAsync(Action<string> onOutputReceived)
        {
            await RunCommandAsync("rustc --version", onOutputReceived);
        }

        public static async Task CheckSolanaCLIAsync(Action<string> onOutputReceived)
        {
            await RunCommandAsync("solana --version", onOutputReceived);
        }

        private static async Task RunCommandAsync(string command, Action<string> onOutputReceived)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/C {command}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        // Clean the output (remove null characters or unwanted characters)
                        string cleanedOutput = e.Data.Replace("\0", string.Empty);

                        // Split by newline to handle multi-line output properly
                        string[] lines = cleanedOutput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var line in lines)
                        {
                            // Send each line of output to the callback
                            onOutputReceived(line);
                        }
                    }
                };

                process.Start();
                process.BeginOutputReadLine();

                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    onOutputReceived($"Error: Command exited with code {process.ExitCode}");
                }
            }
            catch (Exception ex)
            {
                onOutputReceived($"Error: {ex.Message}");
            }
        }


        // Example method to download a file using HttpClient
        public static async Task DownloadFileAsync(string url, string destinationPath)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                using (var fs = new System.IO.FileStream(destinationPath, System.IO.FileMode.Create))
                {
                    await response.Content.CopyToAsync(fs);
                }
            }
        }

        public static async Task DownloadAndInstallWSL()
        {
            string wslDownloadUrl = "https://aka.ms/wslstore";  // Example URL, replace with actual
            string destinationPath = "C:\\Users\\admin\\Downloads\\wsl_installer.exe";  // Example destination path

            try
            {
                await DownloadFileAsync(wslDownloadUrl, destinationPath);
                // Optionally, invoke the downloaded file here to run the installer, e.g.:
                System.Diagnostics.Process.Start(destinationPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading WSL: {ex.Message}");
            }
        }

        public static async Task DownloadAndInstallRust()
        {
            string rustDownloadUrl = "https://static.rust-lang.org/rustup/dist/i686-pc-windows-msvc/rustup-init.exe";  // Example URL
            string destinationPath = "C:\\Users\\admin\\Downloads\\rust_installer.exe";

            try
            {
                await DownloadFileAsync(rustDownloadUrl, destinationPath);
                // Optionally, invoke the downloaded file here to run the installer
                System.Diagnostics.Process.Start(destinationPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading Rust: {ex.Message}");
            }
        }

        public static async Task DownloadAndInstallSolanaCLI()
        {
            string solanaDownloadUrl = "https://github.com/solana-labs/solana/releases/download/v1.11.0/solana-release-x86_64-unknown-linux-gnu.tar.bz2";  // Example URL
            string destinationPath = "C:\\Users\\admin\\Downloads\\solana-cli.tar.bz2";

            try
            {
                await DownloadFileAsync(solanaDownloadUrl, destinationPath);
                // Optionally, invoke the downloaded file here to extract and install Solana CLI
                Console.WriteLine("Solana CLI downloaded. You may need to extract and install it.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading Solana CLI: {ex.Message}");
            }
        }

        public static async Task DownloadAndInstallNode()
        {
            string solanaDownloadUrl = "https://github.com/solana-labs/solana/releases/download/v1.11.0/solana-release-x86_64-unknown-linux-gnu.tar.bz2";  // Example URL
            string destinationPath = "C:\\Users\\admin\\Downloads\\solana-cli.tar.bz2";

            try
            {
                await DownloadFileAsync(solanaDownloadUrl, destinationPath);
                // Optionally, invoke the downloaded file here to extract and install Solana CLI
                Console.WriteLine("Solana CLI downloaded. You may need to extract and install it.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading Solana CLI: {ex.Message}");
            }
        }
    }


    //old implementation for posterity
    //public static class DependencyCheckService
    //{
    //    public static bool IsWSLInstalled()
    //    {
    //        var process = new Process
    //        {
    //            StartInfo = new ProcessStartInfo
    //            {
    //                FileName = "wsl",
    //                Arguments = "--version",
    //                RedirectStandardOutput = true,
    //                UseShellExecute = false,
    //                CreateNoWindow = true
    //            }
    //        };

    //        try
    //        {
    //            process.Start();
    //            string output = process.StandardOutput.ReadToEnd();
    //            process.WaitForExit();
    //            return !string.IsNullOrEmpty(output);
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }

    //    public static bool IsRustInstalled()
    //    {
    //        return CheckCommandAvailability("rustc --version");
    //    }

    //    public static bool IsSolanaCLIInstalled()
    //    {
    //        return CheckCommandAvailability("solana --version");
    //    }

    //    private static bool CheckCommandAvailability(string command)
    //    {
    //        try
    //        {
    //            var process = new Process
    //            {
    //                StartInfo = new ProcessStartInfo
    //                {
    //                    FileName = "cmd.exe",
    //                    Arguments = $"/C {command}",
    //                    RedirectStandardOutput = true,
    //                    UseShellExecute = false,
    //                    CreateNoWindow = true
    //                }
    //            };
    //            process.Start();
    //            process.WaitForExit();
    //            return process.ExitCode == 0;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }
    //}

}
