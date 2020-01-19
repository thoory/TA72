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
    /// Logique d'interaction pour CreateEquipementWindow.xaml
    /// </summary>
    public partial class CreateEquipementWindow : Window
    {
        public CreateEquipementWindow(string name, string type, string os, string version, string ip, string macAddress ,string notes)
        {
            InitializeComponent();
            ProjName.Text = name;
            Type.Text = type;
            Os.Text = os;
            Version.Text = version;
            Ip.Text = ip;
            MacAddr.Text = macAddress;
            Notes.Text = notes;

            ProjName.Focus();
        }
        public CreateEquipementWindow()
        {
            InitializeComponent();
        }
        private void createEquipement(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(ProjName.Text))
            {
                DialogResult = true;
            } else
            {
                WarningSave.Visibility = Visibility.Visible;
            }
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ProjName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(ProjName.Text))
            {
                WarningSave.Visibility = Visibility.Collapsed;
            } else
            {
                WarningSave.Visibility = Visibility.Visible;
            }
        }
    }
}
