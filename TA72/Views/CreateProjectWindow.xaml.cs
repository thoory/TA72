using System.Windows;

namespace TA72.Views
{
    /// <summary>
    /// Logique d'interaction pour CreateProjectWindow.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window
    {
        public CreateProjectWindow(string name, string description, string buttonValue)
        {
            InitializeComponent();
            if(name != "Unknown" || description != "Unknown")
            {
                projName.Text = name;
                projDesc.Text = description;
            }
            Validate.Content = buttonValue;
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
