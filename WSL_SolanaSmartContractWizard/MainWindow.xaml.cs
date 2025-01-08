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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainTabControl.SelectionChanged += MainTabControl_SelectionChanged;
            var mainWindow = (MainWindow)Application.Current.MainWindow;

            var selectedTab = mainWindow.MainTabControl.Items[1] as TabItem;
            if (selectedTab?.Content is FrameworkElement content)
            {
                var marioPipe = content.FindName("MarioPipe") as Image;
                if (marioPipe != null)
                {
                    ImageBehavior.SetAutoStart(marioPipe, false);
                }
            }

            var selectedTab2 = mainWindow.MainTabControl.Items[2] as TabItem;
            if (selectedTab2?.Content is FrameworkElement content2)
            {
                var hondaSmash = content2.FindName("HondaSmash") as Image;
                if (hondaSmash != null)
                {
                    ImageBehavior.SetAutoStart(hondaSmash, false);
                }
            }
        }

        public async void NextTabButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            
            if (mainWindow.MainTabControl.SelectedIndex == 1)
            {
                var selectedTab = mainWindow.MainTabControl.Items[1] as TabItem;
                if (selectedTab?.Content is FrameworkElement content)
                {                   
                    var marioPipe = content.FindName("MarioPipe") as Image;
                    if (marioPipe != null)
                    {
                        ImageBehavior.SetAutoStart(marioPipe, true);
                        ImageBehavior.SetRepeatBehavior(marioPipe, new System.Windows.Media.Animation.RepeatBehavior(1));
                        //await Task.Delay(600);
                        PlayPipeSound();
                        await Task.Delay(1200);
                        
                        ImageBehavior.SetAutoStart(marioPipe, false);
                        ImageBehavior.SetRepeatBehavior(marioPipe, new System.Windows.Media.Animation.RepeatBehavior(0)); 
                    }
                }
            }

            if (mainWindow.MainTabControl.SelectedIndex == 2)
            {
                var selectedTab = mainWindow.MainTabControl.Items[2] as TabItem;
                if (selectedTab?.Content is FrameworkElement content)
                {
                    var hondaSmash = content.FindName("HondaSmash") as Image;
                    if (hondaSmash != null)
                    {
                        ImageBehavior.SetAutoStart(hondaSmash, true);
                        ImageBehavior.SetRepeatBehavior(hondaSmash, new System.Windows.Media.Animation.RepeatBehavior(1));


                        PlayKenSound();
                        await Task.Delay(1200);

                        ImageBehavior.SetAutoStart(hondaSmash, false);
                        ImageBehavior.SetRepeatBehavior(hondaSmash, new System.Windows.Media.Animation.RepeatBehavior(0));
                    }
                }
            }

            mainWindow.MainTabControl.SelectedIndex++;
        }

        public void PreviousTabButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainTabControl.SelectedIndex--;            
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var currentTabIndex = mainWindow.MainTabControl.SelectedIndex;

            PreviousTabButton.Visibility = currentTabIndex > 0 ? Visibility.Visible : Visibility.Collapsed;
            NextTabButton.Visibility = currentTabIndex < mainWindow.MainTabControl.Items.Count - 1 ? Visibility.Visible : Visibility.Collapsed;
            PlayTabChangeSound();
        }
        private void PlayTabChangeSound()
        {
            PlaySound("page-flip-10.wav");
        }

        private void PlayPipeSound()
        {
            PlaySound("pipe.wav");
        }

        private void PlayKenSound()
        {
            PlaySound("ken.wav");
        }

        private void PlaySound(string soundPath)
        {
            try
            {
                var sri = Application.GetResourceStream(new Uri($"pack://application:,,,/WSL_SolanaSmartContractWizard;component/Sounds/{soundPath}"));

                if (sri != null)
                {
                    using (var s = sri.Stream)
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(s);

                        player.Load();
                        Thread.Sleep(500);
                        player.Play();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing sound ({soundPath}): {ex.Message}");
            }
        }
    }
}