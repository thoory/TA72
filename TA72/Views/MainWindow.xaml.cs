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
            EquipementController equipCtrl = new EquipementController();
            Equipement e1 = equipCtrl.Create("equip1", p);
            Equipement e2 = equipCtrl.Create("equip2", p);
            Equipement e3 = equipCtrl.Create("equip3", p);

            foreach (Equipement equ in projCtrl.GetEquipements(p))
            {
                System.Diagnostics.Trace.WriteLine(equ.name);
            }

            projCtrl.RemoveEquipement(p, e2);
            Equipement e4 = equipCtrl.Create("equip4", p);
            projCtrl.AddEquipement(p, e4);
            foreach (Equipement equ in projCtrl.GetEquipements(p))
            {
                System.Diagnostics.Trace.WriteLine(equ.name);
            }

            /*OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                p = projCtrl.load(openFileDialog.FileName);*/
        }
    }
}
