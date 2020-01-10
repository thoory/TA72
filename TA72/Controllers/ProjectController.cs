using System;
using System.Collections.Generic;
using System.Text;
using TA72.Models;

namespace TA72.Controllers
{
    class ProjectController
    {
        public ProjectController(){}

        public Project create(string name, string desc) {
            Project p;

            p = new Project(name, desc);

            return p;
        }

        public void AddEquipement(Project project, Equipement equipement)
        {
            this.GetEquipements(project).Add(equipement);
        }
        public void RemoveEquipement(Project project, Equipement equipement)
        {
            this.GetEquipements(project).Remove(equipement);
        }
        public List<Equipement> GetEquipements(Project project)
        {
            List<Equipement> equipements = new List<Equipement>();
            if (project != null)
                 equipements = project.Equipements;
            return equipements;
        }

        public String getName(Project p)
        {
            return p.Name;
        }
        public String getDesc(Project p)
        {
            return p.Desc;
        }

        public void setName(Project p, String name)
        {
            p.Name = name;
            p.Lastupdate = DateTime.Now;
        }
        public void setDesc(Project p, String desc)
        {
            p.Desc = desc;
            p.Lastupdate = DateTime.Now;
        }
        public void save(Project p)
        {
            p.save();
        }

        public Project load(String path)
        {
            Serializer serializer = new Serializer();
            Project p = serializer.load(path);

            return p;
        }
    }
}
