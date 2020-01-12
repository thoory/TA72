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
using TA72.Models;

namespace TA72.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProjectController projCtrl = new ProjectController();
        private Project p;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuNew_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectWindow newProjectWindow = new CreateProjectWindow();
            if (newProjectWindow.ShowDialog() == true)
                projCtrl.load(newProjectWindow.Name);
        }
        private void mnuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "json (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                p = projCtrl.load(openFileDialog.FileName);
                Title = string.Concat(Title, " - ", p.Name);
            }
        }
        private void mnuSave_Click(object sender, RoutedEventArgs e)
        {
            projCtrl.save(p);
        }
        private void mnuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            //ToDo
        }
    }
}
