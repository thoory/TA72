using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TA72.Controllers;
using TA72.Models;

namespace TA72.Views
{
    /// <summary>
    /// Logique d'interaction pour CreateProjectWindow.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window
    {
        private ProjectController projCtrl = new ProjectController();
        private Project p;
        public CreateProjectWindow()
        {
            InitializeComponent();
        }
        private void createProject(object sender, RoutedEventArgs e)
        {
            String name, description;

            name = projName.Text;
            description = projDesc.Text;

            p = projCtrl.create(name, description);

            projCtrl.save(p);
        }
    }
}
