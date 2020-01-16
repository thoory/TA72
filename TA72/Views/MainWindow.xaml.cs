using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Net;
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
        private AllDataContext dataCtrl = new AllDataContext();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = dataCtrl;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) { }

        private void mnuNew_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectWindow newProjectWindow = new CreateProjectWindow(dataCtrl.ProjCtrl.Name, dataCtrl.ProjCtrl.Description, "Create");
            if (newProjectWindow.ShowDialog() == true)
            {
                dataCtrl.ProjCtrl.CreateProject(newProjectWindow.projName.Text, newProjectWindow.projDesc.Text);
            }
        }
        private void mnuOpen_Click(object sender, RoutedEventArgs e)
        {
            dataCtrl.ProjCtrl.Load();
        }
        private void mnuSave_Click(object sender, RoutedEventArgs e)
        {
            dataCtrl.ProjCtrl.Save();
        }
        private void mnuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            dataCtrl.ProjCtrl.SaveAs();
        }
        private void mnuProjModify_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectWindow newProjectWindow = new CreateProjectWindow(dataCtrl.ProjCtrl.Name, dataCtrl.ProjCtrl.Description, "Change");
            if (newProjectWindow.ShowDialog() == true)
            {
                dataCtrl.ProjCtrl.Name = newProjectWindow.projName.Text;
                dataCtrl.ProjCtrl.Description = newProjectWindow.projDesc.Text;
            }
        }

        private void Refresh_Ip_Clik(object sender, RoutedEventArgs e)
        {
            dataCtrl.NetCtrl.GetHostIp();
        }

        private void Launch_Scan_Click(object sender, RoutedEventArgs e)
        {
            dataCtrl.NetCtrl.NetworkScan();
        }

        private void cmbbxIpList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmd = sender as ComboBox;
            dataCtrl.NetCtrl.IpHost = cmd.SelectedItem as IPAddress;
        }
    }
}
