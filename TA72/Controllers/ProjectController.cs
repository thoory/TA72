using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using TA72.Models;

namespace TA72.Controllers
{
    class ProjectController : INotifyPropertyChanged
    {
        public ProjectController()
        {
            Project = new Project("Unknown", "Unknown");
        }

        #region Properties
        public Project Project { get; set; }

        public List<Equipement> Equipements
        {
            get { return Project.Equipements; }
            set {Project.Equipements = value; }
        }

        public string Name
        {
            get { return Project.Name; }
            set
            { 
                Project.Name = value;
                Project.Lastupdate = DateTime.Now;
                RaisePropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return Project.Description; }
            set
            {
                Project.Description = value;
                RaisePropertyChanged("Description");
            }
        }

        public string Path
        {
            get { return Project.Path; }
            set { Project.Path = value; }
        }
        #endregion

        #region Functions
        public void CreateProject(string name, string description)
        {
            this.Project = new Project(name, description);
            Refresh();
        }
        public void AddEquipement(Equipement equipement)
        {
            Project.Equipements.Add(equipement);
        }
        public void RemoveEquipement(Equipement equipement)
        {
            Project.Equipements.Remove(equipement);
        }
        public void Save()
        {
            Serializer serializer = new Serializer();
            if (String.IsNullOrEmpty(Path))
            {
                this.SaveAs();
            } else
            {
                serializer.Save(Project, Path);
            }
        }

        public void SaveAs()
        {
            Serializer serializer = new Serializer();
            Path = serializer.Save(Project);
        }

        public void Load()
        {
            Serializer serializer = new Serializer();
            serializer.Load(this);
            Refresh();
        }

        public void Refresh()
        {
            Name = Project.Name;
            Description = Project.Description;
            Equipements = Project.Equipements;
        }
        #endregion

        #region INotifyOrioertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
