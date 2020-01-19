using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TA72.Models
{
    class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Lastupdate { get; set; }
        public string Path { get; set; }
        public ObservableCollection<Equipement> Equipements { get; set; }

        public Project(string name, string desc)
        {
            this.Name = name;
            this.Description = desc;
            this.CreationDate = DateTime.Now;
            this.Lastupdate = DateTime.Now;
            this.Equipements = new ObservableCollection<Equipement>();
        }
    }
}
