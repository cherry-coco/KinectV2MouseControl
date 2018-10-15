using System.Windows;
using System.Windows.Input;

namespace KinectV2MouseControl
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            KinectReader.window = this;
            CursorViewModel.LoadSettings();
        }

        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            CursorViewModel.ResetToDefault();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            CursorViewModel.Quit();
        }

        private void window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D && ((Keyboard.Modifiers & ModifierKeys.Control) > 0))
            {
                DisableRadio.IsChecked = true;
            }
        }
    }
}
