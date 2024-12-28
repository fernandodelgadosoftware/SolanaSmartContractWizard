using System.Media;
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
using WpfAnimatedGif;

namespace WSL_SolanaSmartContractWizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainTabControl.SelectionChanged += MainTabControl_SelectionChanged;
        }

        //SoundPlayer player = new SoundPlayer(@"Sounds/page-flip-10.wav"); // Adjust the path if needed
        //try
        //{
        //    player.Play();
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show($"Error playing sound: {ex.Message}");
        //}

        //    private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {
        //        // Code to execute when tab changes
        //        if (MainTabControl.SelectedIndex == 0) // Welcome Tab
        //        {
        //            // Play sound for Welcome tab
        //            PlayTabChangeSound();
        //        }
        //        else if (MainTabControl.SelectedIndex == 1) // Environment Setup Tab
        //        {
        //            // Play sound for Environment Setup tab
        //            PlayTabChangeSound();
        //        }
        //    }

        //    private static void PlayTabChangeSound()
        //    {
        //        // Assuming you have a sound file named "TabChangeSound.wav" in the "Sounds" folder
        //        var soundPlayer = new SoundPlayer("Sounds/page-flip-10.wav");
        //        soundPlayer.Play();
        //    }
        //}

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlayTabChangeSoundAndWait();
        }

        private void PlayTabChangeSoundAndWait()
        {
            if (TabSound != null)
            {
                try
                {
                    // Get the absolute file path
                    string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "page-flip-10.wav");

                    // Check if the file exists
                    if (System.IO.File.Exists(filePath))
                    {
                        TabSound.Source = new Uri(filePath, UriKind.Absolute);
                    }
                    else
                    {
                        MessageBox.Show($"Sound file not found at: {filePath}");
                        return;
                    }

                    // Log various properties for debugging (optional)
                    Console.WriteLine($"Source: {TabSound.Source}");
                    Console.WriteLine($"Position: {TabSound.Position}");
                    Console.WriteLine($"NaturalDuration: {TabSound.NaturalDuration}");
                    Console.WriteLine($"Volume: {TabSound.Volume}");
                    Console.WriteLine($"Balance: {TabSound.Balance}");
                    Console.WriteLine($"SpeedRatio: {TabSound.SpeedRatio}");
                    Console.WriteLine($"HasAudio: {TabSound.HasAudio}");

                    // Ensure the sound file is valid
                    if (TabSound.Source == null || string.IsNullOrEmpty(TabSound.Source.LocalPath) || !System.IO.File.Exists(TabSound.Source.LocalPath))
                    {
                        MessageBox.Show("Sound file not found or path is empty.");
                        return;
                    }

                    // Set the initial playback settings
                    TabSound.Volume = 1.0;  // Set to full volume
                    TabSound.Position = TimeSpan.Zero;  // Start from the beginning of the file

                    // Play the sound
                    TabSound.Play();

                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during playback
                    MessageBox.Show($"Error playing sound: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("MediaElement for tab sound not found.");
            }
        }


    }
}