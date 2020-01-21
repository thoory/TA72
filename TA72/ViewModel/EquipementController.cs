using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using TA72.Models;

namespace TA72.Controllers
{
    class EquipementController : INotifyPropertyChanged
    {
        public EquipementController() { Create(""); }

        #region Properties
        private Equipement _equipement;
        public Equipement Equipement
        {
            get { return _equipement; }
            set
            {
                _equipement = value;
                RaisePropertyChanged("Equipement");
            }
        }
        public string Name
        {
            get { return Equipement.Name; }
            set
            {
                Equipement.Name = value;
                RaisePropertyChanged("Name");
            }
        }
        public int ImageId
        {
            get { return Equipement.ImageId; }
            set
            {
                Equipement.ImageId = value;
                RaisePropertyChanged("ImageId");
            }
        }
        public string Type
        {
            get { return Equipement.Type; }
            set
            {
                Equipement.Type = value;
                RaisePropertyChanged("Type");
            }
        }
        public string MacAddress
        {
            get { return Equipement.MacAddress; }
            set
            {
                Equipement.MacAddress = value;
                RaisePropertyChanged("MacAddress");
            }
        }
        public string Os
        {
            get { return Equipement.Os; }
            set
            { 
                Equipement.Os = value;
                RaisePropertyChanged("Os");
            }
        }

        public string Version
        {
            get { return Equipement.Version; }
            set
            { 
                Equipement.Version = value;
                RaisePropertyChanged("Version");
            }
        }

        public IPAddress Ip
        {
            get { return Equipement.Ip; }
            set
            { 
                Equipement.Ip = value;
                RaisePropertyChanged("Ip");
            }
        }

        public string Notes
        {
            get { return Equipement.Notes; }
            set
            {
                Equipement.Notes = value;
                RaisePropertyChanged("Notes");
            }
        }

        public bool IsActive
        {
            get { return Equipement.IsActive; }
            set
            {
                Equipement.IsActive = value;
                LastUpdate = DateTime.Now;
                RaisePropertyChanged("IsActive");
            }
        }
        public DateTime LastUpdate
        {
            get { return Equipement.LastUpdate; }
            set
            {
                Equipement.LastUpdate = value;
                RaisePropertyChanged("LastUpdate");
            }
        }
        #endregion

        #region Functions
        public void Create(string name)
        {
            this.Equipement = new Equipement(name);
        }

        public void Create(IPAddress ip)
        {
            if(ip != null)
            {
                Create(ip.ToString());
                Equipement.Ip = ip;
            }
        }

        public void DeleteFromProject(ProjectController projCtrl)
        {
            projCtrl.RemoveEquipement(Equipement);
        }
        public void AddToProject(ProjectController projCtrl)
        {
            projCtrl.AddEquipement(Equipement);
        }
        public void IsActiveCheck()
        {
            IsActive = false;
            if (Ping(Ip))
              IsActive = true;
        }
        public void IsActiveCheck(ProjectController projCtrl)
        {
            ObservableCollection<Equipement> list = new ObservableCollection<Equipement>();
            foreach (Equipement e in projCtrl.Equipements)
            {
                if(e.Ip != null)
                {
                    Equipement = e;
                    IsActiveCheck();
                }
                LastUpdate = DateTime.Now;
                list.Add(e);
            }
            projCtrl.Equipements = list;
        }

        public bool Ping(IPAddress ip)
        {
            Ping ping = new Ping();
            PingReply pingReply;

            try
            {
                pingReply = ping.Send(ip.ToString(), 3);
                if (pingReply != null &&
                    pingReply.Status == IPStatus.Success)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        #region PropertyChange

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
