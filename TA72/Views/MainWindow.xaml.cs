using Microsoft.Win32;
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

namespace TA72.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProjectController projCtrl = new ProjectController();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = projCtrl;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) { }

        private void mnuNew_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectWindow newProjectWindow = new CreateProjectWindow();
            if (newProjectWindow.ShowDialog() == true)
            {
                projCtrl.Name = newProjectWindow.projName.Text;
                projCtrl.Description = newProjectWindow.projDesc.Text;
            }
        }
        private void mnuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "json (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                projCtrl.load(openFileDialog.FileName);
            }
        }
        private void mnuSave_Click(object sender, RoutedEventArgs e)
        {
            projCtrl.save();
        }
        private void mnuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            //ToDo
        }

        private void DataGridScanPort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
