using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using TA72.Controllers;
using TA72.Models;

namespace TA72
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProjectController projCtrl = new ProjectController();
        private Project p;
        public MainWindow()
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

        private void LoadProject(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                p = projCtrl.load(openFileDialog.FileName);
        }
    }
}
