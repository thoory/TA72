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
using TA72.Models;
using MahApps.Metro.Controls;

namespace TA72.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
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
        private void mnuCreateEquipement(object sender, RoutedEventArgs e)
        {
            CreateEquipementWindow newEquipementWindow = new CreateEquipementWindow();
            if (newEquipementWindow.ShowDialog() == true)
            {
                dataCtrl.EquiCtrl.Create(newEquipementWindow.ProjName.Text);
                CreateEquipFromWindow(newEquipementWindow);
                dataCtrl.EquiCtrl.AddToProject(dataCtrl.ProjCtrl);
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

        private void Port_Scan_Click(object sender, RoutedEventArgs e)
        {
            ipInvalidPortScan.Visibility = Visibility.Collapsed;
            IpScanTextBox.Text = ScanList.SelectedItem.ToString();
            Dispatcher.BeginInvoke((Action)(() => scanTabControl.SelectedIndex = 1));
            dataCtrl.NetCtrl.PortScan(ScanList.SelectedItem as IPAddress);
        }
        private void Scan_from_TextBox_Click(object sender, RoutedEventArgs e)
        {
            ScanList.SelectedItem = -1;
            if(dataCtrl.NetCtrl.StringtoIp(IpScanTextBox.Text) != null) {
                ipInvalidPortScan.Visibility = Visibility.Collapsed;
                dataCtrl.NetCtrl.PortScan(dataCtrl.NetCtrl.StringtoIp(IpScanTextBox.Text));
            } else {
                ipInvalidPortScan.Visibility = Visibility.Visible;
            }
                
        }

        private void Add_Equipement_From_Scan_Click(object sender, RoutedEventArgs e)
        {
            IPAddress ip = ScanList.SelectedItem as IPAddress;
            dataCtrl.EquiCtrl.Create(ip);
            dataCtrl.EquiCtrl.AddToProject(dataCtrl.ProjCtrl);
        }
        

        private void RefreshStatus(object sender, RoutedEventArgs e)
        {
            dataCtrl.EquiCtrl.IsActiveCheck(dataCtrl.ProjCtrl);
        }
        
        private void RemoveEquipement_Click(object sender, RoutedEventArgs e)
        {
            if (SetSelectedEquip())
            {
                dataCtrl.EquiCtrl.DeleteFromProject(dataCtrl.ProjCtrl);
                dataCtrl.EquiCtrl.Equipement = null;
            }
                
        }
        private void ModifyEquipement_Click(object sender, RoutedEventArgs e)
        {
            if (SetSelectedEquip())
            {
                string ip = dataCtrl.EquiCtrl.Ip != null ? dataCtrl.EquiCtrl.Ip.ToString() : null;
                CreateEquipementWindow newEquipementWindow = new CreateEquipementWindow(
                    dataCtrl.EquiCtrl.Name,
                    dataCtrl.EquiCtrl.Type,
                    dataCtrl.EquiCtrl.Os,
                    dataCtrl.EquiCtrl.Version,
                    ip,
                    dataCtrl.EquiCtrl.MacAddress,
                    dataCtrl.EquiCtrl.Notes);
                if (newEquipementWindow.ShowDialog() == true)
                {
                    dataCtrl.EquiCtrl.DeleteFromProject(dataCtrl.ProjCtrl);
                    CreateEquipFromWindow(newEquipementWindow);
                    dataCtrl.EquiCtrl.AddToProject(dataCtrl.ProjCtrl);
                }
            }
        }
             private void ScanPortFromMain_Click(object sender, RoutedEventArgs e)
        {
            if (SetSelectedEquip())
            {
                if(dataCtrl.EquiCtrl.Ip != null)
                {
                    ipInvalidPortScan.Visibility = Visibility.Collapsed;
                    IpScanTextBox.Text = dataCtrl.EquiCtrl.Ip.ToString();
                    Dispatcher.BeginInvoke((Action)(() => scanTabControl.SelectedIndex = 1));
                    dataCtrl.NetCtrl.PortScan(dataCtrl.EquiCtrl.Ip);
                }
            }
        }

        private void CreateEquipFromWindow(CreateEquipementWindow window)
        {
            dataCtrl.EquiCtrl.Name = window.ProjName.Text;
            dataCtrl.EquiCtrl.Os = window.Os.Text;
            dataCtrl.EquiCtrl.Type = window.Type.Text;
            dataCtrl.EquiCtrl.Version = window.Version.Text;
            dataCtrl.EquiCtrl.MacAddress = window.MacAddr.Text;
            dataCtrl.EquiCtrl.Notes = window.Notes.Text;
            dataCtrl.EquiCtrl.Ip = dataCtrl.NetCtrl.StringtoIp(window.Ip.Text);
            dataCtrl.EquiCtrl.IsActiveCheck();
        }
        private bool SetSelectedEquip()
        {
            if(EquipList.SelectedItem != null)
            {
                dataCtrl.EquiCtrl.Equipement = EquipList.SelectedItem as Equipement;
                return true;
            }
            return false;
        }
    }
}
