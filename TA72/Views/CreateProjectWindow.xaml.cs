using System.Windows;

namespace TA72.Views
{
    /// <summary>
    /// Logique d'interaction pour CreateProjectWindow.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window
    {
        public CreateProjectWindow()
        {
            InitializeComponent();
        }
        private void createProject(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
