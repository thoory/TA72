using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using TA72.Models;

namespace TA72.Controllers
{
    class EquipementController : ModuleController
    {
        public EquipementController() { }

        #region Properties
        public Equipement Equipement { get; set; }
        public string Type
        {
            get { return Equipement.Type; }
            set
            {
                Equipement.Type = value;
                Equipement.LastUpdate = DateTime.Now;
            }
        }

        public string Os
        {
            get { return Equipement.Os; }
            set
            { 
                Equipement.Os = value;
                Equipement.LastUpdate = DateTime.Now;
            }
        }

        public string Version
        {
            get { return Equipement.Version; }
            set
            { 
                Equipement.Version = value;
                Equipement.LastUpdate = DateTime.Now;
            }
        }

        public IPAddress Ip
        {
            get { return Equipement.Ip; }
            set
            { 
                Equipement.Ip = value;
                Equipement.LastUpdate = DateTime.Now;
            }
        }

        public bool IsActive
        {
            get { return Equipement.IsActive; }
            set
            {
                Equipement.IsActive = value;
                Equipement.LastUpdate = DateTime.Now;
            }
        }
        #endregion

        #region Functions
        public Equipement Create(string name)
        {
            return new Equipement(name);
        }

        /*public void Delete(Equipement equipement)
        {
            ProjectController projectController = new ProjectController();
            projectController.RemoveEquipement(equipement);
            return equipement;
        }*/

        public void IsActiveCheck()
        {
            IsActive = false;
            if (NetworkController.Ping(Ip))
                IsActive = true;
        }
        #endregion
    }
}
