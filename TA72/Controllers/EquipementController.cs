using System;
using System.Collections.Generic;
using System.Text;
using TA72.Models;

namespace TA72.Controllers
{
    class EquipementController : ModuleController
    {
        public EquipementController() { }

        public Equipement Create(string name, Project project)
        {
            ProjectController projectController = new ProjectController();
            Equipement equipement = new Equipement(name);
            projectController.AddEquipement(project, equipement);
            return equipement;
        }
        public Equipement Delete(Equipement equipement, Project project)
        {
            ProjectController projectController = new ProjectController();
            projectController.RemoveEquipement(project, equipement);
            return equipement;
        }

        public void SetName(Equipement equipement, String name)
        {
            equipement.name = name;
            equipement.lastUpdate = DateTime.Now;
        }
        public void SetImageId(Equipement equipement, String name)
        {
            equipement.name = name;
            equipement.lastUpdate = DateTime.Now;
        }
        public void SetType(Equipement equipement, String type)
        {
            equipement.type = type;
            equipement.lastUpdate = DateTime.Now;
        }
        public String GetType(Equipement equipement)
        {
            return equipement.type;
        }

        public void SetOs(Equipement equipement, String OperatingSystem)
        {
            equipement.os = OperatingSystem;
            equipement.lastUpdate = DateTime.Now;
        }
        public String GetOs(Equipement equipement)
        {
            return equipement.os;
        }

        public void SetVersion(Equipement equipement, String version)
        {
            equipement.version = version;
            equipement.lastUpdate = DateTime.Now;
        }
        public String GetVersion(Equipement equipement)
        {
            return equipement.version;
        }
        public void SetIp(Equipement equipement, String ip)
        {
            equipement.ip = ip;
            equipement.lastUpdate = DateTime.Now;
        }
        public String GetIp(Equipement equipement)
        {
            return equipement.ip;
        }

        public void SetMac(Equipement equipement, String mac)
        {
            equipement.mac = mac;
            equipement.lastUpdate = DateTime.Now;
        }
        public String GetMac(Equipement equipement)
        {
            return equipement.mac;
        }

        public void SetIsActive(Equipement equipement, Boolean isActive)
        {
            equipement.isActive = isActive;
            equipement.lastUpdate = DateTime.Now;
        }
        public Boolean GetIsActive(Equipement equipement)
        {
            return equipement.isActive;
        }
    }
}
