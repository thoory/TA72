using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TA72.Models;

namespace TA72.Controllers
{
    class ProjectController : INotifyPropertyChanged
    {
        Project _project;
        public ProjectController()
        {
            _project = new Project("Unknown", "Uknown");
        }

        #region Properties
        public Project Project
        {
            get { return _project; }
            set { _project = value; }
        }

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
            get { return Project.Desc; }
            set { Project.Desc = value; }
        }
        #endregion

        #region Functions
        public void AddEquipement(Equipement equipement)
        {
            Project.Equipements.Add(equipement);
        }
        public void RemoveEquipement(Equipement equipement)
        {
            Project.Equipements.Remove(equipement);
        }
        public void save()
        {
            Serializer serializer = new Serializer();
            serializer.save(Project);
        }

        public void load(String path)
        {
            Serializer serializer = new Serializer();
            Project = serializer.load(path);
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
